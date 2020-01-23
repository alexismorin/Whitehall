using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GhostAI : MonoBehaviour
{
    NavMeshAgent agent;

    [SerializeField]
    Transform playerTransform;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        InvokeRepeating("Walk", 0f, 1f);
    }

    void Walk()
    {
        agent.destination = playerTransform.position;
    }
}
