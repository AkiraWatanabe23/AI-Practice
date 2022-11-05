using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Policies;

/// <summary>
/// Agentクラスのオーバーライドメソッドを使用する
/// </summary>
public class RollerAgent : Agent
{
    [SerializeField] private Transform _target;
    private Rigidbody _rb;

    //初期化時に呼ばれる(Startでも良い?)
    public override void Initialize()
    {
        _rb = GetComponent<Rigidbody>();
    }

    //エピソード開始時に呼ばれる
    public override void OnEpisodeBegin()
    {
        //RollerAgentが床から落下している時
        if (transform.localPosition.y < 0)
        {
            // RollerAgentの位置と速度をリセット
            _rb.angularVelocity = Vector3.zero;
            _rb.velocity = Vector3.zero;
            transform.localPosition = new Vector3(0.0f, 0.5f, 0.0f);
        }

        //Targetの位置のリセット
        _target.localPosition
            = new Vector3(Random.value * 8 - 4, 0.5f, Random.value * 8 - 4);
    }


    //状態取得時に呼ばれる
    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(_target.localPosition.x); //TargetのX座標
        sensor.AddObservation(_target.localPosition.z); //TargetのZ座標
        sensor.AddObservation(transform.localPosition.x); //RollerAgentのX座標
        sensor.AddObservation(transform.localPosition.z); //RollerAgentのZ座標
        sensor.AddObservation(_rb.velocity.x); // RollerAgentのX速度
        sensor.AddObservation(_rb.velocity.z); // RollerAgentのZ速度
    }

    //行動実行時に呼ばれる
    public override void OnActionReceived(ActionBuffers actionBuffers)
    {
        //RollerAgentに力を加える
        Vector3 controlSignal = Vector3.zero;
        controlSignal.x = actionBuffers.ContinuousActions[0];
        controlSignal.z = actionBuffers.ContinuousActions[1];
        _rb.AddForce(controlSignal * 10);

        //RollerAgentがTargetの位置にたどりついた時
        float distanceToTarget = Vector3.Distance(
            transform.localPosition, _target.localPosition);
        if (distanceToTarget < 1.42f)
        {
            AddReward(1.0f);
            EndEpisode();
        }

        //RollerAgentが床から落下した時
        if (transform.localPosition.y < 0)
        {
            EndEpisode();
        }
    }

    //ヒューリスティックモードの行動決定時に呼ばれる
    //人間による行動の決定
    public override void Heuristic(in ActionBuffers actionsOut)
    {
        var continuousActionsOut = actionsOut.ContinuousActions;
        continuousActionsOut[0] = Input.GetAxis("Horizontal");
        continuousActionsOut[1] = Input.GetAxis("Vertical");
    }
}