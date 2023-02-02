using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PelletLaunch : MonoBehaviour
{
    // Variables d'instantiation des boules d'énergie:
    [SerializeField][Range(1, 15)] private float energyPelletSpeed = 20.0f;
    public GameObject energyPelletPrefab;
    public Transform energyPelletSpawnPoint;

    // Variables de délai de spawn des EnergyPellet:
    int maxPelletNumber = 10;
    //int currentPelletNumber = 0;
    [SerializeField] private float instantiationDelay = 7.0f;


    // Fonction pour que le launcher fonctionne seulement aux endroits d'intérêt:
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "Player")
        {
            // Démarrage de la co-routine seulement après détection du joueur:
            StartCoroutine(InstantiatePellets());
        }
    }

    // Coroutine pour une instantiation des EnergyPellet avec un délai:
    IEnumerator InstantiatePellets()
    {
        // Boucle while pour contrôler le nombre maximum de spawn d'EnergyPellet:
        int i = 0;
        while (i < maxPelletNumber)
        {
            var energyPellet = Instantiate(energyPelletPrefab, energyPelletSpawnPoint.position, energyPelletSpawnPoint.rotation);
            energyPellet.GetComponent<Rigidbody>().velocity = energyPelletSpawnPoint.forward * energyPelletSpeed;
            i++;
            yield return new WaitForSeconds(instantiationDelay);
        }
    }
}
