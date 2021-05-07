using UnityEngine; 

// Utility for visualizing patrol points set in Unity. 
public class Waypoint : MonoBehaviour
{ 
    [SerializeField]
    protected float debugDrawRadius = 1.0F; 

    public virtual void OnDrawGizmos() 
    {
        Gizmos.color = Color.yellow; 
        Gizmos.DrawWireSphere(transform.position, debugDrawRadius);
    }
}