using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class my0_nonaction : MonoBehaviour, IActionModule{

    public void Activate(){
        Debug.Log("fake action done");
    }

}