using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateButtons : MonoBehaviour
{
    // Références de l'animator pour les animations du gros bouton et des portes :
    [SerializeField] private Animator buttonPressedAnimator;
    [SerializeField] private Animator roomDoorOneAnimator;
    [SerializeField] private Animator roomDoorTwoAnimator;


    private void OnTriggerEnter(Collider other)
    {
        // Activation de l'animation du bouton et des portes si le Companion Cube est posé sur le bouton:
        if(other.CompareTag("CompanionCube"))
        {
            buttonPressedAnimator.SetBool("isCubePlaced", true);
            buttonPressedAnimator.SetBool("open", true);           
        } else if (other.tag != "CompanionCube")
        {
            // Désactivation des animations des portes si il y a autre chose que le Companion Cube sur le bouton:
            buttonPressedAnimator.SetBool("close", true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Désactivation de l'animation du bouton si rien n'est placé dessus:
        buttonPressedAnimator.SetBool("isCubePlaced", false);
    }
}
