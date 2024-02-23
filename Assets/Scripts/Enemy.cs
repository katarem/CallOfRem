using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    private Player player;

    public static int KILLS = 0;

    private AudioSource aud;
    [SerializeField]
    public float speed = 3;
    [SerializeField]
    private float acceleration = 2.5f;
    [SerializeField]
    private int lives = 1;
    private bool playingSound = false;
    // Start is called before the first frame update
    
    public virtual void Start()
    {
        speed = 3; //si no se acumula infinitamente la velocidad tras cada partida
        player = GameObject.Find("Player").GetComponent<Player>();
        aud = gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        move();

        //enemies will tell the player whenever they are close to it
        if(isNear() && !playingSound)
        {
            StartCoroutine(playSound());
        }
            
    }
    //enemies will always go straight to your position
    void move()
    {
        transform.LookAt(player.transform);
        transform.Rotate(new Vector3(90,0,0));
        transform.Translate(new Vector3(0,1,0) * speed * Time.deltaTime, Space.Self);
    }
    void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.tag == "Player")
        {
            player.damage();
            Destroy(this.gameObject);
        }
    }
    //when you kill an enemy with a bullet
    public void getShot()
    {
        lives--;
        if(lives == 0)
        {
            sendKill();
            ParticleSystem p = gameObject.GetComponentInChildren<ParticleSystem>();
            p.Play();
            gameObject.GetComponent<MeshRenderer>().enabled = false;
            Destroy(this.gameObject, 0.8f);
        }
    }

    public virtual void sendKill()
    {
        Enemy.KILLS++;
    }

    //the boolean value for detecting if it's near or not
    bool isNear()
    {
        float diffX = Mathf.Abs(transform.position.x - player.transform.position.x);
        float diffZ = Mathf.Abs(transform.position.z - player.transform.position.z);
        if(diffX+diffZ<=30)
            return true;
        else
            return false;

    }
    //near sound
    IEnumerator playSound()
    {
        playingSound = true;
        aud.Play();
        yield return new WaitForSeconds(5);
        playingSound = false;
    }
    //needed for the spawner class
    public void accelerate()
    {
        this.speed += acceleration;
    }


}
