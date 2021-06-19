using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;

    [SerializeField] private Transform playerTransform;
    public Vector3 PlayerPosition => playerTransform.position;

    public int TimeRemaining;
    public int CurrentCoinAmount = 60;

    private float time;


    private void Awake()
    {
        if (LevelManager.Instance == null)
        {
            LevelManager.Instance = this;
        }
        else if (LevelManager.Instance != this)
        {
            Destroy(this.gameObject);
        }
    }


    private void Start()
    {
        time = TimeRemaining;
        PlayerHud.Instance.ChangeTimeNum(TimeRemaining);
        PlayerHud.Instance.ChangeCoinNum(CurrentCoinAmount);
    }

    private void Update()
    {
        TimeRemaining = (int)ChangeLevelTime();
        PlayerHud.Instance.ChangeTimeNum(TimeRemaining);

        if (TimeRemaining == 0)
        {
            LevelMenu.Instance.EndGame();
        }
        //PlayerHud.Instance.ChangeCoinNum(CurrentCoinAmount);
    }

    public float ChangeLevelTime()
    {
        time -= Time.deltaTime;

        if (time < 0.5f && time > 0f)
        {
            time = 1;
        }

        return time;
    }
}
