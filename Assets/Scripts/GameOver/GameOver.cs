using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
public class GameOver : MonoBehaviour
{
    [SerializeField]
    private TMP_Text KillsText;

    [SerializeField]
    private TMP_Text TimeText;

    [SerializeField]
    private TMP_Text WeaponText;

    void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        KillsText.text = "Kills: " + PlayerPrefs.GetInt("playerNormalKills") + PlayerPrefs.GetInt("playerBigKills");
        string totalTime = PlayerPrefs.GetString("timeSurvived");
        TimeText.text = $"Time survived: {totalTime}";
        WeaponText.text = "Used weapon: " + PlayerPrefs.GetString("selectedWeapon");
    }

    public void restart()
    {
        SceneManager.LoadScene("MainScene");
    }

    public void mainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

}
