using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    [SerializeField] private bool imAMagnet;
    [SerializeField] private float magentRadius;
    [SerializeField] private LayerMask coinLayer;
    [SerializeField] private int possibleJumps;

    [SerializeField] private ParticleSystem invinicleVfx;
    [SerializeField] private ParticleSystem magnetVfx;

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
        UpgradeInfo info = new UpgradeInfo();

        //info = upgrades.Find(upgrade => upgrade.Type == _type);

        foreach (var item in upgrades)
        {
            if (item.Type == _type)
            {
                info = item;
            }
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
                magnetVfx.Play();
                break;
            case TypeOfUpgrade.Invincible:
                info.Type = TypeOfUpgrade.Invincible;
                Physics2D.IgnoreLayerCollision(3, 11, true);
                invinicleVfx.Play();
                break;
            default:
                break;
        }

        PlayerHud.Instance.OpenUpgrade(_type, info.currentTime);
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
                    magnetVfx.Stop();
                    break;
                case TypeOfUpgrade.Invincible:
                    info.Type = TypeOfUpgrade.Invincible;
                    Physics2D.IgnoreLayerCollision(3, 11, false);
                    invinicleVfx.Stop();
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

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, magentRadius);
    }
}
