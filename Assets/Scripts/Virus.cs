using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Virus : MonoBehaviour{
    public Player player;
    public void DoDamage(float damage){
        player.life -= damage;
    }
}
