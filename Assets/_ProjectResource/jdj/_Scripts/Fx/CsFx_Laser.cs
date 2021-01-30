using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CsFx_Laser : CsFxEntity
{
    public Transform raserAnchor;
    public LayerMask targetLayer;

    private Vector3 from;
    private Vector3 to;

    public float rayRange = 50.0f;

    public override void Awake() {
        Destroy(gameObject, lifetime);
    }

    private void Start() {
        from = transform.position;

        RaycastHit2D hit = Physics2D.Raycast(from, transform.up, rayRange, targetLayer);

        if(hit)
            to = hit.point;
        else
            to = transform.position + transform.up * rayRange;



        

    }
}
