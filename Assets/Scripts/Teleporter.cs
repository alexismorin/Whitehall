using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Teleporter : MonoBehaviour {

    public Transform ghost;

    void OnTriggerEnter (Collider other) {
        if (other.gameObject.tag == "Player") {
            ghost.gameObject.GetComponent<NavMeshAgent> ().enabled = false;
            ghost.gameObject.GetComponent<NavMeshAgent> ().nextPosition = transform.parent.position + Vector3.up * 2f;
            ghost.transform.position = transform.parent.position;
            ghost.gameObject.GetComponent<NavMeshAgent> ().enabled = true;

        }

    }
}