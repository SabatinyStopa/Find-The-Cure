using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class GameController : MonoBehaviour{
    public float time;
    public Player player;
	public GameObject[] crosses;
    public GameObject gameOverScreen, PauseScreen;
    public bool gameOver = false;
    public bool tutorial, victory, paused;
    public Virus virus;
    public float virusDamage = 0.1f;
    public int collectedMedicine = 0;
    public Text timeText, collectedMedicineText, gameOverText;
    public Material[] keyColors;

    void Start(){
        setCrossesColors();
		timeText.text = time.ToString ("0");
        tutorial = true;
    }
    void Update(){
        victory = (collectedMedicine >= 16);
        TimeControl();
        if(victory || player.life < 0)
            GameOver();

        UiTextControl();
    }
    void UiTextControl(){
        collectedMedicineText.text = "Remédios: " + collectedMedicine.ToString() + "/16";
    }

	void setCrossesColors(){
        for(int i = 0; i < crosses.Length; i++){
            int j = Random.Range(i, crosses.Length);
            GameObject temp = crosses[i];
            crosses[i] = crosses[j];
            crosses[j] = temp;
        }

        for(int i = 0; i < crosses.Length; i++){
            Item item = crosses[i].GetComponent<Item>();
            if(i < 5){
			    crosses[i].GetComponent<MeshRenderer>().material = keyColors[0];
                item.keyColor = "red";
            }
            else if(i < 10){
			    crosses[i].GetComponent<MeshRenderer>().material = keyColors[1];
                item.keyColor = "green";
            }
            else if(i < 15){
			    crosses[i].GetComponent<MeshRenderer>().material = keyColors[2];
                item.keyColor = "pink";
            }
            else if(i < 20){
			    crosses[i].GetComponent<MeshRenderer>().material = keyColors[3];
                item.keyColor = "purple";
            }
            else if(i < 25){
			    crosses[i].GetComponent<MeshRenderer>().material = keyColors[4];
                item.keyColor = "blue";
            }
        }
	}

    void TimeControl(){
        if(time <= 0)
            return;

        StopCoroutine("DoDamage");

        if(player.infected)
            time -= 1 * Time.deltaTime;
        
        if(time <= 0)
            StartCoroutine("DoDamage");

        timeText.text = time.ToString ("0");
    }
    IEnumerator DoDamage(){
        while(player.life >= 0){
            yield return new WaitForSecondsRealtime(2f);
            virus.DoDamage(virusDamage);
        }
        
    }

    public void PauseGame(){
        Time.timeScale = 0;
        PauseScreen.SetActive(true);
        paused = true;
    }

    public void UnpauseGame(){
        Time.timeScale = 1;
        PauseScreen.SetActive(false);
        paused = false;
    }

    public void GameOver(){
        gameOverScreen.SetActive(true);
        gameOver = true;

        if(victory)
            gameOverText.text = VictoryText();
        else
            gameOverText.text = LostText();
    }

    string LostText(){
        return "Você perdeu!\n" + "Coletou: " + collectedMedicine.ToString() + "/16 remédios\n";
    }
    string VictoryText(){
        return "Você conseguiu coletar todas as medicações e não morreu!!!\n Parabéns!!!";
    }
}
