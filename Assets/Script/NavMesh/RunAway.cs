using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// Playerを避けるように動くAI
/// </summary>
public class RunAway : MonoBehaviour
{
    [Tooltip("Playerから逃げるまでの距離")]
    [SerializeField, Range(1.0f, 20.0f)] private float _runAwayDis = 5.0f;
    [Tooltip("移動するスピード")]
    [SerializeField, Range(1.0f, 10.0f)] private float _runAwaySpeed = 5.0f;
    [SerializeField] private NavMeshAgent _agent;
    [SerializeField] private Transform _player;
    /// <summary> 進行先を格納したList </summary>
    private readonly List<GameObject> _checkPoint = new();
    /// <summary> 進行先のPositionが格納されたListのインデックス </summary>
    private int _listNum = 0;
    /// <summary> 移動時間 </summary>
    private float _time = 0.0f;
    /// <summary> Playerを避けるかどうかの判定 </summary>
    private bool _runAwayCheck = false;
    /// <summary> 行動管理 </summary>
    private RunAwayState _state;

    // Start is called before the first frame update
    void Start()
    {
        //移動速度をInspectorで設定する
        _agent.speed = _runAwaySpeed;
        //逃げる先のPositionをListに格納
        for (int i = 0; i < 10; i++)
        {
            _checkPoint.Add(transform.GetChild(i).gameObject);
        }
        //最初に逃げる先を指定
        _listNum = Random.Range(0, 10);
        //初期状態を設定
        _state = RunAwayState.Wait;
    }

    // Update is called once per frame
    void Update()
    {
        //Playerと、このオブジェクトの距離をとる
        //Vector3.SqrMagnitudeだと、2乗の値が返ってくる
        float dis = Vector3.SqrMagnitude(_player.position - transform.position);
        //以下の書き方でも、SqrMagnitudeとやっていることは同じ
        //float disX = _player.position.x - transform.position.x;
        //float disY = _player.position.y - transform.position.y;
        //float disZ = _player.position.z - transform.position.z;
        //float dis = disX * disX + disY * disY + disZ * disZ;

        //Playerとこのオブジェクトとの距離(の2乗)が一定より短くなったら && 待機状態なら
        //以下の条件式が正しく動かない時がある(移動中に以下の条件を満たした時?)
        if (dis <= _runAwayDis * _runAwayDis && _state == RunAwayState.Wait)
        {
            _runAwayCheck = true;
            _state = RunAwayState.RunAway;
        }

        if (_runAwayCheck == true)
        {
            _agent.SetDestination
                (_checkPoint[_listNum].transform.position);
            _time += Time.deltaTime;
            //一定時間経ったら移動を終了する
            if (_time > 2.0f)
            {
                _state = RunAwayState.Wait;
                _runAwayCheck = false;
                _time = 0.0f;
                //次の移動先を決定する
                //(このままだと、次の進行先が変更されない可能性がある...Random.Range()で取得した値が同じだった場合)
                _listNum = Random.Range(0, 10);
            }
        }
    }

    /// <summary> 逃げるAIの状態(行動管理) </summary>
    public enum RunAwayState
    {
        Wait,
        RunAway,
    }
}
