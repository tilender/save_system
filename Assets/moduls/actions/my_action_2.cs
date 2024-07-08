using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class my_action_2 : MonoBehaviour, IActionModule{

    public GameObject obj; 

    public void Activate(){
        obj.SetActive(true);
    }


}