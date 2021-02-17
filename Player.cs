using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : PhysicsObject
{
    //Extend to PhysicsObject script for access to gravity/physics vars
    //max speed
    [SerializeField] float maxSpeed = 1f;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Left & Right movement
        targetVelocity = new Vector2(Input.GetAxis("Horizontal") * maxSpeed, 0); //Time.deltaTime calc in PhysicsObject Script
        
        //Jump
        if (Input.GetButtonDown("Jump") && grounded)
        {
            velocity.y = 10; //Vec2 var called in PO.cs
        }

    }
}
