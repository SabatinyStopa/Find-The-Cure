using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiText : MonoBehaviour{    
    public Text text;

    public void textFadeOut(){
        StartCoroutine("FadeText");    
    }

    IEnumerator FadeText(){
        yield return new WaitForSecondsRealtime(2f);
        text.gameObject.SetActive(false);
    }
}
