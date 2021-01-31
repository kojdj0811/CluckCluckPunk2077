using Platformer.Mechanics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class goldText : MonoBehaviour
{
    public GameObject player;

    void Update()
    {
        this.gameObject.GetComponent<Text>().text = player.GetComponent<PlayerController>().money.ToString();
    }
}
