using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RunAway : MonoBehaviour
{
    [Tooltip("Playerから逃げるまでの距離")]
    [SerializeField, Range(1.0f, 20.0f)] private float _runAwayDis = 5.0f;
    [Tooltip("Playerから逃げるスピード")]
    [SerializeField, Range(1.0f, 10.0f)] private float _runAwaySpeed = 5.0f;
    [SerializeField] private NavMeshAgent _agent;
    [SerializeField] private Transform _player;
    private bool _runAwayCheck = false;
    [SerializeField] private float _time = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        _agent.speed = _runAwaySpeed;
    }

    // Update is called once per frame
    void Update()
    {
        var dis = Vector3.Distance(_player.position, transform.position);

        if (dis <= _runAwayDis)
        {
            Debug.Log("Run Away");
            _runAwayCheck = true;
        }

        if (_runAwayCheck == true)
        {
            _agent.SetDestination(_player.position * -1);
            _time += Time.deltaTime;
            if (_time > 2.0f)
            {
                Debug.Log("Stop");
                _runAwayCheck = false;
                _time = 0.0f;
            }
        }
    }

    /// <summary> 逃げるAIの状態(行動管理) </summary>
    public enum RunAwayState
    {
        Wait,
        RunAway,
        //Attack,
    }
}
