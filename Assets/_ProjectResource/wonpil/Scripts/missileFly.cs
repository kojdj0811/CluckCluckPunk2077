using System;
using UnityEngine;

public class missileFly : MonoBehaviour
{
    [SerializeField]
    private float missileSpeed = 0.05f;

    private Vector3 direction;
    private GameObject target;

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player");
        Vector3 targetPos = target.transform.position;
        Vector3 targetPosFlattened = new Vector3(targetPos.x, targetPos.y, 0);
        transform.LookAt(targetPosFlattened);
        direction = Vector3.forward * missileSpeed;
    }
    
    void Update()
    {
        transform.Translate(direction);
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Player collider에 트리거가 없네요.
        if(other.gameObject == target)
        {
            Debug.Log("Player Hit!");
        }
    }
    private void OnBecameInvisible()
    {
        //Debug("Destroying bullet...");
        Destroy(gameObject);
    }
}
