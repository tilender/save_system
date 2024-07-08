using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Scene_ctrl : MonoBehaviour{

    public Save_ctrl Save_ctrl_scr;

    public TriggerM[] triggers;//index = id
    public ActionM[] actions;

    public GameObject Player;

    //public int[] which_block_id_trigger;
    
    void Start(){
        for(int i = 0; i < triggers.Length ; i++){
            triggers[i].Scene_ctrl_scr = this;
            triggers[i].Save_ctrl_scr = Save_ctrl_scr;
            triggers[i].id = i;
        }
        for(int i2 = 0; i2 < actions.Length ; i2++){
            actions[i2].Scene_ctrl_scr = this;
            actions[i2].Save_ctrl_scr = Save_ctrl_scr;
            actions[i2].id = i2;
        }
    }

    public void respawn_player(int active_id){
        //Save_ctrl_scr.blocks[active_id].
        if(active_id < Save_ctrl_scr.blocks.Count){
            IModule module = Save_ctrl_scr.blocks[active_id].GetModule();
            if (module is SaveModule saveModule){
                Player.transform.position = saveModule.spawn_point;
                Debug.Log("player respawned at saveModule " + active_id);
                //triggerModule.Trigger(id);
            }else if (module is StartModule startModule){
                Player.transform.position = startModule.spawn_point;
                Debug.Log("player respawned at startModule " + active_id);
                //triggerModule.Trigger(id);
            }
            
        }
    }

    public void reset_game(){
        Save_ctrl_scr.reset_active();
        reload_scene();
    }
    public void reload_scene(){
        SceneManager.LoadScene(0);
    }
}