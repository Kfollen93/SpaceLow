using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ToMainMenu : MonoBehaviour
{
    // Public method for OnClick event
    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
