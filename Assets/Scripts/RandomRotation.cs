using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class RandomRotation : MonoBehaviour {
    [MenuItem ("Tools/Random Rotation")]
    static void FixFloor () {

        Transform[] selection = Selection.transforms;

        for (int i = 0; i < selection.Length; i++) {
            selection[i].rotation = Random.rotation;
        }

    }
}