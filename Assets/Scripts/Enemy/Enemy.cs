using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    
    [SerializeField] private string _name;
    [SerializeField] private float _moveSpeed;
    private float _healthPoint;
    [SerializeField] private float _maxHealthPoint;
    private Transform _target;
    [SerializeField] private float _distance;
    private float _YstartingPosition;
    private Rigidbody2D _rigidbody;
    private Vector2 _moveTo;

    //Conditions for FixedUpdate
    private bool _inRange;
    //TRUE = "Right" FALSE = "Left"
    private bool _direction;

    private void Awake()
    {
        _YstartingPosition = transform.position.y;
        _healthPoint = _maxHealthPoint;
        _target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    //Calculate data in Update
    private void Update()
    {
        GetDestination();
        
    }

    //Move objects in FixedUpdate
    private void FixedUpdate()
    {
        Destination();
        
    }


    private void GetDestination()
    {
        Vector2 direction = new Vector2 (_target.position.x - transform.position.x, _YstartingPosition);
        direction.Normalize();
        _moveTo = direction;
        if (Vector2.Distance(transform.position, _target.position) < _distance)
        {
            _inRange = true;
            GetRotate();
        }
    }
    private void Destination()
    {
        //if in distance then move to position
        if (_inRange)
        {
            Rotate();
            _rigidbody.MovePosition((Vector2)transform.position + (_moveTo * _moveSpeed * Time.deltaTime));
            _inRange = false;
        }

    }
    private void GetRotate()
    {
        if (transform.position.x > _target.position.x)
        {
            _direction = false;
        }
        else
        {
            _direction = true;
        }
    }
    private void Rotate()
    {
        if (!_direction)
        {
            transform.eulerAngles = new Vector3(transform.rotation.x, 180f, transform.rotation.z);
        }
        else
        {
            transform.eulerAngles = new Vector3(transform.rotation.x, 0f, transform.rotation.z);
        }
    }
}
