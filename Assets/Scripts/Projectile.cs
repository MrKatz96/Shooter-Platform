using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Projectile : MonoBehaviour
{
    public float _speed;
    public float _lifeTime;

    public GameObject destroyEffect;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("DestroyProjectile", _lifeTime);   
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(transform.up * _speed * Time.deltaTime);
        
    }
    void DestroyProjectile()
    {
        Instantiate(destroyEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
