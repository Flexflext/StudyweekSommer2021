using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public enum TypeOfUpgrade
{
    Doublejump,
    Magnet,
}

public class Upgrade : MonoBehaviour
{
    [SerializeField] private TMP_Text timeText;
    public float currentTime;
    public TypeOfUpgrade upgradeType;

    private void Start()
    {
        currentTime = 0;
    }

    private void Update()
    {
        if (currentTime > 0)
        {
            currentTime -= Time.deltaTime;
            timeText.text = ((int)currentTime).ToString();
        }
        else
        {
            currentTime = 0;
            this.gameObject.SetActive(false);
        }
    }
}
