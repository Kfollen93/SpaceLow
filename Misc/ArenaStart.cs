using System;
using UnityEngine;

public class ArenaStart : MonoBehaviour
{
    public event EventHandler OnPlayerEnters; 
    
    private void OnTriggerEnter(Collider col)
    { 
        if (col.CompareTag("Player"))
        { 
            // Player inside trigger
            OnPlayerEnters?.Invoke(this, EventArgs.Empty);

            // Prevent player from triggering it again
            Destroy(gameObject);
        }
    }
}
