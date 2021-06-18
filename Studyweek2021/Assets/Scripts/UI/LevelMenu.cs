using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelMenu : MonoBehaviour
{
    // Functions for the In Game LevelMenu

    public static LevelMenu Instance;

    // Script for the User Interface in the Level

    [Header("Options")]
    [SerializeField] private GameObject optionsScreen;
    [SerializeField] private Slider volumeSlider;

    [Header("PauseMenu")]
    [SerializeField] private GameObject pauseMenu;


    [SerializeField] private GameObject endMenu;

    private void Awake()
    {
        if (LevelMenu.Instance == null)
        {
            LevelMenu.Instance = this;
        }
        else if (LevelMenu.Instance != this)
        {
            Destroy(this.gameObject);
        }
    }

    private void Start()
    {
        // Change the Volume on the Volume Slider
        volumeSlider.value = AudioManager.Instance.MasterVolume;
    }

    private void Update()
    {
        //Chcke that the Player isnt dead
        if (endMenu.activeSelf)
        {
            return;
        }

        //Check for player input and What Menu to Open when
        if (!pauseMenu.activeSelf && !optionsScreen.activeSelf)
        {
            //Open Pause Menu if pausemenu is not active an options are not open
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                OpenPause();
            }
        }
        else if (!optionsScreen.activeSelf && pauseMenu.activeSelf)
        {
            // Close Pause if options are not open but pause Menu is Open
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                ClosePause();
            }
        }
        else if (optionsScreen.activeSelf && !pauseMenu.activeSelf)
        {
            // Close options if options are open and pausemenu is inactive
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                CloseOptions();
            }
        }
    }

    /// <summary>
    /// Change Volume of the Audiomanager by _value
    /// </summary>
    /// <param name="_value"></param>
    public void ChangeVolumeOnSilder(float _value)
    {
        AudioManager.Instance.SetMasterVolume(_value);
    }

    /// <summary>
    /// Open Options and Diables pauseMenu
    /// </summary>
    public void OpenOptions()
    {
        optionsScreen.SetActive(true);
        pauseMenu.SetActive(false);
    }

    /// <summary>
    /// Closes Options and Opens the Pause Menu
    /// </summary>
    public void CloseOptions()
    {
        optionsScreen.SetActive(false);
        pauseMenu.SetActive(true);
    }

    /// <summary>
    /// Opens the Pause Menu and stops Time
    /// </summary>
    public void OpenPause()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0;
    }

    /// <summary>
    /// Closes the Pause Menu and sets timecale to 1
    /// </summary>
    public void ClosePause()
    {
        pauseMenu.SetActive(false);
    }

    /// <summary>
    /// Returns to the Main Menu
    /// </summary>
    public void ReturnMenu()
    {
        //GUIManager.Instance.ChangeSceneWithAnimation(0);
    }

    /// <summary>
    /// Reloads the Same Scene 
    /// </summary>
    public void Retry()
    {
        //GUIManager.Instance.ChangeSceneWithAnimation(SceneManager.GetActiveScene().buildIndex);
    }

    /// <summary>
    /// End Game by loosing, opens deathMenu and Stops Time
    /// </summary>
    public void EndGame()
    {
        pauseMenu.SetActive(false);
        optionsScreen.SetActive(false);

        endMenu.SetActive(true);
        Time.timeScale = 0;
    }
}
