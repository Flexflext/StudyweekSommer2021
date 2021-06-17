using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    [SerializeField] private bool imAMagnet;
    [SerializeField] private float magentRadius;
    [SerializeField] private LayerMask coinLayer;
    [SerializeField] private int possibleJumps;

    private PlayerMovement playerMovement;

    private List<UpgradeInfo> upgrades = new List<UpgradeInfo>();

    private void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        UpdateUpgrades();

        if (imAMagnet)
        {
            MagnetizeCoins();
        }
    }

    private void MagnetizeCoins()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, magentRadius, coinLayer);

        foreach (Collider2D collider in colliders)
        {
            Coin coin = collider.GetComponent<Coin>();
            coin.magnetizeToPlayer = true;
        }
    }

    private void CheckUpgrade(TypeOfUpgrade _type, float _time)
    {
        UpgradeInfo info;
        info = upgrades.Find(upgrade => upgrade.Type == _type);

        if (info == null)
        {
            info = new UpgradeInfo();
        }

        info.currentTime = _time;

        switch (_type)
        {
            case TypeOfUpgrade.Doublejump:
                info.Type = TypeOfUpgrade.Doublejump;
                playerMovement.MaxJumps = 2;
                break;
            case TypeOfUpgrade.Magnet:
                info.Type = TypeOfUpgrade.Magnet;
                imAMagnet = true;
                break;
            default:
                break;
        }

        PlayerHud.Instance.OpenUpgrade(_type, _time);
        upgrades.Add(info);
    }

    private void UpdateUpgrades()
    {
        List<UpgradeInfo> toRemove = new List<UpgradeInfo>();

        foreach (UpgradeInfo info in upgrades)
        {
            info.currentTime -= Time.deltaTime;

            if (info.currentTime <= 0)
            {
                toRemove.Add(info);
            }
        }

        foreach (UpgradeInfo info in toRemove)
        {
            upgrades.Remove(info);

            switch (info.Type)
            {
                case TypeOfUpgrade.Doublejump:
                    playerMovement.MaxJumps = 1;
                    break;
                case TypeOfUpgrade.Magnet:
                    imAMagnet = false;
                    break;
                default:
                    break;
            }
        }

        toRemove.Clear();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 10)
        {
            GroundUpgrade upgrade = collision.GetComponent<GroundUpgrade>();
            CheckUpgrade(upgrade.Type, upgrade.MaxTime);

            Destroy(collision.gameObject);
        }
    }
}
