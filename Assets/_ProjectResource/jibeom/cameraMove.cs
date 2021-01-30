using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraMove : MonoBehaviour
{
    public float cameraSpeed = 0.3f, shakeRange_LR = 1.0f, shakeSpeed_LR = 0.4f;
    public float shakeRange_UD = 1.0f, shakeSpeed_UD = 0.4f;
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
                break;
            cameraSpeed -= slow;
            slow -= 0.02f;
            if (slow <= 0.0f && f_slow)
                slow = 0.02f;
            if (cameraSpeed <= 0.0f)
            {
                f_slow = false;
                cameraSpeed = 0.02f;
            }
            Debug.Log(cameraSpeed);
            yield return null;
        }
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
