using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyCube : MonoBehaviour
{
    public GameObject companionCube;

    private void OnTriggerStay(Collider other)
    {
        Destroy(companionCube, 1);
    }
}
