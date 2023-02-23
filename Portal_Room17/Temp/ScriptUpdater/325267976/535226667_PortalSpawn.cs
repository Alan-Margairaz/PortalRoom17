using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.HID;

public class PortalSpawn : MonoBehaviour
{
    // Variables de détection des murs où un portail peut spawn & du type de balle tiré:
    public GameObject orangeBullet;
    public GameObject blueBullet;
    //public LayerMask spawnableLayerMask;
    private RaycastHit hit;
    //GameObject playerCamera;
    GameObject playerCamera;

    // Variables d'instantiation des portails:
    public GameObject bluePortalPrefab;
    public GameObject orangePortalPrefab;


    private void Start()
    {
        //Récupération de la caméra du joueur pour le Raycast:
        playerCamera = GameObject.FindGameObjectWithTag("MainCamera");

    }

    private void OnCollisionEnter(Collision collider)
    {
        int x = Screen.width / 2;
        int y = Screen.height / 2;

        Ray ray = GetComponent<Camera>().ScreenPointToRay(new Vector3(x, y));
        Quaternion hitObjectRotation = Quaternion.LookRotation(hit.normal);

        if (Physics.Raycast(ray, out hit))
        {
            // Test de si le mur permet l'apparition d'un portail ou non grâce à un Raycast:
            if (collider.collider.CompareTag("Spawnable") && gameObject.Equals(blueBullet))
            {
                // Calcul de la rotation de l'objet sur lequel un portail va apparaître grâce à la normale de l'objet 'obstacle':
                //Quaternion hitObjectRotation = Quaternion.LookRotation(hit.normal);
                //SpawnPortal();

                Instantiate(bluePortalPrefab, hit.point, hitObjectRotation);
                Destroy(blueBullet);
            } else if (collider.collider.CompareTag("Spawnable") && gameObject.Equals(orangeBullet))
            {
                Instantiate(orangePortalPrefab, hit.point, hitObjectRotation);
                Destroy(orangeBullet);
            }
            else
            {
                // Si le projectile collide avec autre chose ou le portail ne peut pas spawn, il est détruit:
                Destroy(gameObject);
            }
        }
    }

    //void SpawnPortal()
    //{
        
    //        // On fait apparaître un portail bleu ou orange en fonction du projectile tiré:
    //        if (gameObject.Equals(blueBullet))
    //        {
    //            // Instantiation du portail bleu & destruction du projectile:
    //            Instantiate(bluePortalPrefab, hit.point, hitObjectRotation);
    //            Destroy(blueBullet);
    //        }

    //        if (gameObject.Equals(orangeBullet))
    //        {
    //            // Instantiation du portail orange & destruction du projectile:
    //            Instantiate(orangePortalPrefab, hit.point, hitObjectRotation);
    //            Destroy(orangeBullet);
    //        }
    //    }
    //}
}

//if (Physics.Raycast(
//            playerCamera.transform.position,
//            playerCamera.transform.forward,
//            out hit, spawnableLayerMask))
//{
//    if (collider.collider.CompareTag("Spawnable"))
//    {
//        // Calcul de la rotation de l'objet sur lequel un portail va apparaître grâce à la normale de l'objet 'obstacle':
//        Quaternion hitObjectRotation = Quaternion.LookRotation(hit.normal);
//        SpawnPortal(hitObjectRotation);
//    }
//    else
//    {
//        // Si le projectile collide avec autre chose ou le portail ne peut pas spawn, il est détruit:
//        Destroy(gameObject);
//    }
//}
