using NavMeshPlus.Components;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;
using UnityEngine.AI;

public class Follow_AI : MonoBehaviour
{
    [SerializeField] private Transform objective;
    [SerializeField] private NavMeshSurface navMeshSurface2D;

    private NavMeshAgent navMeshAgent;

   private void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.updateRotation = false;
        navMeshAgent.updateUpAxis = false;
    }

    private void Update()
    {
        navMeshAgent.SetDestination(objective.position);
        if(Input.GetKeyDown(KeyCode.Space))
        {
            navMeshSurface2D.BuildNavMeshAsync();
        }
    }
}
