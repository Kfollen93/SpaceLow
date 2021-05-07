using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextPhase : MonoBehaviour
{
    [SerializeField] private ArenaStart colTrigger; 
    public  GameObject firstPhaseObjects; 
    public GameObject secondPhaseObjects; 
    
    void Start()
    {
        colTrigger.OnPlayerEnters += ColTrigger_OnPlayerEnters;         
    }

    private void ColTrigger_OnPlayerEnters(object sender, System.EventArgs e) 
    { 
        firstPhaseObjects.SetActive(false);
        secondPhaseObjects.SetActive(true); 
    }
}
