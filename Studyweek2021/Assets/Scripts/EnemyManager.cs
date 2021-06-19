using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager Instance;

    [SerializeField] private GameObject[] upgrades;


    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        if (Instance != this)
            Destroy(gameObject);
    }

    public void SpawnRandomUpgrade(Vector3 _pos)
    {
        Instantiate(upgrades[Random.Range(0, upgrades.Length)], _pos, Quaternion.identity);
    }
}
