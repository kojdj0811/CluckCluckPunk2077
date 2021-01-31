using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CsColTest : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other) {
        Debug.Log("name : " + other.name);
        Debug.Log("tag : " + other.tag);
    }
}
