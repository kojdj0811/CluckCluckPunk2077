using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class gate : MonoBehaviour
{
    public GameObject Cam, stageManger, Player;
    public string direction;
    public int stage;
    public int index, keyCond;
    public int stageNum;
    Vector3 dir;
    cameraMove cam;
    int playerKeyCount;
    SpriteRenderer col;
    Color color;

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

    void Update()
    {
    
        stageNum = cam.StageNum;
        playerKeyCount = Player.GetComponent<Platformer.Mechanics.PlayerController>().keyCount;
        if (stageNum == stage && playerKeyCount == keyCond)
        {
            col = gameObject.GetComponent<SpriteRenderer>();
            color = col.color;
            color.a = 1.0f;
            col.color = color;
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.transform == Player.gameObject.transform && playerKeyCount == keyCond)
        {
<<<<<<< HEAD
            playerKeyCount = 0;
            Player.GetComponent<Platformer.Mechanics.PlayerController>().keyCount = 0;
            cam.StageNum++;
=======
            Player.GetComponent<Platformer.Mechanics.PlayerController>().ResetKeyCountFromInitStage();
>>>>>>> bfeb5e493285290dc18c7dc852653420650da0ad
            stageManger.GetComponent<StageChanger>().stageChanger(index);
            cam.StartCoroutine(cam.move(dir));
        }
    }
}
