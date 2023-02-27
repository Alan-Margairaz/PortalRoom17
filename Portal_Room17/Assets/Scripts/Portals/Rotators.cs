using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotators : MonoBehaviour
{
    private Quaternion rotatorRotation;
    private Quaternion playerRotation;
    public GameObject orangePortal;
    public GameObject bluePortal;
    private PortalScript portalScript;
    bool hasPassed = false;

    private void OnCollisionEnter(Collision collision)
    {
        rotatorRotation = gameObject.transform.localRotation;

        if (collision.gameObject.CompareTag("Player"))
        {
            playerRotation = rotatorRotation;
        }
    }
}
