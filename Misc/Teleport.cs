using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Teleport : MonoBehaviour 
{ 
    public Transform teleportTarget; 
    public GameObject player; 
    public Vector3 offset; // offset to apply to teleport destination to prevent endless loop
    public ParticleSystem psystem1;
    public ParticleSystem psystem2; 
    private AudioSource audioSrc; 
    public float teleportDelay; 
    private IEnumerator _TP; 

    private void Awake()
    { 
        audioSrc = GetComponent<AudioSource>();
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Player") && !col.isTrigger)
        {
            if (_TP != null) return; 
            _TP = TeleportAnimate();  //create instance to handle stopping teleporting 
            StartCoroutine(_TP);
        }
    }

    public void OnTriggerExit(Collider other)
    { 
        if (other.CompareTag("Player"))
        {
            if (_TP == null) return;
            psystem1.Stop();
            psystem2.Stop(); 
            StopCoroutine(_TP);
            _TP = null; 
        }
    }

    public IEnumerator TeleportAnimate()
    {
        audioSrc.Play(); 
        psystem1.Play();
        psystem2.Play(); 
        yield return new WaitForSeconds(teleportDelay);     
        player.transform.position = teleportTarget.transform.position + offset;
        player.transform.rotation = teleportTarget.transform.rotation; 
    }
}