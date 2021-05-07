using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CutsceneHandler : MonoBehaviour
{ 
    public GameObject player; 
    public GameObject cutsceneCam;
    public GameObject sceneObject1; 
 
    private void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Player"))
        {
            cutsceneCam.SetActive(true);
            player.SetActive(false);

            // move rocket ship 
            sceneObject1.GetComponent<Orbit>().enabled = true;
            StartCoroutine(LoadCreditsScene());
        }
    }

    private IEnumerator LoadCreditsScene()
    {
        yield return new WaitForSeconds(7f);
        SceneManager.LoadSceneAsync("Credits");
    }
}