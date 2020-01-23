using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[ExecuteInEditMode]
public class RandomMeshComponent : MonoBehaviour {

    [SerializeField]
    Mesh[] meshes;

    void Update () {
        if (this.transform.hasChanged) {
            GetComponent<MeshFilter> ().mesh = meshes[Random.Range (0, meshes.Length)];
        }
    }
}