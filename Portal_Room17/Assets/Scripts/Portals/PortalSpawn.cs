using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.HID;

public class PortalSpawn : MonoBehaviour
{
    // Variable de la caméra pour la détection des surfaces où un portail peut spawn:
    GameObject playerCamera;

    // Variables d'instantiation des portails en fonction des balles tirées:
    public GameObject bluePortalPrefab;
    public GameObject orangePortalPrefab;

    public GameObject orangeBullet;
    public GameObject blueBullet;

    // Variables pour n'avoir qu'un seul portail de chaque couleur sur la map:
    //private bool usedOnce = false;
    public List<GameObject> orangePortalsList;
    public List<GameObject> bluePortalsList;
    GameObject orangePortalToDestroy = null;
    GameObject bluePortalToDestroy = null;

    private void Start()
    {
        //Récupération de la caméra du joueur pour le Raycast:
        playerCamera = GameObject.FindGameObjectWithTag("MainCamera");
    }

    private void Update()
    {

    }

    // Apparition du portail lorsque la balle collide avec une surface permettant le spawn d'un portail:
    private void OnCollisionEnter(Collision collider)
    {
        // Coordonnées X et Y par rapport à l'écran afin de mettre le Raycast au centre de l'écran:
        int x = Screen.width / 2;
        int y = Screen.height / 2;

        // Création du Raycast en partant de la caméra du joueur et au centre de l'écran:
        RaycastHit hit;
        Ray ray = playerCamera.GetComponent<Camera>().ScreenPointToRay(new Vector3(x, y));
        
        // On check si le Raycast touche quelque chose:
        if (Physics.Raycast(ray, out hit))
        {
            // Calcul de la rotation de la surface sur laquelle va spawn le portail grâce à sa normale:
            Quaternion hitObjectRotation = Quaternion.LookRotation(hit.normal);

            // Test de si le mur permet l'apparition d'un portail ou non ainsi que du type de balle tiré:
            if (collider.collider.CompareTag("Spawnable") && gameObject.Equals(blueBullet))
            {
                // Conditions pour s'il y a un autre portail déjà dans le jeu, on le détruit et en instantie un nouveau:
                if (GameObject.FindGameObjectWithTag("BluePortal") == null)
                {
                    // Instantiation du portail bleu au point ou la balle touche et avec la rotation de la surface touchée:
                    Instantiate(bluePortalPrefab, hit.point + new Vector3(0.1f, 0, 0), hitObjectRotation);
                }
                else
                {
                    // Phase de remplacement du portail existant en un nouveau portail:
                    Destroy(GameObject.FindGameObjectWithTag("BluePortal"));
                    Instantiate(bluePortalPrefab, hit.point + new Vector3(0.1f, 0, 0), hitObjectRotation);
                }
                // Destruction de la balle après l'instantiation
                Destroy(blueBullet);
            }
            // Même processus et test pour le portail orange:
            else if (collider.collider.CompareTag("Spawnable") && gameObject.Equals(orangeBullet))
            {
                if (GameObject.FindGameObjectWithTag("OrangePortal") == null)
                {
                    // Instantiation du portail bleu au point ou la balle touche et avec la rotation de la surface touchée:
                    Instantiate(orangePortalPrefab, hit.point + new Vector3(0.1f, 0, 0), hitObjectRotation);
                }
                else
                {
                    Destroy(GameObject.FindGameObjectWithTag("OrangePortal"));
                    Instantiate(orangePortalPrefab, hit.point + new Vector3(0.1f, 0, 0), hitObjectRotation);
                }
                Destroy(orangeBullet);
            }
            // Si le projectile collide avec une surface non spawnable, il est détruit:
            else
            {
                Destroy(gameObject);
            }
        }
    }
}
