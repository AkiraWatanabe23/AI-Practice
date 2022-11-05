using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Chase : MonoBehaviour
{
    [SerializeField] private NavMeshAgent _agent;
    [SerializeField] private Transform _target;

    // Update is called once per frame
    void Update()
    {
        //destination...s‚«æ(AI‚Ìis•ûŒü‚ÌŒˆ’è)
        _agent.SetDestination(_target.position);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.SetActive(false);
            Debug.Log("’…‚¢‚½");
        }
    }
}
