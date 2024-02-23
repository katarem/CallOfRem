using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{    
    bool isJumping = false;

    private Rigidbody rigid;

    [SerializeField]
    public int lives = 3;
    
    [SerializeField]
    float jumpForce = 5;

    [SerializeField]
    public static float speed = 5;

    [SerializeField]
    private UI_Manager ui;
    public static Gun gun;

    [SerializeField]
    private ItemSpawner itemSpawner;


    void Start()
    {
        ui.showHp(lives);
        rigid = gameObject.GetComponent<Rigidbody>();
        //gets the selected weapon at the main menu
        setWeapon(gameObject);
    }

    void setWeapon(GameObject gameObject)
    {
        if (PlayerPrefs.GetString("selectedWeapon").ToLower().Equals("sniper"))
        {
            Gun otherWeapon = gameObject.GetComponentInChildren<Pistol>();
            otherWeapon.gameObject.SetActive(false);
            gun = gameObject.GetComponentInChildren<Sniper>();
            ui.setAmmo(gun.maxBullets, gun.maxCharger);
        }
        else if (PlayerPrefs.GetString("selectedWeapon").ToLower().Equals("pistol"))
        {
            Gun otherWeapon = gameObject.GetComponentInChildren<Sniper>();
            otherWeapon.gameObject.SetActive(false);
            gun = gameObject.GetComponentInChildren<Pistol>();
            ui.setAmmo(gun.maxBullets, gun.maxCharger);
        }
    }


    void Update()
    {
        move();
        jump();
    }
    //moves the player depending on the camera rotation
    public void getView(Vector3 angle)
    {
        transform.eulerAngles= new Vector3(transform.eulerAngles.x,angle.y,transform.eulerAngles.z);
    }

    //movement
    void move()
    {
        Vector3 direction = new Vector3(Input.GetAxis("Horizontal"),0,Input.GetAxis("Vertical"));
            transform.Translate(direction * 5 * Time.deltaTime);
    }
    //jumping
    void jump()
    {
        if(Input.GetKeyDown(KeyCode.Space) && !isJumping)
        {
            isJumping = true;
            rigid.AddForce(Vector3.up * jumpForce,ForceMode.Impulse);
        }
    }

    void OnCollisionEnter(Collision other){
        if(other.gameObject.tag == "Ground")
        {
            isJumping = false;
        }
        //these last two ifs are for taking healthkit and ammokit from the floor
        if(other.gameObject.tag == "Health")
        {
            if(lives<3)
            {
                lives++;
                ui.showHp(lives);
                StartCoroutine(itemSpawner.RespawnItem(other.gameObject));
            }
        }
        if(other.gameObject.tag == "Ammo")
        {
            if(gun.leftBullets < gun.maxBullets || gun.leftCharger < gun.maxCharger)
            {
                gun.leftBullets = gun.maxBullets;
                gun.leftCharger = gun.maxCharger;
                ui.setAmmo(gun.maxBullets, gun.maxCharger);
                StartCoroutine(itemSpawner.RespawnItem(other.gameObject));
            }
            
        }
    }
    //calculates player dmg and shows it on the ui
    public void damage(int dmg = 1)
    {
        lives -= dmg;
        ui.showHp(lives);
    }

}
