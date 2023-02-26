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
    public LayerMask pickableMask;
    private bool isGrounded;
    private bool isOnCube;


    private void Awake()
    {
        playerControls = new PlayerControls();
        characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        // Récupération des inputs mouvement du joueur en temps réel :
        Vector2 playerMove = playerControls.Player.Move.ReadValue<Vector2>();

        // Appel des fonctions pour éviter de surpeupler la fonction Update, puis permet une meilleure organisation :
        Gravity();
        OnMove();
        OnJump();
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
        if ( (playerControls.Player.Jump.triggered && isGrounded) || (playerControls.Player.Jump.triggered && isOnCube) )
        {
            playerVelocity.y = Mathf.Sqrt(jumpHeight * -0.75f * gravity);      // Saut
        }
    }

    private void Gravity()
    {
        // On check si le joueur touche le sol ou non en créeant une sphère sous les pieds du joueur, si la sphère touche le sol le joueur touche le sol :
        isGrounded = Physics.CheckSphere(groundCheck.position, distanceToGround, groundMask);

        // Cas de si le joueur est sur le cube:
        isOnCube = Physics.CheckSphere(groundCheck.position, distanceToGround, pickableMask);

        if( (isGrounded && playerVelocity.y < 0) || (isOnCube && playerVelocity.y < 0) )
        {
            playerVelocity.y = -2.0f;       // Permet un saut plus fluide
        }

        // Mise en place de la gravité :
        playerVelocity.y += gravity * Time.deltaTime;
        characterController.Move(playerVelocity * Time.deltaTime);
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
