using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    //Variable pour récupérer les inputs :
    private PlayerControls playerControls;

    // Variable pour rotate le joueur lorsque la caméra change de position :
    public Transform playerTransform;      

    // Variables pour calculer le suivi de caméra par rapport au joueur :
    [SerializeField][Range(50,300)] private float mouseSensitivity;
    private float xRotation;
    private Vector2 mouseLook;


    private void Awake()
    {
        playerControls = new PlayerControls();
        Cursor.lockState = CursorLockMode.Locked;               // Permet le blocage du curseur au centre de l'écran en jeu
    }

    private void Update()
    {
        OnLook();
    }

    private void OnLook()
    {
        // On récupère l'input de déplacement de la souris :
        mouseLook = playerControls.Player.Look.ReadValue<Vector2>();        

        // Mouvement sur les axes X et Y, indépendant du framerate : 
        float xAxis = mouseLook.x * mouseSensitivity * Time.deltaTime;
        float yAxis = mouseLook.y * mouseSensitivity * Time.deltaTime;

        // Limitation de la FOV en -75 / 75 :
        xRotation -= yAxis;
        xRotation = Mathf.Clamp(xRotation, -75.0f, 75.0f);

        // Rotation du joueur avec la caméra :
        transform.localRotation = Quaternion.Euler(xRotation, 0, 0);
        playerTransform.Rotate(Vector3.up * xAxis);
    }


    // On évite les pertes de mémoire en activant et désactivant la récupération des controls : 
    private void OnEnable()
    {
        playerControls.Enable();
    }

    private void OnDisable()
    {
        playerControls.Disable();
    }

}