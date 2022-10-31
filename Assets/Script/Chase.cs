using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Chase : MonoBehaviour
{
    [SerializeField] NavMeshAgent _agent;
    [SerializeField] GameObject _target;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (_target != null)
        {
            //destination...s‚«æ(AI‚Ìis•ûŒü‚ÌŒˆ’è)
            _agent.destination = _target.transform.position;
        }
    }

    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.CompareTag("Finish"))
        {
            col.gameObject.SetActive(false);
            Debug.Log("’…‚¢‚½");
        }
    }
}
