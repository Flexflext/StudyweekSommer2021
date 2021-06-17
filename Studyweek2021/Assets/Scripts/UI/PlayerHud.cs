using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerHud : MonoBehaviour
{
    public static PlayerHud Instance;

    [Header("Upgrades")]
    [SerializeField] private Upgrade[] upgrades;

    [Header("Hud Elements")]
    [SerializeField] private TMP_Text coinNum;
    [SerializeField] private TMP_Text timeText;


    private void Awake()
    {
        if (PlayerHud.Instance == null)
        {
            PlayerHud.Instance = this;
        }
        else if (PlayerHud.Instance != this)
        {
            Destroy(this.gameObject);
        }
    }


    public void ChangeCoinNum(int _num)
    {
        coinNum.text = _num.ToString();
    }

    public void ChangeTimeNum(int _currentNum)
    {
        timeText.text = _currentNum.ToString();
    }

    public void OpenUpgrade(TypeOfUpgrade _type, float _time)
    {
        Upgrade upgrade = System.Array.Find(upgrades, type => type.upgradeType == _type);

        upgrade.currentTime = _time;
        upgrade.gameObject.SetActive(true);
    }
}
