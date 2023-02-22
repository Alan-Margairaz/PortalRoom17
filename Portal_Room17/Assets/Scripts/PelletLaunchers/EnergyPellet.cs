using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyPellet : MonoBehaviour
{
    // Variables de contrôle de rebonds:
    int reboundCount = 0;

    // Grâce au Physics Material, on peut contrôler le nombre de rebonds voulu des boules d'énergie:
    private void OnCollisionEnter(Collision collision)
    {
        if (reboundCount <= 6)
        {
            reboundCount++;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Les triggers placés sur les 2 éléments de l'EnergyPelletCatcher permettent de détruire les boules d'énergie: 
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "PelletCatcher")
        {
            Destroy(gameObject);
        }
    }
}
