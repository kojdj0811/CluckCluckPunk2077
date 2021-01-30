using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageChanger : MonoBehaviour
{


    public void stageChanger(int index)
    {
        if (index -2 >= 0 && this.transform.GetChild(index - 2).gameObject.activeSelf)
            this.transform.GetChild(index - 2).gameObject.SetActive(false);
        if (index + 2 < this.transform.childCount && !this.transform.GetChild(index + 2).gameObject.activeSelf)
            this.transform.GetChild(index + 2).gameObject.SetActive(true);

    }

}
