using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// Playerから逃げるAI
/// </summary>
public class RunAway : MonoBehaviour
{
    [Tooltip("Playerから逃げるまでの距離")]
    [SerializeField, Range(1.0f, 20.0f)] private float _runAwayDis = 5.0f;
    [Tooltip("Playerから逃げるスピード")]
    [SerializeField, Range(1.0f, 10.0f)] private float _runAwaySpeed = 5.0f;
    [SerializeField] private NavMeshAgent _agent;
    [SerializeField] private Transform _player;
    [Tooltip("逃げる先の位置")]
    private readonly List<GameObject> _checkPoint = new();
    /// <summary> AIの逃げる先のPositionが格納されたListのインデックス </summary>
    private int _listNum = 0;
    /// <summary> Playerから逃げる距離にいるかどうかのフラグ </summary>
    private bool _runAwayCheck = false;
    /// <summary> 移動時間 </summary>
    private float _time = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        //AIの移動速度をインスペクターで設定する
        _agent.speed = _runAwaySpeed;
        //逃げる先のPositionをListに格納
        for (int i = 0; i < 10; i++)
        {
            _checkPoint.Add(GameObject.Find("GameObject").transform.GetChild(i).gameObject);
        }
        //最初に逃げる先を指定
        _listNum = Random.Range(0, 10);
    }

    // Update is called once per frame
    void Update()
    {
        //Playerと、このオブジェクトの距離をとる(Vector3.Distanceは重くなると考えたので、2乗の値で比較する)
        float disX = _player.transform.position.x - transform.position.x;
        float disY = _player.transform.position.y - transform.position.y;
        float disZ = _player.transform.position.z - transform.position.z;
        float dis = disX*disX + disY*disY + disZ*disZ;

        //Playerとこのオブジェクトとの距離(の2乗)が一定より短くなったら
        if (dis <= _runAwayDis * _runAwayDis)
        {
            _runAwayCheck = true;
        }

        //AIにあるPositionを移動先として指定する
        if (_runAwayCheck == true)
        {
            _agent.SetDestination
                (_checkPoint[_listNum].transform.position);
            _time += Time.deltaTime;
            //一定時間経ったら移動を終了する
            if (_time > 2.0f)
            {
                _runAwayCheck = false;
                _listNum = Random.Range(0, 10);
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
