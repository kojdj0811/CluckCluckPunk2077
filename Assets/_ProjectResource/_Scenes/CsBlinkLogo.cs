using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CsBlinkLogo : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;

    private void OnMouseDown() {
        SceneManager.LoadScene("stage");
    }

}
