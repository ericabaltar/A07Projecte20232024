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



    private bool isFacingRight = true;


    private NavMeshAgent navMeshAgent;






    private void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.updateRotation = false;
        navMeshAgent.updateUpAxis = false;
    }


    private void Update()
    {
        bool isFacingRight = transform.position.x < objective.transform.position.x;
        Flip(isFacingRight);




        navMeshAgent.SetDestination(objective.position);
        if (Input.GetKeyDown(KeyCode.Space))
        {
            navMeshSurface2D.BuildNavMeshAsync();
        }
    }


    private void Flip(bool isPlayerRight)
    {
        if ((isFacingRight && isPlayerRight) || (!isFacingRight && !isPlayerRight))
        {
            isFacingRight = !isFacingRight;
            Vector3 scale = transform.localScale;
            scale.x *= -1;
            transform.localScale = scale;
        }
    }




}

