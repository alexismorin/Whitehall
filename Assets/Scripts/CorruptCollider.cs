using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorruptCollider : MonoBehaviour
{

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Destructible")
        {
            other.gameObject.GetComponent<Rigidbody>().isKinematic = false;
            other.gameObject.GetComponent<Rigidbody>().AddForce(new Vector3(0f, Random.Range(-1.5f, 1.5f), 0f), ForceMode.Impulse);
        }
    }


}
