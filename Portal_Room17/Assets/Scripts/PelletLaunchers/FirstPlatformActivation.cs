using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPlatformActivation : MonoBehaviour
{
    // Références au script de plateformes mouvantes pour activation :
    public GameObject movablePlatforms;

    private void Awake()
    {
        movablePlatforms.GetComponent<MovablePlatforms>().enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("EnergyPellet"))
        {
            movablePlatforms.GetComponent<MovablePlatforms>();
            movablePlatforms.GetComponent<MovablePlatforms>().enabled = true;
        }
    }
}
