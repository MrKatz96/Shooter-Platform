using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class Enemy : MonoBehaviour
{
    [SerializeField] private string _name;
    [SerializeField] private float _moveSpeed;

    public Transform _target;
    [SerializeField] private float _distance;
    private Rigidbody2D _rigidbody;
    private Vector2 _moveTo;

    //Update Conditions for FixedUpdate
    private bool _inRange;
    //TRUE = "Right" FALSE = "Left"
    private bool _direction = true;

    private float _startingY;
    private float nextWayPointDist = 3f;
    Path path; 
    int currentWayPoint = 0;
    Seeker seeker;
    bool reachedEndOfPath = false;

    private void Start()
    {
        

        seeker = GetComponent<Seeker>();
        _rigidbody = GetComponent<Rigidbody2D>();
        InvokeRepeating("UpdatePath", 0f, .5f);
        _startingY = transform.position.y;
        seeker.StartPath(_rigidbody.position, new Vector2(_target.position.x, _startingY), OnPathComplete);
     
    }

    void UpdatePath()
    {
        if (seeker.IsDone())
            seeker.StartPath(_rigidbody.position, new Vector2(_target.position.x, _startingY), OnPathComplete);
    }
    void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWayPoint = 0;
        }
    }
    //Calculate data in Update
    private void Update()
    {
       
    }

    //Move objects in FixedUpdate
    private void FixedUpdate()
    {
        if (path == null)
            return;
        if (currentWayPoint >= path.vectorPath.Count)
        {
            reachedEndOfPath = true;
            return;
        }
        else
        {
            reachedEndOfPath = false;
        }
        Vector2 direction = ((Vector2)path.vectorPath[currentWayPoint] - _rigidbody.position).normalized;
        Vector2 force = direction * _moveSpeed * Time.deltaTime;
        _rigidbody.AddForce(force);
        float distance = Vector2.Distance(_rigidbody.position, path.vectorPath[currentWayPoint]);
        if (distance < nextWayPointDist)
        {
            currentWayPoint++;
        }
    }


    private void GetDestination()
    {
        Vector2 direction = _target.position - transform.position;
        direction.Normalize();
        _moveTo = direction;
        if (Vector2.Distance(transform.position, _target.position) < _distance)
        {
            _inRange = true;
        }
    }
    private void Destination()
    {
        //if in distance then move to position
        if (_inRange)
        {
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
            //_sp.flipX = true;
            transform.eulerAngles = new Vector3(transform.rotation.x, 180f, transform.rotation.z);
        }
        else
        {
            //_sp.flipX = false;
            transform.eulerAngles = new Vector3(transform.rotation.x, 0f, transform.rotation.z);
        }
    }
}
