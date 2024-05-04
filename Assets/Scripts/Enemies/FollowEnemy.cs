using UnityEngine;
using UnityEngine.AI;

public class Follow_AI : MonoBehaviour
{
    [SerializeField] private string playerTag = "Player";
    private Transform playerObject;
    private NavMeshAgent navMeshAgent;
    private bool isFacingRight = true;

    private void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.updateRotation = false;
        navMeshAgent.updateUpAxis = false;
        playerObject = GameObject.FindGameObjectWithTag(playerTag).transform;
    }

    private void FixedUpdate()
    {
        if (playerObject != null)
        {
            bool isPlayerRight = transform.position.x < playerObject.position.x;
            Flip(isPlayerRight);
            navMeshAgent.SetDestination(playerObject.position);
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
