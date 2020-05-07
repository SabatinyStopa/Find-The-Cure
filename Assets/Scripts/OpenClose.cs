using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenClose : MonoBehaviour{
    public Animator animator;

    private void OnTriggerEnter(Collider other){
        if(animator.GetBool("open"))
            animator.SetBool("open", false);
        else
            animator.SetBool("open", true);
    }
}
