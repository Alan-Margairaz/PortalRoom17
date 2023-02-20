using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyCube : MonoBehaviour
{
    // Variable d'ouverture de la porte de l'ascenceur de fin par référencement de l'animator:
    [SerializeField] private Animator doorOneAnimator;
    [SerializeField] private Animator doorTwoAnimator;

    // Référence du cube à détruire:
    public GameObject companionCube;

    // J'ai choisi OnTriggerStay pour que si jamais le cube glitch ou que la collision est mal faite le cube ne se détruise pas par erreur:
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("CompanionCube"))
        {
            // Destruction du Companion Cube:
            Destroy(companionCube, 2f);

            // Setter du booléen appliqué dans l'animator pour activer les animations seulement si le cube est détruit:
            doorOneAnimator.SetBool("isCubeDestroyed", true);
            doorTwoAnimator.SetBool("isCubeDestroyed", true);
        }
    }
}
