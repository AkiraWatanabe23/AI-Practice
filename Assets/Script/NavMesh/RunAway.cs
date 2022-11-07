using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// Player���瓦����AI
/// </summary>
public class RunAway : MonoBehaviour
{
    [Tooltip("Player���瓦����܂ł̋���")]
    [SerializeField, Range(1.0f, 20.0f)] private float _runAwayDis = 5.0f;
    [Tooltip("Player���瓦����X�s�[�h")]
    [SerializeField, Range(1.0f, 10.0f)] private float _runAwaySpeed = 5.0f;
    [SerializeField] private NavMeshAgent _agent;
    [SerializeField] private Transform _player;
    [Tooltip("�������̈ʒu")]
    private readonly List<GameObject> _checkPoint = new();
    /// <summary> AI�̓�������Position���i�[���ꂽList�̃C���f�b�N�X </summary>
    private int _listNum = 0;
    /// <summary> Player���瓦���鋗���ɂ��邩�ǂ����̃t���O </summary>
    private bool _runAwayCheck = false;
    /// <summary> �ړ����� </summary>
    private float _time = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        //AI�̈ړ����x���C���X�y�N�^�[�Őݒ肷��
        _agent.speed = _runAwaySpeed;
        //��������Position��List�Ɋi�[
        for (int i = 0; i < 10; i++)
        {
            _checkPoint.Add(GameObject.Find("GameObject").transform.GetChild(i).gameObject);
        }
        //�ŏ��ɓ��������w��
        _listNum = Random.Range(0, 10);
    }

    // Update is called once per frame
    void Update()
    {
        //Player�ƁA���̃I�u�W�F�N�g�̋������Ƃ�(Vector3.Distance�͏d���Ȃ�ƍl�����̂ŁA2��̒l�Ŕ�r����)
        float disX = _player.transform.position.x - transform.position.x;
        float disY = _player.transform.position.y - transform.position.y;
        float disZ = _player.transform.position.z - transform.position.z;
        float dis = disX*disX + disY*disY + disZ*disZ;

        //Player�Ƃ��̃I�u�W�F�N�g�Ƃ̋���(��2��)�������Z���Ȃ�����
        if (dis <= _runAwayDis * _runAwayDis)
        {
            _runAwayCheck = true;
        }

        //AI�ɂ���Position���ړ���Ƃ��Ďw�肷��
        if (_runAwayCheck == true)
        {
            _agent.SetDestination
                (_checkPoint[_listNum].transform.position);
            _time += Time.deltaTime;
            //��莞�Ԍo������ړ����I������
            if (_time > 2.0f)
            {
                _runAwayCheck = false;
                _listNum = Random.Range(0, 10);
                _time = 0.0f;
            }
        }
    }

    /// <summary> ������AI�̏��(�s���Ǘ�) </summary>
    public enum RunAwayState
    {
        Wait,
        RunAway,
        //Attack,
    }
}
