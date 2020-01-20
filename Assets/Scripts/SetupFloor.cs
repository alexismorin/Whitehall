using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class SetupFloor : MonoBehaviour {

    [MenuItem ("Tools/Setup Selected Floors")]
    static void FixFloor () {

        GameObject lightingFolder;

        if (GameObject.Find ("Lighting") != null) {
            lightingFolder = GameObject.Find ("Lighting");
        } else {
            lightingFolder = new GameObject ();
            lightingFolder.transform.localPosition = Vector3.zero;
            lightingFolder.transform.localEulerAngles = Vector3.zero;
            lightingFolder.name = "Lighting";
        }

        Transform[] selectedFloorPieces = Selection.transforms;

        // Light Probe

        GameObject lightProbeObject = new GameObject ();
        lightProbeObject.transform.parent = lightingFolder.transform;
        lightProbeObject.transform.localPosition = Vector3.zero;
        lightProbeObject.transform.localEulerAngles = Vector3.zero;
        lightProbeObject.name = "Light Probe Group";

        lightProbeObject.AddComponent<LightProbeGroup> ();

        LightProbeGroup probes = lightProbeObject.GetComponent<LightProbeGroup> ();
        Vector3[] tempProbePositions = new Vector3[selectedFloorPieces.Length];

        // Reflection Probe

        GameObject reflectionProbeObject = new GameObject ();
        reflectionProbeObject.transform.parent = lightingFolder.transform;
        reflectionProbeObject.transform.localPosition = Vector3.zero;
        reflectionProbeObject.transform.localEulerAngles = Vector3.zero;
        reflectionProbeObject.name = "Reflection Probe";
        reflectionProbeObject.AddComponent<ReflectionProbe> ();

        ReflectionProbe reflectionProbe = reflectionProbeObject.GetComponent<ReflectionProbe> ();
        reflectionProbe.boxProjection = true;

        Bounds reflectionBounds = new Bounds (selectedFloorPieces[0].transform.position, Vector3.zero);

        for (int i = 0; i < selectedFloorPieces.Length; i++) {

            // If this is actually a floor piece
            if (selectedFloorPieces[i].gameObject.GetComponent<MeshRenderer> () != null) {
                tempProbePositions[i] = selectedFloorPieces[i].gameObject.GetComponent<MeshRenderer> ().bounds.center + (Vector3.up * Random.Range (0.5f, 2f));

                reflectionBounds.Encapsulate (selectedFloorPieces[i].gameObject.GetComponent<MeshRenderer> ().bounds);
            }

        }

        reflectionProbeObject.transform.position = reflectionBounds.center + (Vector3.up * 2f);

        //   reflectionProbe.center = reflectionBounds.center + (Vector3.up * 2f);
        reflectionProbe.size = reflectionBounds.size + (Vector3.up * 4f) + new Vector3 (0.5f, 0.5f, 0.5f);

        probes.probePositions = tempProbePositions;

    }

}