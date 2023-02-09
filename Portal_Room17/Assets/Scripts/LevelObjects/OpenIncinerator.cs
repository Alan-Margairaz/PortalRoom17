using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenIncinerator : MonoBehaviour
{
    public void DestroyIncineratorDoor()
    {
        Destroy(gameObject, 2f);
    }
}
