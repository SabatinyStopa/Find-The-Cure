using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour{
    public CharacterController controller;
    public Camera playerCamera;
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    public float speed = 12f;
    public float gravity = -9.81f;
    public float jumpHeight = 3f;
    public float pushPower = 2.0f;
    public GameObject[] items = new GameObject[5];
    public GameObject[] keysImages;
    public Text text;
    public Slider slider;
    public GameController gameController;
    public float life = 1f;
    public bool infected = false;
    public int medicineCounter = 0;
    public int bagCounter = 0;
    Vector3 velocity;
    bool isGrounded;
    
    
    void Update(){
        movement();
        if(Input.GetKeyDown("escape") && !gameController.gameOver)
            gameController.PauseGame();

        slider.value = life;
    }
    void FixedUpdate(){
        lifeControl();
        collectRaycast();
    }

    void keysOnScreen(){
        Color red =  new Color32(244, 67, 54, 100);
        Color pink = new Color32(233, 30, 99, 100);
        Color blue = new Color32(3, 169, 244, 100);
        Color green = new Color32(76, 175, 80, 100);
        Color purple = new Color32(156, 39, 176, 100);

        for(int i = 0; i < items.Length; i++){
            if(items[i] != null){
                string itemKeyColor = items[i].GetComponent<Item>().keyColor;
                if(itemKeyColor == "red")
                    keysImages[i].GetComponent<Image>().color = red;
                else if(itemKeyColor == "pink")
                    keysImages[i].GetComponent<Image>().color = pink;
                else if(itemKeyColor == "blue")
                    keysImages[i].GetComponent<Image>().color = blue;
                else if(itemKeyColor == "green")
                    keysImages[i].GetComponent<Image>().color = green;
                else if(itemKeyColor == "purple")
                    keysImages[i].GetComponent<Image>().color = purple;
            }
                 
        }
    }

    void lifeControl(){
        if(life > 1)
            life = 1;
    }

    void collectRaycast(){
        float x = Screen.width / 2;
        float y = Screen.height / 2;

        RaycastHit hit;
        
        Ray ray = playerCamera.ScreenPointToRay(new Vector3(x, y, 0));

        Vector3 forward = transform.TransformDirection(Vector3.forward);

        bool colectItem = Physics.Raycast(ray, out hit, 6);

        if (colectItem && hit.collider.gameObject.CompareTag("CollectableItem")){
            text.gameObject.SetActive(true);
            text.text = "Colete " + hit.collider.gameObject.GetComponent<Item>().Name + " pressionando E";
            if(Input.GetKeyDown("e"))
                CollectItem(hit.collider.gameObject);
            text.GetComponent<UiText>().textFadeOut();
        }
    }
    void movement(){
        if(gameController.tutorial || gameController.gameOver || gameController.paused)
            return;

        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if(isGrounded && velocity.y < 0)
            velocity.y = -2f;
        
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z ;

        controller.Move(move * speed * Time.deltaTime);

        if(Input.GetButtonDown("Jump") && isGrounded)
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
    
        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime); 
    }
    void CollectItem(GameObject item){
        Item itemScript =  item.GetComponent<Item>();
        string item_name = itemScript.Name;

        if(itemScript.key && bagCounter >= 5){
            text.gameObject.SetActive(true);
            text.text = "Bolsa cheia";
            text.GetComponent<UiText>().textFadeOut();
            return;
        }

        item.SetActive(false);
        text.gameObject.SetActive(true);
        text.text = "Você coletou " + item_name;
        text.GetComponent<UiText>().textFadeOut();

        if(itemScript.key){
            for(int i = 0; i < items.Length; i++){
                if(items[i] == null){
                    bagCounter++;
                    items[i] = item;
                    keysImages[i].SetActive(true);
                    keysOnScreen();
                    return;
                }
            }
        }else{
            itemScript.DoCure();
            itemScript.IncreaseTime(20f);
            gameController.collectedMedicine++;
            Destroy(item);
        }
    }
    void OnControllerColliderHit(ControllerColliderHit hit){
        Rigidbody body = hit.collider.attachedRigidbody;

        if (body == null || body.isKinematic)
            return;

        if (hit.moveDirection.y < -0.3)
            return;

        Vector3 pushDir = new Vector3(hit.moveDirection.x, 0, hit.moveDirection.z);

        body.velocity = pushDir * pushPower;
    }
}
