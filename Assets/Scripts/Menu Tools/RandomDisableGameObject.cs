using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class RandomDisableGameObject : MonoBehaviour
{

    [MenuItem("Tools/RandomDisableGameObject")]
    static void FixFloor()
    {

        Transform[] selection = Selection.transforms;

        for (int i = 0; i < selection.Length; i++)
        {
            if (Random.Range(0, 3) == 0)
            {
                selection[i].gameObject.SetActive(false);
            }
            else
            {
                selection[i].gameObject.SetActive(true);
            }

        }

    }

}