using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class MenuController : MonoBehaviour
{
    public GameObject grayBackground;
    public GameObject restartButton;
    private bool isMenuOpen;
    public PlayerTakeDamage playerTakeDmgScript;
    private bool PlayerIsDead => playerTakeDmgScript.currentHealth < 1;

    private void Start()
    {
        HideCursor();
    }

    private void Update()
    {
        if (!isMenuOpen && Input.GetKeyDown(KeyCode.Escape))
        {
            OpenMenu();
        }
        else if (isMenuOpen && Input.GetKeyDown(KeyCode.Escape))
        {
            CloseMenu();
        }
        else if (PlayerIsDead)
        {
            OpenMenu();
            restartButton.gameObject.SetActive(true);
        }
    }

    private void OpenMenu()
    {
        DisplayCursor();
        isMenuOpen = true;
        grayBackground.SetActive(true);
    }

    private void CloseMenu()
    {
        HideCursor();
        isMenuOpen = false;
        grayBackground.SetActive(false);
    }

    private void HideCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void DisplayCursor()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}