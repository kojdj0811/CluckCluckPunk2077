using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shopUIManager : MonoBehaviour
{
    public GameObject Cam;
    cameraMove cam;

    void Start()
    {
        cam = Cam.GetComponent<cameraMove>();
    }

    void Update()
    {
        int stage = cam.StageNum;
        if (stage == 4 || stage == 9)
            this.transform.GetChild(0).gameObject.SetActive(true);
        else
            this.transform.GetChild(0).gameObject.SetActive(false);
    }
}
