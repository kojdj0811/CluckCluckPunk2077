using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class goldText : MonoBehaviour
{
    public GameObject player;
    void Update()
    {
        gameObject.GetComponent<Text>().text = player.GetComponent<Platformer.Mechanics.PlayerController>().money.ToString();

    }
}
