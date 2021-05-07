using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFlashGreenOnLowStamina : MonoBehaviour
{
    private Renderer rend;
    private Color white = Color.white;
    private Color green = Color.green;
    private Transform playerSkin;

    private void Start()
    {
        playerSkin = GameObject.FindWithTag("PlayerSkinRender").transform;
        rend = playerSkin.GetComponent<Renderer>();
    }

    private void LateUpdate()
    {
        if (StaminaBar.Instance == null) return;
        FlashGreen();
    }

    private IEnumerator FlashGreenCo()
    {
        rend.material.color = white;
        yield return new WaitForSeconds(0.09f);
        rend.material.color = green;
        yield return new WaitForSeconds(0.09f);
        rend.material.color = white;
    }

    private void FlashGreen()
    {
        if (Input.GetKey(KeyCode.LeftShift) && StaminaBar.Instance.currentStamina <= 0.2f)
        {
            StartCoroutine(FlashGreenCo());
        }
    }
}
