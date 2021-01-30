using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraMove : MonoBehaviour
{
    public float cameraSpeed = 0.3f, shakeRange_LR = 1.0f, shakeSpeed_LR = 0.4f;
    public float shakeRange_UD = 1.0f, shakeSpeed_UD = 0.4f;
    public GameObject player, chick;
    Vector3 camPos;

    void Start()
    {
        camPos = this.transform.position;
    }

    public IEnumerator move(Vector3 dir)
    {
        Vector3 pos = this.transform.position;
        float slow = 0.1f;
        bool f_slow = true;
        while (true)
        {
            transform.Translate(dir * cameraSpeed);
            if (Mathf.Abs(pos.x - transform.position.x) >= 30 || Mathf.Abs(pos.y - transform.position.y) >= 20)
            {
                break;
            }
            cameraSpeed -= slow;
            slow -= 0.02f;
            if (slow <= 0.0f && f_slow)
                slow = 0.02f;
            if (cameraSpeed <= 0.0f)
            {
                f_slow = false;
                cameraSpeed = 0.02f;
            }
            yield return null;
        }

        if (transform.position.x < 15)
            transform.position = new Vector3(3.3f, transform.position.y);
        if (transform.position.x > 30)
            transform.position = new Vector3(33.3f, transform.position.y);
        if (transform.position.y < 10)
            transform.position = new Vector3(transform.position.x, -0.5f);
        else if (transform.position.y < 30)
            transform.position = new Vector3(transform.position.x, 19.5f);
        else if (transform.position.y < 40)
            transform.position = new Vector3(transform.position.x, 39.5f);


        player.transform.position = new Vector3(this.transform.position.x - 15.0f, this.transform.position.y - 6, player.transform.position.z);
        player.gameObject.GetComponent<Collider2D>().isTrigger = true;
        player.gameObject.GetComponent<Rigidbody2D>().gravityScale = 0;
        while (true)
        {
            player.transform.Translate(Vector3.right * 0.1f);
            if(player.transform.position.x >= this.transform.position.x - 9)
                break;
            yield return null;
        }
        player.gameObject.GetComponent<Collider2D>().isTrigger = false;
        player.gameObject.GetComponent<Rigidbody2D>().gravityScale = 1;
        player.transform.position = new Vector3(this.transform.position.x - 9.0f, player.transform.position.y, player.transform.position.z);
        slow = 0.1f;
    }

    public IEnumerator shakeLeftRight() {
        Vector3 dir = Vector3.right;
        Vector3 pos = this.transform.position;
        float tempRange = shakeRange_LR;
        while (true)
        {
            transform.Translate(dir * shakeSpeed_LR);
            if (Mathf.Abs(pos.x - transform.position.x) >= shakeRange_LR && (pos.x - transform.position.x) * dir.x < 0 )
                dir *= -1;
            shakeRange_LR -= 0.1f;
            if (shakeRange_LR <= 0)
                break;
            yield return null;
        }
        this.transform.position = pos;
        shakeRange_LR = tempRange;
    }
    public IEnumerator shakeUpDown()
    {
        Vector3 dir = Vector3.up;
        Vector3 pos = this.transform.position;
        float tempRange = shakeRange_UD;

        while (true)
        {
            transform.Translate(dir * shakeSpeed_UD);
            if (Mathf.Abs(pos.y - transform.position.y) >= shakeRange_UD && (pos.y - transform.position.y) * dir.y < 0)
                dir *= -1;
            shakeRange_UD -= 0.1f;
            if (shakeRange_UD <= 0)
                break;
            yield return null;
        }
        this.transform.position = pos;
        shakeRange_UD = tempRange;
    }

}
