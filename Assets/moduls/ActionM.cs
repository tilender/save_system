using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionM : MonoBehaviour{

    public Save_ctrl Save_ctrl_scr;
    public Scene_ctrl Scene_ctrl_scr;
    public int id;
    public GameObject object_with_IAM_scr;
    public IActionModule IAM_scr;
    public int wait_time;//в милисекундах

    public void start_action(){
        IAM_scr = object_with_IAM_scr.GetComponent<IActionModule>();
        Debug.Log("start_action() in ActionM " + id);
        IAM_scr.Activate();
    }

}

public interface IActionModule{
    void Activate();
}

// , IActionModule{
//     public void Activate(){
// 
//     }