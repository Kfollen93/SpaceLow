using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreviousLevelChecker : MonoBehaviour
{
    public static string PreviousLevel { get; private set; }
    private void OnDestroy()
    {
        PreviousLevel = gameObject.scene.name;
    }
}
