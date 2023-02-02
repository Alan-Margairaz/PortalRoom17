using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    // Variable récupération d'inputs de mouvement :
    private PlayerControls playerControls;
    private CharacterController characterController;
    private Vector2 playerMove;

    // Variables de déplacement du joueur :
    [SerializeField][Range(1, 15)] private float playerMoveSpeed = 6.0f;
    private Vector3 playerVelocity;

    // Variables de saut :
    private float jumpHeight = 2f;
    private float gravity = -9.81f;
    public float distanceToGround = 0.4f;
    public Transform groundCheck;
    public LayerMask groundMask;
    private bool isGrounded;

    // Variables de tir :
    [SerializeField][Range(1, 15)] private float bulletSpeed = 10.0f;
    public Transform bulletSpawnPoint;
    public GameObject bulletPrefab;

    // Variables pour le pick du Companion Cube :
    [SerializeField][Range(1, 20)] private float pickupRange;
    public LayerMask pickableLayerMask;
    public Transform playerCameraTransform;
    public GameObject pickupUI;
    private RaycastHit hit;


    private void Awake()
    {
        playerControls = new PlayerControls();
        characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        // Récupération des inputs mouvement du joueur en temps réel :
        Vector2 playerMove = playerControls.Player.Move.ReadValue<Vector2>();

        // Appel des fonctions pour éviter de sur peupler la fonction Update, puis permet une meilleure organisation :
        Gravity();
        OnMove();
        OnJump();
        OnFire();
        OnPick();
    }

    private void OnMove()
    {
        // Récupération des inputs ZQSD :
        playerMove = playerControls.Player.Move.ReadValue<Vector2>();

        // Mouvement du joueur :
        Vector3 movement = (playerMove.y * transform.forward) + (playerMove.x * transform.right);
        characterController.Move(movement * playerMoveSpeed * Time.deltaTime);
    }

    private void OnJump()
    {
        // Check de si le joueur appuie sur espace ET de s'il touche le sol :
        if (playerControls.Player.Jump.triggered && isGrounded)
        {
            playerVelocity.y = Mathf.Sqrt(jumpHeight * -0.75f * gravity);      // Saut
        }
    }

    private void Gravity()
    {
        // On check si le joueur touche le sol ou non en créeant une sphère sous les pieds du joueur, si la sphère touche le sol le joueur touche le sol :
        isGrounded = Physics.CheckSphere(groundCheck.position, distanceToGround, groundMask);

        if(isGrounded && playerVelocity.y < 0)
        {
            playerVelocity.y = -2.0f;       // Permet une fin de saut plus smooth
        }

        // Mise en place de la gravité :
        playerVelocity.y += gravity * Time.deltaTime;
        characterController.Move(playerVelocity * Time.deltaTime);
    }

    private void OnFire()
    {
        if (playerControls.Player.Fire.triggered)
        {
            // Instantiation de la balle avec la bonne position & la bonne rotation en utilisant un préfab :
            var bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);   
            // Mise en place de la vélocité de la balle :
            bullet.GetComponent<Rigidbody>().velocity = bulletSpawnPoint.forward * bulletSpeed;             
        }
    }

    private void OnPick()
    {
        Debug.DrawRay(playerCameraTransform.position, playerCameraTransform.forward * pickupRange, Color.red);
        if (hit.collider != null)
        {
            pickupUI.SetActive(false);
        }

        // On vérifie si la collision se fait grâce à plusieurs paramètres du Raycast :
        if (Physics.Raycast(
            playerCameraTransform.position,             // Position de lancement du raycast
            playerCameraTransform.forward,              // Direction du raycast
            out hit, pickupRange,                       // Sortie du hit du raycast + portée maximum
            pickableLayerMask))                         // On rajoute le layer pour ne pouvoir hit que les objets avec le layer 'Pickable'
        {
            pickupUI.SetActive(true);
        }
    }


    // Pour éviter les pertes de mémoires il faut activer et désactiver l'écoute des inputs que lorsqu'on en a besoin : 
    private void OnEnable()
    {
        playerControls.Enable();
    }

    private void OnDisable()
    {
        playerControls.Disable();
    }
}
