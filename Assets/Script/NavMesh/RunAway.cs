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
    [SerializeField] private GameObject _player;
    [Tooltip("逃げる先の位置")]
    [SerializeField] private List<GameObject> _checkPoint = new();
    private int _listCount = 0;
    private bool _runAwayCheck = false;
    private float _time = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        _agent.speed = _runAwaySpeed;
        for (int i = 0; i < 10; i++)
        {
            _checkPoint.Add(GameObject.Find("GameObject").transform.GetChild(i).gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        var dis = Vector3.Distance(_player.transform.position, transform.position);

        if (dis <= _runAwayDis)
        {
            _runAwayCheck = true;
        }

        if (_runAwayCheck == true)
        {
            _agent.SetDestination
                (_listCount != 9 ? _checkPoint[_listCount].transform.position : _checkPoint[0].transform.position);
            _time += Time.deltaTime;
            if (_time > 2.0f)
            {
                Debug.Log("Stop");
                Debug.Log(_listCount);
                _runAwayCheck = false;
                _listCount += _listCount != 9 ? 1 : -_listCount;
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
