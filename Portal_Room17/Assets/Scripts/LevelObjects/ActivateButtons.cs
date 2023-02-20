using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateButtons : MonoBehaviour
{
    // Références de l'animator pour les animations du gros bouton et des portes :
    [SerializeField] private Animator buttonPressedAnimator;
    [SerializeField] private Animator roomDoor1Animator;
    [SerializeField] private Animator roomDoor2Animator;


    private void OnTriggerStay(Collider other)
    { 
        // Activation de l'animation du bouton :
        if(other.CompareTag("CompanionCube"))
        {
            buttonPressedAnimator.SetBool("isCubePlaced", true);
            roomDoor1Animator.SetBool("isCubePlaced", true);
            roomDoor2Animator.SetBool("isCubePlaced", true);
        }
    }
}
