using System;
using System.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

public class Block{
    
    public int intro_id { get; set; }
    public int id { get; set; }
    public int outro_id { get; set; }


    [JsonIgnore]
    public Save_ctrl SCtrl { get; set; }

    public IModule main_module;

    public void AddModule(IModule module){
        SetModuleBlock(module);
        main_module = module;
        module.OnAdd();
    }

    public void SetModuleBlock(IModule module){
        module.Block = this;
    }

    public IModule GetModule(){
        return main_module;
    }
    
    public void RemoveModule(){
        main_module = null;
    }

    public void move_to_next(){
        Debug.Log("moved from " + id + ", to " + outro_id);
        SCtrl.move_active(outro_id);
        //переключение текущего блока в другом файле
    }

}

public interface IModule{
    Block Block { get; set; }
    void Activate();
    void OnAdd();
}

public class SaveModule : IModule {
    public Vector3 spawn_point;
    
    public void AddPoint(Vector3 spawn_point_vec){
        spawn_point = spawn_point_vec;
    }

    //interface
    [JsonIgnore]
    public Block Block { get; set; }
    public void Activate(){
        Debug.Log("save started on " + Block.id);
        Block.SCtrl.save_active(Block.id);
        Block.move_to_next();
    }
    public void OnAdd(){

    }

}

public class TriggerModule : IModule {

    
    public int TriggerId { get; private set; }

    public bool Triggered = false;

    public event Action OnTriggersReached;

    public void Trigger(int triggerId){
        if (TriggerId == triggerId && !Triggered){
            Triggered = true;
            OnTriggersReached?.Invoke();
        }
    }

    public void AddTrigger(int triggerId){
        TriggerId = triggerId;
    }

    //interface
    [JsonIgnore]
    public Block Block { get; set; }
    public void Activate(){
        Debug.Log("trigger on block" + Block.id + " waiting");
    }
    public void OnAdd(){
        OnTriggersReached += () => { 
            Debug.Log("trigger on block" + Block.id + " reached");
        };
        OnTriggersReached += Block.move_to_next;
    }  

}

public class ActionModule : IModule {

    private int ActionId ;
    private int time_to_wait;

    public event Action OnActionReached;

    public void AddAction(int actionId){
        ActionId = actionId;
    }

    //interface
    [JsonIgnore]
    public Block Block { get; set; }

    public async void Activate(){
        Debug.Log("starting action on block" + Block.id);
        time_to_wait = Block.SCtrl.Scene_ctrl_scr.actions[ActionId].wait_time;
        Block.SCtrl.Scene_ctrl_scr.actions[ActionId].start_action();
        Debug.Log("action on block" + Block.id + " started");
        await action_end();
    }

    public void OnAdd(){
        OnActionReached += Block.move_to_next;
    }

    async Task action_end(){
        await Task.Delay(time_to_wait);
        Debug.Log("action on block" + Block.id + " end");
        OnActionReached?.Invoke();
    }

}

public class StartModule : IModule {

    public Vector3 spawn_point;

    public void AddPoint(Vector3 spawn_point_vec){
        spawn_point = spawn_point_vec;
    }

    //interface
    [JsonIgnore]
    public Block Block { get; set; }
    public void Activate(){
        Debug.Log("start on " + Block.id);
        Block.SCtrl.save_active(Block.id);
        Block.move_to_next();
    }
    public void OnAdd(){

    }

}