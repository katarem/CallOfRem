using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Camera : MonoBehaviour
{
    float mouseX = 0f;
    float mouseY = 0f;


    [SerializeField]
    private float sensitivity = 5f;

    [SerializeField]
    UI_Manager ui;

    private Player player;
    void Start()
    {
        //locks the cursor so it is always at the center of the screen
        Cursor.lockState = CursorLockMode.Locked;
        player = gameObject.GetComponentInParent<Player>();

        //load sounds
        AudioListener.volume = PlayerPrefs.GetFloat("volumeAudio", 0.5f);
        var music = ui.GetComponentInChildren<AudioSource>();
        music.volume = PlayerPrefs.GetFloat("musicVolume", 0.5f);
    }

    // Update is called once per frame
    void Update()
    {
        //if the game isn't paused will always calculate which rotation it has to be
        if(!ui.isPaused)
        {
            mouseX += Input.GetAxis("Mouse X") * sensitivity;
            mouseY += Input.GetAxis("Mouse Y") * sensitivity * -1;
            float maxRotation = 60f;
            float minRotation = -60f;
            transform.eulerAngles = new Vector3(Mathf.Clamp(mouseY,minRotation,maxRotation),mouseX,0);
            player.getView(new Vector3(Mathf.Clamp(mouseY,minRotation,maxRotation),mouseX,0));
        }
    }
}
