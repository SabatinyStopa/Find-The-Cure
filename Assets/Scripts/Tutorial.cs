using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Tutorial : MonoBehaviour{
    public Text text;
    public Virus virus;
    public int counter = 0;
    void Start() {
        text.gameObject.SetActive(true);
        text.text = "Você deve pegar todas as chaves e abrir os armários! (CLIQUE PARA AVANÇAR)";    
    }
    void Update() {
        if(FindObjectOfType<GameController>().tutorial)
            if(Input.GetButtonDown("Fire1"))
                NextText();    
    }


    void NextText(){
        switch (counter){
            case 0:
                text.text = "Porém, você tem um limite de 5 chaves para carregar ";
            break;
            case 1:
                text.text = "Use-as nos ármarios para destrancá-los";
            break;
            case 2:
                text.text = "Pegue também as medicações que estão espalhadas";
            break;
            case 3:
                text.text = "Afinal, você está doente e irá morrer sem ela...";
            break;
            case 4:
                text.text = "Use a tecla E para coletar os itens";
            break;
            case 5:
                text.text = "Para abrir os armários clique com o botão esquerdo";
            break;
            case 6:
                text.text = "Não esqueça de destrancar ele primeiro";
            break;
            case 7:
                text.text = "Parabéns! Você está infectado";
                FindObjectOfType<Player>().infected = true;
                FindObjectOfType<GameController>().tutorial = false;
                text.GetComponent<UiText>().textFadeOut();
            break;
        }
        counter++;
    }

    void VirusStart(){
        virus.player.infected = true;
    }
}
