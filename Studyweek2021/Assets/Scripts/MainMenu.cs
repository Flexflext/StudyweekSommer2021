using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    // Functions for the Main Menu

    [Header("Options")]
    [SerializeField] private GameObject optionsScreen;
    [SerializeField] private Slider volumeSlider;

    [Header("MainMenu")]
    [SerializeField] private GameObject mainMenu;

    private float currentVolume = 1f;
    private bool start;

    private void Start()
    {
        // Set Volume to AudioManager Volume
        volumeSlider.value = AudioManager.Instance.MasterVolume;

        // Play Main Menu Music and reset the volume
    }


    private void Update()
    {
        //Player input to open and Close the Optionsmenu
        if (!optionsScreen.activeSelf)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                OpenOptions();
            }
        }
        else if (optionsScreen.activeSelf)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                CloseOptions();
            }
        }
    }

    /// <summary>
    /// Opens option Menu and Closes normal Main Menu 
    /// </summary>
    public void OpenOptions()
    {
        mainMenu.SetActive(false);
        optionsScreen.SetActive(true);
    }

    /// <summary>
    /// Closes Options and opens Main Menu
    /// </summary>
    public void CloseOptions()
    {
        mainMenu.SetActive(true);
        optionsScreen.SetActive(false);
    }



    /// <summary>
    /// Sets Volume of the audio Manager
    /// </summary>
    /// <param name="_value"></param>
    public void SetVolume(float _value)
    {
        AudioManager.Instance.SetMasterVolume(_value);
    }


    /// <summary>
    /// Quits Game with an Animation
    /// </summary>
    public void QuitGame()
    {
        //UIManager.Instance.QuitApplicationWithAnimation();
        Application.Quit();
    }

    /// <summary>
    /// Changes Scene with an Animation
    /// </summary>
    /// <param name="_idx"></param>
    public void ChangeScene(int _idx)
    {
        //GUIManager.Instance.ChangeSceneWithAnimation(_idx);
        start = true;
    }
}
