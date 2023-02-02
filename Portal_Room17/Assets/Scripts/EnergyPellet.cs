using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyPellet : MonoBehaviour
{
    int reboundCount = 0;

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
}
