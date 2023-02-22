using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyableCameras : MonoBehaviour
{
    private Rigidbody rb;

    [SerializeField] private float fallForce = 10f;

    private void Awake()
    {
        // Récupération du rigidbody présent sur les caméras:
        rb = GetComponent<Rigidbody>();
    }

    // Lorsqu'il y a une collision avec une balle tirée (par le joueur seulement),
    // on applique la gravité sur la caméra avec une force vers le bas: 
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bullet"))
        {
            // Application de la gravité du rigidbody:
            rb.useGravity = true;
            rb.isKinematic = false;

            // Application d'une force vers le bas:
            rb.AddForce(Vector3.down * fallForce);
        }
    }
}
