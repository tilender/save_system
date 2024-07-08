using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Controller : MonoBehaviour{

    public Rigidbody rb;
    public float speed;

    void FixedUpdate(){

        float vert_axis = 0;
        vert_axis += Input.GetKey(KeyCode.W) ? 1 : 0;
        vert_axis -= Input.GetKey(KeyCode.S) ? 1 : 0;

        float hor_axis = 0;
        hor_axis += Input.GetKey(KeyCode.D) ? 1 : 0;
        hor_axis -= Input.GetKey(KeyCode.A) ? 1 : 0;

        Vector3 localInput = new Vector3(-hor_axis, 0, -vert_axis).normalized;

        Vector3 localVelocity = localInput * speed;

        Vector3 globalVelocity = rb.gameObject.transform.TransformDirection(localVelocity);

        globalVelocity.y = rb.velocity.y;

        rb.velocity = globalVelocity;
    }

}