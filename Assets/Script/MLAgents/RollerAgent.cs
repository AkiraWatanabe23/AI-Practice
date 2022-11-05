using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Policies;

/// <summary>
/// Agent�N���X�̃I�[�o�[���C�h���\�b�h���g�p����
/// </summary>
public class RollerAgent : Agent
{
    [SerializeField] private Transform _target;
    private Rigidbody _rb;

    //���������ɌĂ΂��(Start�ł��ǂ�?)
    public override void Initialize()
    {
        _rb = GetComponent<Rigidbody>();
    }

    //�G�s�\�[�h�J�n���ɌĂ΂��
    public override void OnEpisodeBegin()
    {
        //RollerAgent�������痎�����Ă��鎞
        if (transform.localPosition.y < 0)
        {
            // RollerAgent�̈ʒu�Ƒ��x�����Z�b�g
            _rb.angularVelocity = Vector3.zero;
            _rb.velocity = Vector3.zero;
            transform.localPosition = new Vector3(0.0f, 0.5f, 0.0f);
        }

        //Target�̈ʒu�̃��Z�b�g
        _target.localPosition
            = new Vector3(Random.value * 8 - 4, 0.5f, Random.value * 8 - 4);
    }


    //��Ԏ擾���ɌĂ΂��
    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(_target.localPosition.x); //Target��X���W
        sensor.AddObservation(_target.localPosition.z); //Target��Z���W
        sensor.AddObservation(transform.localPosition.x); //RollerAgent��X���W
        sensor.AddObservation(transform.localPosition.z); //RollerAgent��Z���W
        sensor.AddObservation(_rb.velocity.x); // RollerAgent��X���x
        sensor.AddObservation(_rb.velocity.z); // RollerAgent��Z���x
    }

    //�s�����s���ɌĂ΂��
    public override void OnActionReceived(ActionBuffers actionBuffers)
    {
        //RollerAgent�ɗ͂�������
        Vector3 controlSignal = Vector3.zero;
        controlSignal.x = actionBuffers.ContinuousActions[0];
        controlSignal.z = actionBuffers.ContinuousActions[1];
        _rb.AddForce(controlSignal * 10);

        //RollerAgent��Target�̈ʒu�ɂ��ǂ������
        float distanceToTarget = Vector3.Distance(
            transform.localPosition, _target.localPosition);
        if (distanceToTarget < 1.42f)
        {
            AddReward(1.0f);
            EndEpisode();
        }

        //RollerAgent�������痎��������
        if (transform.localPosition.y < 0)
        {
            EndEpisode();
        }
    }

    //�q���[���X�e�B�b�N���[�h�̍s�����莞�ɌĂ΂��
    //�l�Ԃɂ��s���̌���
    public override void Heuristic(in ActionBuffers actionsOut)
    {
        var continuousActionsOut = actionsOut.ContinuousActions;
        continuousActionsOut[0] = Input.GetAxis("Horizontal");
        continuousActionsOut[1] = Input.GetAxis("Vertical");
    }
}