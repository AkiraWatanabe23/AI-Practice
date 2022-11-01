using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Chase : MonoBehaviour
{
    [SerializeField] NavMeshAgent _agent;
    [SerializeField] GameObject _target;

    // Update is called once per frame
    void Update()
    {
        if (_target != null)
        {
            //destination...行き先(AIの進行方向の決定)
            _agent.destination = _target.transform.position;
        }
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.CompareTag("Finish"))
        {
            other.gameObject.SetActive(false);
            Debug.Log("着いた");
        }
    }
}
