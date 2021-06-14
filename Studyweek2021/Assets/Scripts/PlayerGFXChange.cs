using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerGfx
{
    High,
    Low,
}

public class PlayerGFXChange : MonoBehaviour
{
    [SerializeField] private PlayerGfx currentGfx;

    [SerializeField] private SpriteRenderer highGfx;
    [SerializeField] private SpriteRenderer lowGfx;

    private void Update()
    {
        highGfx.enabled = false;
    }
}
