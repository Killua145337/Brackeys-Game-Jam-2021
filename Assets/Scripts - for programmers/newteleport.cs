using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class newteleport : MonoBehaviour
{
    // Start is called before the first frame update

public Transform teleportTarget;
public GameObject thePlayer;

void OnTriggerEnter(Collider other)
{
    thePlayer.transform.position = teleportTarget.transform.position;
}

}
