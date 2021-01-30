using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class crackBlock : MonoBehaviour
{
    public GameObject Player;
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.transform == Player.gameObject.transform)
        {
            Invoke("crack", 1);
        }
    }
    
    void crack()
    {
        Destroy(this.gameObject);
    }
}
