using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GfxType
{
    High,
    Low,
}

public class PlayerGFXChange : MonoBehaviour
{
    [SerializeField] private GfxType currentGfx;

    [SerializeField] private SpriteRenderer highGfx;
    [SerializeField] private SpriteRenderer lowGfx;



    private void OnTriggerStay2D(Collider2D collision)
    {

        if (collision.gameObject.layer == 6)
        {
            highGfx.enabled = true;
            lowGfx.enabled = false;

            currentGfx = GfxType.High;
        }

        if (collision.gameObject.layer == 7)
        {

            highGfx.enabled = false;
            lowGfx.enabled = true;

            currentGfx = GfxType.Low;
        }
    }


}
