using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalScript : MonoBehaviour
{
    // Variables de téléportation:
    bool hasPassedPortal = false;
    private CharacterController characterController;

    // Variables de référence des portails:
    public GameObject bluePortal, orangePortal;


    private void Start()
    {
        // Récupération du character controller du joueur pour l'activation/désactivation au moment de la téléportation:
        characterController = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("triggered TP");
        // Check des objets qui peuvent passer le portail (le joueur, le cube ou les boules d'énergie):
        if(other.CompareTag("Player") || other.CompareTag("CompanionCube") || other.CompareTag("EnergyPellet"))
        {
            // Désactivation du character controller car il empêche la téléportation:
            characterController.enabled = false;
            if (hasPassedPortal == false)
            {
                // Cas de si le joueur entre en trigger avec un portail bleu:
                if (gameObject.Equals(bluePortal))
                {
                    // Destination du joueur à travers le portail bleu:
                    Vector3 blueDestinationPos = GameObject.FindGameObjectWithTag("OrangePortal").transform.position + (2 * GameObject.FindGameObjectWithTag("OrangePortal").transform.forward);

                    // Téléportation du joueur à la position du 2nd portail:
                    other.gameObject.transform.position = blueDestinationPos;

                    // Reset du booléen pour avoir un suivi de si oui ou non la téléportation a été faite & réactivation du character controller à la sortie du portail:
                    hasPassedPortal = true;
                    characterController.enabled = true;
                }
                // Même processus dans le cas du portail orange:
                if (gameObject.Equals(orangePortal))
                {
                    Vector3 orangeDestinationPos = GameObject.FindGameObjectWithTag("BluePortal").transform.position + (2 * GameObject.FindGameObjectWithTag("BluePortal").transform.forward);
                    other.gameObject.transform.position = orangeDestinationPos;
                    hasPassedPortal = true;
                    characterController.enabled = true;
                }
            }
            else
            {
                // Réactivation du character controller lors de la sortie du portail:
                characterController.enabled = true;
            }
        }
    }
} 


