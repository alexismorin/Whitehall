using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class RandomHide : MonoBehaviour {

    [MenuItem ("Tools/Random Disable")]
    static void FixFloor () {

        Transform[] selection = Selection.transforms;

        for (int i = 0; i < selection.Length; i++) {
            if (Random.Range (0, 3) == 0) {
                selection[i].gameObject.GetComponent<Rigidbody> ().isKinematic = false;
            } else {
                selection[i].gameObject.GetComponent<Rigidbody> ().isKinematic = true;
            }

        }

    }

}