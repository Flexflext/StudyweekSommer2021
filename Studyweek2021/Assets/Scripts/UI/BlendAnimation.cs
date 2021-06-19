using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BlendAnimation : MonoBehaviour
{
    //Code von Felix
    //Purpose: Spielt eine Animation ab wenn der Spieler eine Neue Scene Läd


    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponentInChildren<Animator>();
    }

    /// <summary>
    /// Starts the Belnd Animation
    /// </summary>
    /// <param name="_index"></param>
    public void LoadLevelWithAnimation(int _index)
    {
        StartCoroutine(LoadLevel(_index));
    }

    IEnumerator LoadLevel(int _idx)
    {
        animator.SetTrigger("LoadNewScene");

        yield return new WaitForSeconds(1);

        SceneManager.LoadScene(_idx);
    }
}
