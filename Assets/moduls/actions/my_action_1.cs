using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class my_action_1 : MonoBehaviour, IActionModule{

    public GameObject obj;
    public GameObject point1;
    public GameObject point2;
    public float forse;


    
    public void Activate(){
        Rigidbody rb = obj.GetComponent<Rigidbody>();
        rb.isKinematic = false;
        Vector3 direction = (point2.transform.position - point1.transform.position).normalized;
        rb.AddForceAtPosition(direction * forse, point1.transform.position);
    }

}
