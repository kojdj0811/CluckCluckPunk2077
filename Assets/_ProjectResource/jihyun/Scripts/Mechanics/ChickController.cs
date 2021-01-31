using System.Collections;
using System.Collections.Generic;
using Platformer.Gameplay;
using UnityEngine;
using static Platformer.Core.Simulation;

namespace Platformer.Mechanics
{
    /// <summary>
    /// A simple controller for enemies. Provides movement control over a patrol path.
    /// </summary>
    [RequireComponent(typeof(AnimationController), typeof(Collider2D))]
    public class ChickController : MonoBehaviour
    {
        public AudioClip ouch;
        public float jumpForce;
        public float minDistanceXFromPlayer;
        public float minDistanceYFromPlayer;
        public float skillSpeed;
        protected bool jump;

        internal AnimationController control;
        internal Collider2D _collider;
        internal AudioSource _audio;
        SpriteRenderer spriteRenderer;
        public GameObject player;
        PlayerController playerc;
        Rigidbody2D rb;
        float lastY;
        float startX, endX;
        int layerWalkUpBlock;
        float lastJumpTime;

        public Bounds Bounds => _collider.bounds;

        void Awake()
        {
            var origin = Camera.main.ViewportToWorldPoint(new Vector3(0.25f, 0.1f, 0));
            var extent = Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 0.2f, 0));

            startX = origin.x;
            endX = extent.x;
            lastY = transform.position.y;
            rb = GetComponent<Rigidbody2D>();
            jump = false;
            control = GetComponent<AnimationController>();
            _collider = GetComponent<Collider2D>();
            _audio = GetComponent<AudioSource>();
            spriteRenderer = GetComponent<SpriteRenderer>();
            Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("player"), LayerMask.NameToLayer("chick"), true);
            layerWalkUpBlock = LayerMask.NameToLayer("block");
            playerc = player.GetComponent<PlayerController>();
            lastJumpTime = 0;

        }

        void Update()
        {
            lastJumpTime += Time.deltaTime;
                float x_dist = Mathf.Abs(player.transform.position.x -  transform.position.x);
                float y_diff = player.transform.position.y - transform.position.y;
                if (x_dist > minDistanceXFromPlayer)
                {
                    control.move.x = Mathf.Clamp(player.transform.position.x - transform.position.x, -1, 1);
                }
                else
                {
                    control.move.x = 0;
                }
                if(y_diff > minDistanceYFromPlayer && control.move.x == 0) // 자리 위쪽에 있을 때만 점프한다. 플레이어가 유도해야 함.
                {
                    if (x_dist > 0.5) // 점프 할 때는 플레이어 밑에서만.
                    {
                        control.move.x = Mathf.Clamp(player.transform.position.x - transform.position.x, -1, 1);
                    }else
                    if (rb.velocity.y == 0 && lastJumpTime > 1.5f) {
                        Ray2D ray = new Ray2D(new Vector2(transform.position.x, transform.position.y + 1f), Vector2.up);
                        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);
                        lastY = transform.position.y-1;
                        if (hit.collider != null)
                        {
                            if (hit.collider.gameObject.layer == layerWalkUpBlock)
                                GetComponent<Collider2D>().enabled = false;
                        }

                        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Force);
                    lastJumpTime = 0;
                    }
                }
                if(rb.velocity.y > 0)
                {
                    if (lastY <= transform.position.y)
                    {
                        lastY = transform.position.y;
                    }
                    else
                    {
                        GetComponent<Collider2D>().enabled = true;
                    }
                }
            if (playerc.bPoison == false && Input.GetKey(KeyCode.DownArrow))
            {
                if (player.transform.position.x - transform.position.x > 0)
                {
                    control.move.x = Mathf.Clamp(startX - transform.position.x, -1, 1);
                }
                else
                {
                    control.move.x = Mathf.Clamp(endX - transform.position.x, -1, 1);
                }
            }
            else if (playerc.bPoison == false && Input.GetKey(KeyCode.UpArrow))
            {
                control.move.x = Mathf.Clamp(player.transform.position.x - transform.position.x, -1, 1);

            }
        }

    }
}