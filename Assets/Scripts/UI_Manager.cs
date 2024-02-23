using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class UI_Manager : MonoBehaviour
{
    Canvas canvas;
    [SerializeField]
    private Spawner spawner;

    [SerializeField]
    private Texture red;
    [SerializeField]
    private Texture black;

    [SerializeField]
    private Text scoreText;
    [SerializeField]
    private Text ammoText;
    [SerializeField]
    private GameObject mainui;

    float startTime;

    public bool isPaused = false;
    AudioSource aud;

    void Start()
    {
        BigEnemy.KILLS = 0;
        Enemy.KILLS = 0;
        aud = gameObject.GetComponentInChildren<AudioSource>();
        aud.volume = PlayerPrefs.GetFloat("musicVolume",0.5f);
        Debug.Log($"musica={PlayerPrefs.GetFloat("musicVolume")}\ngeneral={PlayerPrefs.GetFloat("volumeAudio")}");
        AudioListener.volume = PlayerPrefs.GetFloat("volumeAudio", 0.5f);


        startTime = Time.time;
        canvas = gameObject.GetComponentInChildren<Canvas>();
        StartCoroutine(countKills());
        StartCoroutine(giveRewards());
        
    }

    void Update()
    {
        if(isPaused && Input.GetKeyDown(KeyCode.Escape))
        {
            Time.timeScale = 1;
            Cursor.lockState = CursorLockMode.Locked;
            aud.UnPause();
            isPaused = false;
        }
            
        else if(Input.GetKeyDown(KeyCode.Escape))
            {
                Time.timeScale = 0;
                Cursor.lockState = CursorLockMode.None;
                aud.Pause();
                isPaused = true;
            }

    }
    
    public void setAmmo(int bullets, int charger)
    {
        ammoText.text = bullets + "/" + charger;
    }

    public void showHp(int hp)
    {
        if (hp < 1) lost();
        var lives = GameObject.FindGameObjectsWithTag("Heart");
        if(hp < lives.Length)
        {
            for (int i = lives.Length - 1; i > hp; i--)
            {
                var vida = lives[i].GetComponentInChildren<RawImage>();
                vida.texture = black;
            }
        }

        for (int i = 0; i < hp; i++)
        {
            var vida = lives[i].GetComponentInChildren<RawImage>();
            vida.texture = red;
        }

    }

    IEnumerator giveRewards()
    {
        while((Enemy.KILLS + BigEnemy.KILLS) < 30)
        {
            yield return new WaitForSeconds(1);
        }
        Player.speed += 3;
        while ((Enemy.KILLS + BigEnemy.KILLS) < 70)
        {
            yield return new WaitForSeconds(1);
        }
        Player.gun.leftBullets = Player.gun.maxBullets;
        Player.gun.leftCharger = Player.gun.maxCharger;
    }


    IEnumerator countKills()
    {
        var totalKills = Enemy.KILLS + BigEnemy.KILLS;
        scoreText.text = "Kills: " + totalKills;
        yield return new WaitForEndOfFrame();
    }

    void lost()
    {
        PlayerPrefs.SetInt("playerNormalKills", Enemy.KILLS);
        PlayerPrefs.SetInt("playerBigKills", BigEnemy.KILLS);
        PlayerPrefs.SetString("timeSurvived", getTime());
        SceneManager.LoadScene("GameOver");
    }

    string getTime()
    {
        var tiempoFloat = Time.time - startTime;
        var hours = (int)(tiempoFloat / 360); //imposible, pero why not
        var mins = (int)(tiempoFloat / 60);
        var segs = (int)(tiempoFloat % 60);
        return $"{hours.ToString("D2")}:{mins.ToString("D2")}:{segs.ToString("D2")}";
    }


}
