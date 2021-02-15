using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTrigger : MonoBehaviour
{
    [SerializeField] GameObject door;
    [SerializeField] float doorPositionChange;

    private Vector3 startingPos;


    private void Start() {
        startingPos = door.transform.position;
    }
    private void OnTriggerStay(Collider other) {
        door.transform.position += new Vector3(0f, doorPositionChange, 0f) * Time.deltaTime;
        GetComponent<Animator>().SetBool("triggered", true);
    }

    private void OnTriggerExit(Collider other) {
        door.transform.position = startingPos;
        GetComponent<Animator>().SetBool("triggered", false);
    }
}
