// Orbit.cs 
/** Class: Orbit 

Class is used to animate environment objects. more specifically, handles moving
or orbiting objects 
*/ 

// libraries 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orbit : MonoBehaviour
{
    [SerializeField] private GameObject _source; 
    [SerializeField] private float _objectSpeed; 
    [SerializeField] private int _moveDirection; 
    [SerializeField] private bool _orbitalZ, _lineTrajectory, _orbitalY; 

    // need instance of original object 
    [SerializeField] private GameObject _originalObject; 

    private Vector3 _originalPosition;
    private Quaternion _originalRotation;
    
    void Start()
    {
        // keep track of original position of objects that move in straight line 
        _originalPosition = _originalObject.transform.position;
        _originalRotation = _originalObject.transform.rotation;
    }

    void Update()
    // will handle if the object should orbit, move in a straight line, or rotate in place
    {
        float moveStep =  _objectSpeed * Time.deltaTime; // Move our position a step closer to the target.

        if (_orbitalZ && !_lineTrajectory && !_orbitalY)
        { 
            // should check that _source isn't null; otherwise, object can't orbit 
            transform.RotateAround(_source.transform.position, (_moveDirection) * Vector3.forward, moveStep);     
        }
        else if (_orbitalY && !_orbitalZ && !_lineTrajectory)
        { 
            // NOTE: will rotate if object is right above source 
            transform.RotateAround(_source.transform.position, (_moveDirection) * Vector3.up, moveStep);     
        }
        else if (_lineTrajectory && !_orbitalZ && !_orbitalY)
        { 
            // should check if there is an end destination
            // NOTE: can still use source object 
            transform.position = Vector3.MoveTowards(transform.position, _source.transform.position, moveStep); 

            // check that object is close to destination. if so, destroy object 
            if (Vector3.Distance(transform.position, _source.transform.position) < 0.001f) 
            { 
                Destroy(gameObject);
                Instantiate(gameObject, _originalPosition, _originalRotation);
            }
        }
    }
}
