using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Platformer.Gameplay;
using static Platformer.Core.Simulation;
using Platformer.Model;
using Platformer.Core;

// 버프 적용 샘플 코드... 버프/디버프에 따라 필요한 값은 인자(값/유지시간)을 같이 보내주면 됩니다.
// peMng.add(player_effect.enumPlayerEffectType.HighJumping);


namespace Platformer.Mechanics
{
    /// <summary>
    /// This is the main class used to implement control of the player.
    /// It is a superset of the AnimationController class, but is inlined to allow for any kind of customisation.
    /// </summary>
    public class PlayerController : KinematicObject
    {
        public AudioClip jumpAudio;
        public AudioClip respawnAudio;
        public AudioClip ouchAudio;
        public int jumpForce = 500;
        public int keyCount;

        /// <summary>
        /// Max horizontal speed of the player.
        /// </summary>
        public float maxSpeed = 7;
        /// <summary>
        /// Initial jump velocity at the start of a jump.
        /// </summary>
        public float jumpTakeOffSpeed = 7;

        public JumpState jumpState = JumpState.Grounded;
        protected bool stopJump;
        public Collider2D collider2d;
        public AudioSource audioSource;
        [HideInInspector]
        public Health health;
        [HideInInspector]
        public bool bPoison = false;
        public bool controlEnabled = true;
        player_effect_manager peMng;

        float lastY = 0;
        bool jump;
        bool run;
        Vector2 move;
        SpriteRenderer spriteRenderer;
        internal Animator animator;
        readonly PlatformerModel model = Simulation.GetModel<PlatformerModel>();
        int layerWalkUpBlock;
        Rigidbody2D rb;

        float DoubleArrowKeyDeltaTime = 0;
        bool bFirstArrowKey = false;

        public Bounds Bounds => collider2d.bounds;

        void Awake()
        {
            health = GetComponent<Health>();
            audioSource = GetComponent<AudioSource>();
            collider2d = GetComponent<Collider2D>();
            spriteRenderer = GetComponent<SpriteRenderer>();
            animator = GetComponent<Animator>();
            rb = GetComponent<Rigidbody2D>();
            peMng = gameObject.AddComponent<player_effect_manager>();

            layerWalkUpBlock = LayerMask.NameToLayer("block");
            run = false;
        }

        public void AddHealth(float _value)
        {
            if(_value < 0) // hurt!!
            {
                health.Decrement( -_value);
                animator.SetTrigger("hurt");
                if (health.IsAlive == false)
                    animator.SetBool("dead", true);
            }
            else  // healing
            {
                health.Increment(_value);
            }
        }

        public void HighJumping()
        {
            jumpState = JumpState.Jumping;
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Force);
            UpdateJumpState();
        }

        void OnCollisionEnter2D(Collision2D collision)
        {
            var tagName = collision.gameObject.tag;
            if (tagName == "enemy")
            {
                health.Decrement();
            }else if(tagName == "block_cloud" && jumpState == JumpState.Grounded)
            {

            }else if(tagName == "block_water" && jumpState == JumpState.Grounded)  // 물 블러. 3초 동안 30% 이동 속도 감소
            {

            }
            else if (tagName == "block_thorn" && jumpState == JumpState.Grounded)  // 가시 블럭. 데미지 0.5
            {

            }
            else if (tagName == "key")
            {
                keyCount++;
                collision.gameObject.SetActive(false);
            }
        }

        protected override void Update()
        {
            if (controlEnabled)
            {
                move.x = Input.GetAxis("Horizontal");
                if (jumpState == JumpState.Grounded && Input.GetButtonDown("Jump"))
                {
                    jumpState = JumpState.PrepareToJump;
                }
                else if (Input.GetButtonUp("Jump"))
                {
                    stopJump = true;
                    Schedule<PlayerStopJump>().player = this;
                }
            }
            else
            {
                move.x = 0;
            }
            UpdateJumpState();

            if (bPoison== false && (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow)) )
            {
                if (bFirstArrowKey == false)
                {
                    DoubleArrowKeyDeltaTime = Time.deltaTime;
                    bFirstArrowKey = true;
                    run = false;
                } 
                else if(DoubleArrowKeyDeltaTime < 0.4)
                {
                    run = true;
                }
                else
                {
                    DoubleArrowKeyDeltaTime = 0;
                    bFirstArrowKey = false;
                    run = false;
                }
            }
            else if (run == true && (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow)) )
            {
                // Debug.Log("RUN !!!");
            }else if( (bFirstArrowKey== true && (DoubleArrowKeyDeltaTime) > 0.4) || run == true)
            {
                DoubleArrowKeyDeltaTime = 0;
                bFirstArrowKey = false;
                run = false;
            }else if(bFirstArrowKey == true)
            {
                DoubleArrowKeyDeltaTime += Time.deltaTime;
            }

            base.Update();
        }

        void UpdateJumpState()
        {
            jump = false;
            switch (jumpState)
            {
                case JumpState.PrepareToJump:
                    //Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("player"), LayerMask.NameToLayer("ground"), true);
                    jumpState = JumpState.Jumping;
                    lastY = transform.position.y;
                    jump = true;
                    stopJump = false;
                    break;
                case JumpState.Jumping:
                    //Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("player"), LayerMask.NameToLayer("ground"), false);
                    Ray2D ray = new Ray2D(new Vector2(transform.position.x, transform.position.y + 1f), Vector2.up);
                    RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);
                    if (hit.collider != null)
                    {
                        if (hit.collider.gameObject.layer == layerWalkUpBlock)
                            GetComponent<Collider2D>().enabled = false;
                    }

                    if (!IsGrounded)
                    {
                        Schedule<PlayerJumped>().player = this;
                        jumpState = JumpState.InFlight;
                    }
                    break;
                case JumpState.InFlight:
                    if( lastY <= transform.position.y )
                    {
                        lastY = transform.position.y;
                    }
                    else
                    {
                        GetComponent<Collider2D>().enabled = true;
                    }

                    if (IsGrounded)
                    {
                        Schedule<PlayerLanded>().player = this;
                        jumpState = JumpState.Landed;
                    }
                    break;
                case JumpState.Landed:
                    jumpState = JumpState.Grounded;
                    break;
            }
        }

        protected override void ComputeVelocity()
        {
            if (jump && IsGrounded)
            {
                velocity.y = jumpTakeOffSpeed * model.jumpModifier;
                jump = false;
            }
            else if (stopJump)
            {
                stopJump = false;
                if (velocity.y > 0)
                {
                    velocity.y = velocity.y * model.jumpDeceleration;
                }
            }

            if (move.x > 0.01f)
                spriteRenderer.flipX = false;
            else if (move.x < -0.01f)
                spriteRenderer.flipX = true;

            //animator.SetBool("grounded", IsGrounded);
            //animator.SetFloat("velocityX", Mathf.Abs(velocity.x) / maxSpeed);

            targetVelocity = move * maxSpeed;
        }

        public enum JumpState
        {
            Grounded,
            PrepareToJump,
            Jumping,
            InFlight,
            Landed
        }
    }
}