using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateButtons : MonoBehaviour
{
    // Références de l'animator pour les animations du gros bouton et des portes :
    [SerializeField] private Animator buttonPressedAnimator;
    [SerializeField] private Animator roomDoorOneAnimator;
    [SerializeField] private Animator roomDoorTwoAnimator;


    private void OnTriggerStay(Collider other)
    { 
        // Activation de l'animation du bouton :
        if(other.CompareTag("CompanionCube"))
        {
            buttonPressedAnimator.SetBool("isCubePlaced", true);
            roomDoorOneAnimator.SetBool("isButtonPressed", true);
            roomDoorTwoAnimator.SetBool("isButtonPressed", true);
        } else if (other.tag != "CompanionCube")
        {
            roomDoorOneAnimator.SetBool("isButtonPressed", false);
            roomDoorTwoAnimator.SetBool("isButtonPressed", false);
        }
    }
}
