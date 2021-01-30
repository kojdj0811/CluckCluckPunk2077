using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CsFx_Laser : CsFxEntity
{
    public Transform raserAnchor;
    public SpriteRenderer spriteRenderer;
    public LayerMask targetLayer;

    public AnimationCurve fadeoutCurve;

    private Vector3 from;
    private Vector3 to;

    public float rayRange = 30.0f;
    public float maxLength;
    public float thickness = 0.47f;



    private Color initColor;
    private float animStartTime;

    public override void Awake() {
        Destroy(gameObject, lifetime);
        animStartTime = Time.timeSinceLevelLoad;
        initColor = spriteRenderer.color;
    }



    private void Start() {
        from = transform.position;

        RaycastHit2D hit = Physics2D.Raycast(from, transform.up, rayRange, targetLayer);

        if(hit)
            to = hit.point;
        else
            to = transform.position + transform.up * rayRange;



        float dist = Vector3.Distance(from, to);
        raserAnchor.localScale = new Vector3(thickness, maxLength * dist / rayRange, 1.0f);
    }

    private void Update() {
        Color color = initColor;
        float u = (Time.timeSinceLevelLoad - animStartTime) / lifetime;
        color.a = fadeoutCurve.Evaluate(u);

        spriteRenderer.color = color;
    }
}
