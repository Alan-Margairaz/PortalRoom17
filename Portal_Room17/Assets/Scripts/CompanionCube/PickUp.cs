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

    // Variables pour le pick & drop du Companion Cube :
    private PlayerControls playerControls;
    private GameObject cubeInHand;
    public Transform pickUpTransform;
    private Rigidbody rb;
    //private float pickUpForce = 100.0f;

    // Variables pour les intéractions:
    public LayerMask interactLayerMask;
    private OpenIncinerator openIncinerator;


    private void Awake()
    {
        playerControls = new PlayerControls();
        openIncinerator = GameObject.FindGameObjectWithTag("Destroyable").GetComponent<OpenIncinerator>();
    }

    void Update()
    {
        OnPick();
        OnDrop();
        OnInteract();
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
            if (playerControls.Player.Interact.triggered && rb != null)
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
        }
    }

    private void OnDrop()
    {
        // On enlève le transform du parent appliqué au cube pour le faire tomber & reset du rb à son état normal
        if (playerControls.Player.Drop.triggered & rb != null)
        {
            rb.useGravity = true;
            rb.drag = 1;
            rb.angularDrag = 0.05f;
            rb.constraints = RigidbodyConstraints.None;

            cubeInHand.transform.SetParent(null);
        }
    }

    private void OnInteract()
    {
        if(Physics.Raycast(
            playerCameraTransform.position,
            playerCameraTransform.forward,
            out hit, pickUpRange,
            interactLayerMask))
        {
            if (playerControls.Player.Interact.triggered)
            {
                openIncinerator.DestroyIncineratorDoor();
            }
        }
    }

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
