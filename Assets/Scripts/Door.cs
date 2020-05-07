using UnityEngine.UI;
using UnityEngine;

public class Door : MonoBehaviour{
    public float[] m_Targets = { 90.0f, 0.0f };
    public int m_Index = 0;
    public HingeJoint m_Joint, m_Joint2;
    public bool Locked = true, doubleDoor = false, playerNear, isThePlayer;
    public Text text;
    public GameObject cross;

    void OnTriggerEnter(Collider other) {
        if(other.gameObject.CompareTag("Player")){
            isThePlayer = true;
            playerNear = true;
        }
    }

    void Update() {
        if(!playerNear || !isThePlayer)
            return;

        if(Input.GetButtonDown("Fire1") && isThePlayer && !Locked){
            OpenDoor();
        }else if(Input.GetButtonDown("Fire1") && isThePlayer && Locked){
            LockedDoor();
        }
    }
    void OnTriggerExit(Collider other) {
        text.gameObject.SetActive(false);
        playerNear = false;
        isThePlayer = false;
    }
    void LockedDoor(){
        text.gameObject.SetActive(true);
        bool HasKey = false;
        
        foreach (GameObject playerItem in FindObjectOfType<Player>().items){
            if(playerItem != null){
                Item itemScript = playerItem.GetComponent<Item>();
                if(playerItem != null){
                    if(itemScript.key && cross.GetComponent<Item>().keyColor == itemScript.keyColor)
                        HasKey = true;
                }
            }
        }

        if(!HasKey)
            text.text = "O ármario está trancado";
        else
            UnlockDoor();
            
    }
    void OpenDoor(){
        if(doubleDoor){
            SpringOpen(m_Joint2);
            m_Index = ++m_Index % m_Targets.Length;
        }

        SpringOpen(m_Joint);
    }

    void SpringOpen(HingeJoint joint){
        JointSpring spring = joint.spring;
        m_Index = ++m_Index % m_Targets.Length;
        spring.targetPosition = m_Targets[m_Index];
        joint.spring = spring;
    }
    public void UnlockDoor(){
        Player playerScript = FindObjectOfType<Player>();
        playerScript.bagCounter--;
        text.text = "Armário destrancado";
        Locked = false;
        OpenDoor();
        for(int i = 0; i < playerScript.items.Length; i++){
            if(playerScript.items[i] != null){
                if(playerScript.items[i].GetComponent<Item>().keyColor == cross.GetComponent<Item>().keyColor){
                    Destroy(playerScript.items[i]);
                    playerScript.items[i] = null;
                    playerScript.keysImages[i].SetActive(false);
                    return;
                }
            }
        }
    }
}