using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

public class Save_ctrl : MonoBehaviour{

    public int active_save_block = 0;
    public int active_block = 0;

    public List<Block> blocks = new List<Block>();

    public Blocks_create Blocks_create_scr;
    public Scene_ctrl Scene_ctrl_scr;

    void Start(){
        active_save_block = get_active();
        active_block = active_save_block;
        test_create_blocks();
    }


    //logic

    public void move_active(int id_to){
        active_block = id_to;
        if(active_block < blocks.Count){
            blocks[id_to].GetModule().Activate();
        }else{
            Debug.Log("end reached");
        }
    }

    public void save_active(int id){
        PlayerPrefs.SetInt("active_save_block", id);
        PlayerPrefs.Save();
        Debug.Log("saved active block " + id);
    }

    public int get_active(){
        int id = PlayerPrefs.GetInt("active_save_block", 0);
        return id;
    }

    public void reset_active(){
        save_active(0);
        get_active();
        Debug.Log("reseted active block to " + get_active());
    }



    //block create
    public void create_start_block(Vector3 spawn_point){
        int id = blocks.Count;
        blocks.Add(Blocks_create_scr.create_block(id,0,this));
        if (blocks[id].GetModule() is StartModule startModule){
            startModule.AddPoint(spawn_point);
        }
        Debug.Log("created StartModule in block " + id);

    }

    public void create_save_block(Vector3 spawn_point){
        int id = blocks.Count;
        blocks.Add(Blocks_create_scr.create_block(id,1,this));
        if (blocks[id].GetModule() is SaveModule saveModule){
            saveModule.AddPoint(spawn_point);
        }
        Debug.Log("created SaveModule in block " + id + ", set as position " + spawn_point);
    }

    public void create_trigger_block(int trigger_id){
        int id = blocks.Count;
        blocks.Add(Blocks_create_scr.create_block(id,2,this));

        if (blocks[id].GetModule() is TriggerModule triggerModule){
            triggerModule.AddTrigger(trigger_id);
        }

        //Scene_ctrl_scr.which_block_id_trigger[trigger_id] = id;
        Debug.Log("created TriggerModule in block " + id + ", set trigger id" + trigger_id);
    }

    public void create_action_block(int action_id){
        int id = blocks.Count;
        blocks.Add(Blocks_create_scr.create_block(id,3,this));

        if (blocks[id].GetModule() is ActionModule actionModule){
            actionModule.AddAction(action_id);
        }
        Debug.Log("created ActionModule in block " + id + ", set action id" + action_id);
    }


    //
    void test_create_blocks(){

        create_start_block(new Vector3(2.5f,0,0));
        create_trigger_block(0);

        //action move
        create_action_block(0);

        create_trigger_block(1);

        //action fall
        create_action_block(1);

        create_trigger_block(2);
        create_save_block(new Vector3(-18,0,0));        
        create_trigger_block(3);

        //action lighting
        create_action_block(2);


        Debug.Log("amount of blocks in list = " + blocks.Count);

        Debug.Log("active save block is " + active_save_block);
        Scene_ctrl_scr.respawn_player(active_save_block);

        
        
        move_active(active_block);


    }
}

//string json = JsonConvert.SerializeObject(blocks, Formatting.Indented);
//Debug.Log(json);