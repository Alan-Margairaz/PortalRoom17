using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalSpawn : MonoBehaviour
{
    // Variables de détection des murs où un portail peut spawn & du type de balle tiré:
    public GameObject orangeBullet;
    public GameObject blueBullet;
    public LayerMask spawnableLayerMask;
    private RaycastHit hit;
    GameObject playerCamera;

    // Variables d'instantiation des portails:
    public GameObject bluePortalPrefab;
    public GameObject orangePortalPrefab;

    private void Start()
    {
        //playerCamera = GameObject.FindGameObjectWithTag("MainCamera");
        playerCamera.GetComponent<Camera>();
    }

    private void OnCollisionEnter(Collision collider)
    {
        // Test de si le mur permet l'apparition d'un portail ou non:
        if (Physics.Raycast(
            playerCamera.transform.position,
            playerCamera.transform.forward,
            out hit, spawnableLayerMask))
        {
            SpawnPortal();
        }
        else    // Si le projectile collide avec autre chose ou le portail ne peut pas spawn, il est détruit:
        {
            Destroy(gameObject);
        }
    }

    private void SpawnPortal()
    {
        // Calcul de la rotation de l'objet sur lequel un portail va apparaître:
        Quaternion hitObjectRotation = Quaternion.LookRotation(hit.normal);

        // On fait apparaître un portail bleu ou orange en fonction du projectile tiré:
        if (gameObject.Equals(blueBullet))
        {
            // Instantiation du portail bleu & destruction du projectile:
            Instantiate(bluePortalPrefab, gameObject.transform.position, hitObjectRotation);
            Destroy(gameObject);
        }

        if (gameObject.Equals(orangeBullet))
        {
            // Instantiation du portail orange & destruction du projectile:
            Instantiate(orangePortalPrefab, gameObject.transform.position, hitObjectRotation);
            Destroy(gameObject);
        }
    }
}
