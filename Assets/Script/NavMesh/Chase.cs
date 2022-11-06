using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// Playerを追いかけるAI(鬼ごっこの鬼のイメージ)
/// </summary>
public class Chase : MonoBehaviour
{
    [SerializeField] private NavMeshAgent _agent;
    [SerializeField] private Transform _target;

    // Update is called once per frame
    void Update()
    {
        //destination...行き先(AIの進行方向の決定)
        _agent.SetDestination(_target.position);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.SetActive(false);
            Debug.Log("着いた");
        }
    }
}
