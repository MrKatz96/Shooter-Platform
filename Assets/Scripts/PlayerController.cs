using System;
using UnityEngine;

public class PlayerController : MonoBehaviour{

    //Config
    [Header("Input Settings:")]
    [SerializeField] float runSpeed = 5f;
    
    //[Space]

    //State
    //bool isAlive = true;

    //Cached Component references


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Run();
        Rotate();
    }

    private void Run()
    {
        //Horizontal movement of the player.
        float controlThrow = Input.GetAxis("Horizontal");
        Vector2 playerVelocity = new Vector2(controlThrow * runSpeed, GetComponent<Rigidbody2D>().velocity.y);
        GetComponent<Rigidbody2D>().velocity = playerVelocity;

        //Running Animation
        bool playerIsMoving = Math.Abs(GetComponent<Rigidbody2D>().velocity.x) > Mathf.Epsilon;
        GetComponent<Animator>().SetBool("Running", playerIsMoving);
    }
  
    private void Rotate()
    {
        Vector3 gunPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (gunPosition.x < transform.position.x)
        {
            transform.eulerAngles = new Vector3(transform.rotation.x, 180f, transform.rotation.z);
        }
        else
        {
            transform.eulerAngles = new Vector3(transform.rotation.x, 0f, transform.rotation.z);
        }
    }
}

