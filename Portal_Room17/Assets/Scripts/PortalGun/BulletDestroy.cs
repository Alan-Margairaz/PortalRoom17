using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletDestroy : MonoBehaviour
{
    public GameObject portalPrefab;

    private void OnCollisionEnter(Collision collision)
    {
        Instantiate(portalPrefab, collision.transform.position, Quaternion.LookRotation(Vector3.forward));
        Destroy(gameObject);
    }
}
