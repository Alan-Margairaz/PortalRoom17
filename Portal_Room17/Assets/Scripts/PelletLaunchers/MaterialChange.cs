using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialChange : MonoBehaviour
{
    // Variables de changement de matériel lorsque le PelletCatcher attrape une boule d'énergie:
    [SerializeField] private Renderer materialChange;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("EnergyPellet"))
        {
            materialChange.material.color = Color.cyan;
        }
    }
    
}
