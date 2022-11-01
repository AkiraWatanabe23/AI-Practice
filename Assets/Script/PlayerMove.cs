using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] float _moveSpeed = 0f;
    Rigidbody _rb;

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        float hol = Input.GetAxisRaw("Horizontal");
        float ver = Input.GetAxisRaw("Vertical");

        Vector3 dir = Vector3.forward * ver + Vector3.right * hol;
        dir = Camera.main.transform.TransformDirection(dir);
        dir.y = 0;
        if (dir != Vector3.zero)
        {
            transform.forward = dir;
        }

        float y = _rb.velocity.y;


        _rb.velocity = dir * _moveSpeed + Vector3.up * y;
    }
}
