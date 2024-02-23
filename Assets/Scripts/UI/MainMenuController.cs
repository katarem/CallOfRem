using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MainMenuController : MonoBehaviour
{
    [SerializeField]
    private GameObject selectWeapon;
    public void select()
    {
        selectWeapon.gameObject.SetActive(true);
    }

    public void play(string selectedWeapon)
    {
        PlayerPrefs.SetString("selectedWeapon", selectedWeapon);
        SceneManager.LoadScene("MainScene");
    }

    public void back()
    {
        selectWeapon.gameObject.SetActive(false);
    }

    public void exit()
    {
        Application.Quit();
    }

}
