using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour{
    Player player;
    GameController gameController;
    public string Name = "Item";
    public bool key = false;
    public bool cure = false;
    public float cureValue = 0.2f;
    public string keyColor;

    void Awake(){
        player = FindObjectOfType<Player>();
        gameController = FindObjectOfType<GameController>();    
    }

    public void DoCure(){
        player.life += cureValue;
    }

    public void IncreaseTime(float time){
        gameController.time += time;
    }         
}
