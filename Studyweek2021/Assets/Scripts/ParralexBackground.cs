using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParralexBackground : MonoBehaviour
{
    [Header("Reset Speed")]
    public Vector2 ParallexEffectMulltiplier;
    //public float YMovement;

    private Transform cameraTransform;
    private Vector3 lastCameraPostion;
    private float textureUnitSizeX;

    // Start is called before the first frame update
    void Start()
    {
        cameraTransform = Camera.main.transform;
        lastCameraPostion = cameraTransform.position;

        Sprite sprite = GetComponent<SpriteRenderer>().sprite;
        Texture2D texture = sprite.texture;
        textureUnitSizeX = texture.width / sprite.pixelsPerUnit;
    }

    private void LateUpdate()
    {
        Vector3 deltaMovement = cameraTransform.position - lastCameraPostion;
        transform.position += new Vector3(deltaMovement.x * ParallexEffectMulltiplier.x, deltaMovement.y * ParallexEffectMulltiplier.y);

        //cameraTransform.position = new Vector3(cameraTransform.position.x, -3, cameraTransform.position.z);
        lastCameraPostion = cameraTransform.position;

        if (Mathf.Abs(cameraTransform.position.x - transform.position.x) >= textureUnitSizeX)
        {
            float offsetPositionX = (cameraTransform.position.x - transform.position.x) % textureUnitSizeX;
            transform.position = new Vector3(cameraTransform.position.x + offsetPositionX, transform.position.y);
        }
    }
}
