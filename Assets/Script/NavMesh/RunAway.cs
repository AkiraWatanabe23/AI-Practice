using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RunAway : MonoBehaviour
{
    [Tooltip("Player‚©‚ç“¦‚°‚é‚Ü‚Å‚Ì‹——£")]
    [SerializeField, Range(1.0f, 20.0f)] private float _runAwayDis = 5f;
    [SerializeField] private NavMeshAgent _agent;
    [SerializeField] private PlayerMove _player;
    private bool _runAwayCheck = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (_runAwayCheck)
        {
            _agent.SetDestination(_player.transform.position * _player.MoveSpeed);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Run Away");
            _runAwayCheck = true;
        }
    }
}
