using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.HID;
using UnityEngine.InputSystem;
using System;

public class PickUp : MonoBehaviour
{
    // Variables pour la détection du Companion Cube:
    [SerializeField][Range(1, 10)] private float pickUpRange = 4.0f;
    public LayerMask pickableLayerMask;
    public Transform playerCameraTransform;
    private RaycastHit hit;

    // Variables pour le pick du Companion Cube :
    private PlayerControls playerControls;
    private GameObject cubeInHand;
    public Transform pickUpTransform;
    private Rigidbody rb;
    private float pickUpForce = 100.0f;


    private void Awake()
    {
        playerControls = new PlayerControls();
    }

    void Update()
    {
        OnPick();
        OnDrop();
        //CubeMove();
    }

    private void OnPick()
    {
        // On vérifie si la collision se fait grâce à un raycast :
        if (Physics.Raycast(
            playerCameraTransform.position,                         // Position de lancement du raycast
            playerCameraTransform.forward,                          // Direction du raycast
            out hit, pickUpRange,                                   // Sortie du hit du raycast + portée maximum
            pickableLayerMask))                                     // On rajoute le layer pour ne pouvoir hit que les objets avec le layer 'Pickable'
        {
            // On récupère le rigidbody du Companion Cube
            rb = hit.collider.GetComponent<Rigidbody>();            

            // On prend le cube lorsque l'on appuie sur 'E':
            if (playerControls.Player.Pick.triggered && rb != null)
            {
                // On parente le cube à un transform préalablement placé devant le joueur pour récupérer son transform:
                cubeInHand = hit.collider.gameObject;
                cubeInHand.transform.SetParent(pickUpTransform.transform, true);

                // On modifie certaines valeurs du rigidbody du cube pour avoir un meilleur game feel:
                rb.drag = 10;
                rb.useGravity = false;
                rb.angularDrag = 1f;
                rb.constraints = RigidbodyConstraints.FreezeRotation;
            }
            else if (Vector3.Distance(cubeInHand.transform.position, pickUpTransform.position) > 0.2f)      // PROBLÈME À RÉGLER ICI, NE S'INITIALISE PAS (NULL REFERENCE)
            {
                Vector3 moveDirection = (pickUpTransform.position - cubeInHand.transform.position);
                rb.AddForce(moveDirection * pickUpForce);
            }
        }
    }

    private void OnDrop()
    {
        // On enlève le transform du parent appliqué au cube pour le faire tomber & reset du rb à son état normal
        if (playerControls.Player.Drop.triggered)
        {
            rb.useGravity = true;
            rb.drag = 1;
            rb.angularDrag = 0.05f;
            rb.constraints = RigidbodyConstraints.None;

            cubeInHand.transform.SetParent(null);
        }
    }

    //private void CubeMove()
    //{
    //    // Mouvements du cube lorsqu'on l'a dans la main:
    //    if (Vector3.Distance(cubeInHand.transform.position, pickUpTransform.position) > 0.1f)
    //    {
    //        Vector3 moveDirection = (pickUpTransform.position - cubeInHand.transform.position);
    //        rb.AddForce(moveDirection * pickUpForce);
    //    }   
    //}

    // Pour éviter les pertes de mémoire:
    private void OnEnable()
    {
        playerControls.Enable();
    }

    private void OnDisable()
    {
        playerControls.Disable();
    }
}
