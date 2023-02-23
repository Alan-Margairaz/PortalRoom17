using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalScript : MonoBehaviour
{
    // Variables de TP du joueur/cube:
    bool hasPassedPortal = false;
    private CharacterController characterController;

    // Variables de référence des portails:
    public GameObject bluePortal, orangePortal;
    Vector3 orangePortalPos, bluePortalPos;
    

    private void Start()
    {
        characterController = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterController>();
    }

    private void Update()
    {
        // Calcul de la position du portail orange:
        orangePortalPos = orangePortal.transform.position +
                          orangePortal.transform.right * 2;

        // Calcul de la position du portail bleu:
        bluePortalPos = bluePortal.transform.position +
                        bluePortal.transform.right * 2;
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check des objets qui peuvent passer le portail (le joueur, le cube ou les boules d'énergie):
        if(other.CompareTag("Player") || other.CompareTag("CompanionCube") || other.CompareTag("EnergyPellet"))
        {
            if (hasPassedPortal == false)
            {
                // Désactivation du character controller car il empêche la téléportation:
                characterController.enabled = false;

                if (gameObject.Equals(bluePortal))
                {
                    // Téléportation du joueur à la position (un peu devant) du 2nd portail:
                    other.gameObject.transform.position = orangePortalPos;

                    // Reset du booléen pour savoir si oui ou non la téléportation a été faite & réactivation du character controller:
                    hasPassedPortal = true;
                    characterController.enabled = true;
                }

                if (gameObject.Equals(orangePortal))
                {
                    // Téléportation du joueur à la position (un peu devant) du 2nd portail:
                    other.gameObject.transform.position = bluePortalPos;

                    // Reset du booléen pour savoir si oui ou non la téléportation a été faite & réactivation du character controller:
                    hasPassedPortal = true;
                    characterController.enabled = true;
                }
            }
            else
            {
                // Réactivation du character controller lors de la sortie du portail:
                characterController.enabled = true;
                Debug.Log(hasPassedPortal);
                Debug.Log("Has NOT TP");
            }
        }
    }
}
