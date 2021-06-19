using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUIManager : MonoBehaviour
{
    public static GUIManager Instance;

    private bool isAlreadyTransitioning;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        if (Instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

    /// <summary>
    /// Loads a New Scene with a Blend Animation
    /// </summary>
    /// <param name="_sceneidx">Sceneindex of Scene you want to Load.</param>
    public void LoadSceneByIndexWithAnimation(int _sceneidx)
    {
        if (!isAlreadyTransitioning)
        {
            BlendAnimation animationBlend = GetComponentInChildren<BlendAnimation>();
            animationBlend.LoadLevelWithAnimation(_sceneidx);
            AudioManager.Instance.StopAllSounds();
            Time.timeScale = 1;
            isAlreadyTransitioning = true;
            StartCoroutine(TimeTillSceneCanLoadAgain());
        }

    }

    /// <summary>
    /// Timer till Another Scene Can be Loadet
    /// </summary>
    /// <returns></returns>
    private IEnumerator TimeTillSceneCanLoadAgain()
    {
        yield return new WaitForSecondsRealtime(2);
        isAlreadyTransitioning = false;
    }
}
