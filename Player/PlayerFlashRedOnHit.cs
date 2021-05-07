using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFlashRedOnHit : MonoBehaviour
{
    private Renderer rend;
    private Color red = Color.red;
    private Color white = Color.white;
    private Transform playerSkin;

    private void Start()
    {
        playerSkin = GameObject.FindWithTag("PlayerSkinRender").transform;
        rend = playerSkin.GetComponent<Renderer>();
    }

    // Called in PlayerTakeDamage.cs script
    public IEnumerator FlashRed()
    {
        rend.material.color = red;
        yield return new WaitForSeconds(0.1f);
        rend.material.color = white;
        yield return new WaitForSeconds(0.1f);
        rend.material.color = red;
        yield return new WaitForSeconds(0.1f);
        rend.material.color = white;
    }
}
