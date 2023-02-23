using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalLaunch : MonoBehaviour
{
    // Variable de récupération des inputs de tirs:
    private PlayerControls playerControls;

    // Variables de tir :
    [SerializeField] private float bulletSpeed = 10.0f;
    public Transform bulletSpawnPoint;
    public GameObject blueBulletPrefab;
    public GameObject orangeBulletPrefab;


    void Awake()
    {
        playerControls = new PlayerControls();
    }

    void Update()
    {
        // Appels des fonctions de tirs:
        OnLeftClick();
        OnRightClick();
    }

    // Instantiation de la balle bleue:
    private void OnLeftClick()
    {
        if (playerControls.Player.LeftClick.triggered)
        {
            // Instantiation de la balle avec la bonne position & la bonne rotation en utilisant un préfab :
            var bullet = Instantiate(blueBulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);

            // Mise en place de la vélocité de la balle :
            bullet.GetComponent<Rigidbody>().velocity = bulletSpawnPoint.forward * bulletSpeed;
        }
    }

    // Instantiation de la balle orange:
    private void OnRightClick()
    {
        if (playerControls.Player.RightClick.triggered)
        {
            // Instantiation de la balle avec la bonne position & la bonne rotation en utilisant un préfab :
            var bullet = Instantiate(orangeBulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);

            // Mise en place de la vélocité de la balle :
            bullet.GetComponent<Rigidbody>().velocity = bulletSpawnPoint.forward * bulletSpeed;
        }
    }

    // Pour éviter les pertes de mémoires:
    private void OnEnable()
    {
        playerControls.Enable();
    }

    private void OnDisable()
    {
        playerControls.Disable();
    }
}


