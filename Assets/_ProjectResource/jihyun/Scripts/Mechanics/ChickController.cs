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
        protected bool jump;

        
        internal AnimationController control;
        internal Collider2D _collider;
        internal AudioSource _audio;
        SpriteRenderer spriteRenderer;
        GameObject player;
        Rigidbody2D rb;
        float lastY;

        public Bounds Bounds => _collider.bounds;

        void Awake()
        {
            lastY = transform.position.y;
            rb = GetComponent<Rigidbody2D>();
            jump = false;
            control = GetComponent<AnimationController>();
            _collider = GetComponent<Collider2D>();
            _audio = GetComponent<AudioSource>();
            spriteRenderer = GetComponent<SpriteRenderer>();
            Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("player"), LayerMask.NameToLayer("chick"), true);
        }

        void OnCollisionEnter2D(Collision2D collision)
        {
            var player = collision.gameObject.GetComponent<PlayerController>();
            if (player != null)
            {
                var ev = Schedule<PlayerEnemyCollision>();
                ev.player = player;
            }
        }

        void Update()
        {
            if( player == null)
            {
                player = GameObject.FindGameObjectWithTag("Player");
            }
            else
            {
                float x_dist = Mathf.Abs(player.transform.position.x -  transform.position.x);
                float y_diff = player.transform.position.y - transform.position.y;
                if (x_dist > 3)
                {
                    control.move.x = Mathf.Clamp(player.transform.position.x - transform.position.x, -1, 1);
                }
                else
                {
                    control.move.x = 0;
                }
                if(y_diff > 2 && control.move.x == 0) // 자리 위쪽에 있을 때만 점프한다. 플레이어가 유도해야 함.
                {
                    if (x_dist > 0.5)
                    {
                        control.move.x = Mathf.Clamp(player.transform.position.x - transform.position.x, -1, 1);
                    }else
                    if (rb.velocity.y == 0) {
                        Debug.Log(" JUNMP!! Chick!!");
                        Ray2D ray = new Ray2D(new Vector2(transform.position.x, transform.position.y + 1f), Vector2.up);
                        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);
                        lastY = transform.position.y-1;
                        if (hit.collider != null)
                        {
                            if (hit.collider.gameObject.layer == LayerMask.NameToLayer("ground"))
                                GetComponent<Collider2D>().enabled = false;
                        }

                        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Force);
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
            }
        }

    }
}