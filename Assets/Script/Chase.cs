using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Chase : MonoBehaviour
{
    [SerializeField] NavMeshAgent _agent;
    [SerializeField] Transform _target;

    // Update is called once per frame
    void Update()
    {
        //destination...s‚«æ(AI‚Ìis•ûŒü‚ÌŒˆ’è)
        _agent.SetDestination(_target.position);
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.CompareTag("Finish"))
        {
            other.gameObject.SetActive(false);
            Debug.Log("’…‚¢‚½");
        }
    }
}
