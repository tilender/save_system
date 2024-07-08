using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blocks_create : MonoBehaviour{


    public Block create_block(int id, int module_id, Save_ctrl SCtrl){
        Block current_block = new Block();
        current_block.SCtrl = SCtrl;
        current_block.id = id;
        current_block.intro_id = id-1;
        current_block.outro_id = id+1;
        switch (module_id){
            case 0:
                current_block.AddModule(new StartModule());
            break;
            case 1:
                current_block.AddModule(new SaveModule());
            break;
            case 2:
                current_block.AddModule(new TriggerModule());
            break;
            case 3:
                current_block.AddModule(new ActionModule());
            break;
        }

        return current_block;
    }

    public void Start(){
        
    }

}