using Platformer.Mechanics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gate : MonoBehaviour
{
    public GameObject Cam, stageManger, Player;
    public string direction;
    public int stage, index, keyCond, rewward, stageNum;
    Vector3 dir;
    cameraMove cam;

    void Start()
    {
        cam = Cam.GetComponent<cameraMove>();
        if (direction == "right")
            dir = Vector3.right;
        else if (direction == "left")
            dir = Vector3.left;
        else if (direction == "up")
            dir = Vector3.up;
        else if (direction == "down")
            dir = Vector3.down;


    }
    void OnTriggerEnter2D(Collider2D other)
    {
        int playerKeyCount = Player.GetComponent<Platformer.Mechanics.PlayerController>().keyCount;
        if (other.gameObject.transform == Player.gameObject.transform && playerKeyCount == keyCond)
        {
            if (Player.GetComponent<Platformer.Mechanics.PlayerController>().isChickHere())
            {
                playerKeyCount = 0;
                Player.GetComponent<Platformer.Mechanics.PlayerController>().keyCount = 0;
                cam.StageNum++;
                if (Player.GetComponent<PlayerController>().bPoison)
                    Player.GetComponent<PlayerController>().bPoison = false;
                stageManger.GetComponent<StageChanger>().stageChanger(index);
                Player.GetComponent<PlayerController>().money += rewward;
                cam.StartCoroutine(cam.move(dir));
                //            cam.StartCoroutine(cam.shakeLeftRight());
                //            cam.StartCoroutine(cam.shakeUpDown());
            }
        }
    }
}
