using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeGiver : MonoBehaviour
{
    // Variables pour instantier le Companion Cube au démarrage du niveau:
    public GameObject companionCubePrefab;
    public Transform cubeSpawn;

    // Variable d'ouverture de la porte:
    public GameObject cubeGiverDoor;

    private void Start()
    {
        Instantiate(companionCubePrefab, cubeSpawn.transform.position, Quaternion.identity);
    }


    private void OnTriggerEnter(Collider other)
    {
        Destroy(cubeGiverDoor, 2.5f);
    }
}
