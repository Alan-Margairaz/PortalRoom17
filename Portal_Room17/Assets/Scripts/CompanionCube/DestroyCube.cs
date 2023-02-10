using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyCube : MonoBehaviour
{
    // Variable d'ouverture de la porte de l'ascenceur de fin par référencement de l'animator:
    [SerializeField] private Animator doorOneAnimator;
    [SerializeField] private Animator doorTwoAnimator;
    bool successfullyDestroyed = false;

    // Référence du cube à détruire:
    //public GameObject companionCube;

    // J'ai choisi OnTriggerStay pour que si jamais le cube glitch ou que la collision est mal faite le cube ne se détruise pas par erreur:
    private void OnTriggerStay(Collider other)
    {
        // Destruction du Companion Cube:
        Destroy(gameObject, 2f);
        successfullyDestroyed = true;

        // Setter du booléen appliqué dans l'animator pour activer l'animation si le cube est bien détruit:
        if (successfullyDestroyed == true)
        {
            doorOneAnimator.SetBool("isCubeDestroyed", true);
            doorTwoAnimator.SetBool("isCubeDestroyed", true);
        }
    }
}
