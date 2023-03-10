using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovablePlatforms : MonoBehaviour
{
    // Référence au script qui nous permet d'avoir l'index des waypoints:
    [SerializeField] private WaypointPath waypointPath;

    // Variable pour le déplacement de la plateforme:
    [SerializeField] private float speed;

    // Variables pour connaître et attribuer la bonne position à la plateforme pour qu'elle se déplace:
    private int targetWaypointIndex;
    private Transform previousWaypoint;
    private Transform targetWaypoint;

    // Variables pour gérer le timing voulu entre deux waypoints:
    private float timeToWaypoint;
    private float elapsedTime;


    void Start()
    {
        TargetNextWaypoint();
    }

    void FixedUpdate()
    {
        // Mouvement entre les deux waypoints:
        elapsedTime += Time.deltaTime;

        // Calcul du pourcentage de complétion du mouvement de la plateforme:
        float elapsedPercentage = elapsedTime / timeToWaypoint;

        // On lerp pour pouvoir avoir un mouvement fluide entre les deux points grâce au temps que mets la plateforme entre les 2 waypoints:
        transform.position = Vector3.Lerp(previousWaypoint.position, targetWaypoint.position, elapsedPercentage);
    }

    public void TargetNextWaypoint()
    {
        // Initialisation de la 'cible' que la plateforme doit avoir en fonction de l'index:
        previousWaypoint = waypointPath.GetWaypoint(targetWaypointIndex);

        // Initialisation de l'index de la 'cible' au prochain dans le chemin de la plateforme: 
        targetWaypointIndex = waypointPath.GetNextWaypoint(targetWaypointIndex);

        // Initialisation de l'index cible à la nouvel index cible:
        targetWaypoint = waypointPath.GetWaypoint(targetWaypointIndex);

        elapsedTime = 0;

        // Calcul de la distance entre les 2 points pour calculer le temps pris par le Lerp pour arriver du point A à B:
        float distanceToWaypoint = Vector3.Distance(previousWaypoint.position, targetWaypoint.position);

        //Produit en croix pour calculer le temps, car on a la vitesse et la distance entre les deux points:
        timeToWaypoint = distanceToWaypoint / speed;
    }


    // Rajout de cette fonction pour l'ascenceur de fin, que le joueur ne glitch pas à travers le sol qui bouge:
    private void OnTriggerEnter(Collider other)
    {
        other.transform.SetParent(transform);
        TargetNextWaypoint();
    }
}
