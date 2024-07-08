using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerM : MonoBehaviour{

    public Save_ctrl Save_ctrl_scr;
    public Scene_ctrl Scene_ctrl_scr;
    public int id;

    private void OnTriggerEnter(Collider other){
        if (other.CompareTag("Player")){

            if(Save_ctrl_scr.active_block < Save_ctrl_scr.blocks.Count){
                IModule module = Save_ctrl_scr.blocks[Save_ctrl_scr.active_block].GetModule();
                if (module is TriggerModule triggerModule){
                    triggerModule.Trigger(id);
                }
            }
        }
    }

}