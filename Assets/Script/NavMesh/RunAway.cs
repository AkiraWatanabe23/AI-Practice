using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// Player�������悤�ɓ���AI
/// </summary>
public class RunAway : MonoBehaviour
{
    [Tooltip("Player���瓦����܂ł̋���")]
    [SerializeField, Range(1.0f, 20.0f)] private float _runAwayDis = 5.0f;
    [Tooltip("�ړ�����X�s�[�h")]
    [SerializeField, Range(1.0f, 10.0f)] private float _runAwaySpeed = 5.0f;
    [SerializeField] private NavMeshAgent _agent;
    [SerializeField] private Transform _player;
    /// <summary> �i�s����i�[����List </summary>
    private readonly List<GameObject> _checkPoint = new();
    /// <summary> �i�s���Position���i�[���ꂽList�̃C���f�b�N�X </summary>
    private int _listNum = 0;
    /// <summary> �ړ����� </summary>
    private float _time = 0.0f;
    /// <summary> Player������邩�ǂ����̔��� </summary>
    private bool _runAwayCheck = false;
    /// <summary> �s���Ǘ� </summary>
    private RunAwayState _state;

    // Start is called before the first frame update
    void Start()
    {
        //�ړ����x��Inspector�Őݒ肷��
        _agent.speed = _runAwaySpeed;
        //��������Position��List�Ɋi�[
        for (int i = 0; i < 10; i++)
        {
            _checkPoint.Add(transform.GetChild(i).gameObject);
        }
        //�ŏ��ɓ��������w��
        _listNum = Random.Range(0, 10);
        //������Ԃ�ݒ�
        _state = RunAwayState.Wait;
    }

    // Update is called once per frame
    void Update()
    {
        //Player�ƁA���̃I�u�W�F�N�g�̋������Ƃ�
        //Vector3.SqrMagnitude���ƁA2��̒l���Ԃ��Ă���
        float dis = Vector3.SqrMagnitude(_player.position - transform.position);
        //�ȉ��̏������ł��ASqrMagnitude�Ƃ���Ă��邱�Ƃ͓���
        //float disX = _player.position.x - transform.position.x;
        //float disY = _player.position.y - transform.position.y;
        //float disZ = _player.position.z - transform.position.z;
        //float dis = disX * disX + disY * disY + disZ * disZ;

        //Player�Ƃ��̃I�u�W�F�N�g�Ƃ̋���(��2��)�������Z���Ȃ����� && �ҋ@��ԂȂ�
        //�ȉ��̏������������������Ȃ���������(�ړ����Ɉȉ��̏����𖞂�������?)
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
            //��莞�Ԍo������ړ����I������
            if (_time > 2.0f)
            {
                _state = RunAwayState.Wait;
                _runAwayCheck = false;
                _time = 0.0f;
                //���̈ړ�������肷��
                //(���̂܂܂��ƁA���̐i�s�悪�ύX����Ȃ��\��������...Random.Range()�Ŏ擾�����l�������������ꍇ)
                _listNum = Random.Range(0, 10);
            }
        }
    }

    /// <summary> ������AI�̏��(�s���Ǘ�) </summary>
    public enum RunAwayState
    {
        Wait,
        RunAway,
    }
}
