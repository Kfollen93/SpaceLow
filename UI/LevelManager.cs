using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public Vector3 playerPosition;
    public GameObject fadeInPanel;
    public GameObject fadeOutPanel;
    public float fadeWait;
    private AudioSource audSrc;
    public string levels;

    private void Awake()
    {
        audSrc = GetComponent<AudioSource>();
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !other.isTrigger)
        {
            StartCoroutine(FadeCo());
        }
    }

    public IEnumerator FadeCo()
    {
        audSrc.Play();
        if (fadeOutPanel != null)
        {
            Instantiate(fadeOutPanel, Vector3.zero, Quaternion.identity);
        }
        yield return new WaitForSeconds(fadeWait);
        SceneManager.LoadScene(levels);
    }
}
