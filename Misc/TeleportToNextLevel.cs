using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TeleportToNextLevel : MonoBehaviour
{
    public Vector3 playerPosition;
    public GameObject fadeInPanel;
    public GameObject fadeOutPanel;
    public float fadeWait;
    public ParticleSystem psystem1;
    public ParticleSystem psystem2;
    private AudioSource audSrc;
    private IEnumerator _fade;
    private GameObject _fadeOutCopy;
    private Dictionary<string, string> levelMapping;

    private void Awake()
    {
        audSrc = GetComponent<AudioSource>();
        _fadeOutCopy = Instantiate(fadeOutPanel, Vector3.zero, Quaternion.identity);
        _fadeOutCopy.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !other.isTrigger)
        {
            if (_fade != null) return; // check if coroutine is running 
            _fade = FadeCo();
            StartCoroutine(_fade);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (_fade == null) return;
            psystem1.Stop();
            psystem2.Stop();
            StopCoroutine(_fade);
            _fadeOutCopy.SetActive(false);
            _fade = null;
        }
    }

    public IEnumerator FadeCo()
    {
        audSrc.Play();
        if (fadeOutPanel != null)
        {
            _fadeOutCopy.SetActive(true);
            psystem1.Play();
            psystem2.Play();
        }
        yield return new WaitForSeconds(fadeWait);

        LoadLevel();
    }

    private void LoadLevel()
    {
        levelMapping = new Dictionary<string, string>()
        {
            { "Main Menu", "Transition_0_1"},
            { "LevelZero_Tunnel", "LevelOne"},
            { "LevelOne", "LevelTwo"},
            { "LevelTwo", "LevelThree"},
            { "LevelThree", "EndLevel"}
        };
        SceneManager.LoadScene(levelMapping[PreviousLevelChecker.PreviousLevel]);
    }
}

