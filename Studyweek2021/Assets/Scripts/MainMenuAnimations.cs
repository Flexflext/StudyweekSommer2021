using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuAnimations : MonoBehaviour
{
    [SerializeField] private GameObject clockHead;
    [SerializeField] private Animator animator;
    [SerializeField] private float speed;

    private float maxRot;

    float currentRot;

    private void Update()
    {
        if ((int)maxRot != (int)currentRot)
        {
            if (maxRot >= currentRot)
            {
                currentRot += speed * Time.deltaTime;
            }
            else
            {
                currentRot -= speed * Time.deltaTime;
            }
            
            clockHead.transform.rotation = Quaternion.Euler(0, 0, currentRot);
        }
    }

    public void ChangeClockHeadRotationObj(float _pos)
    {
        
        if (maxRot != _pos)
        {
            maxRot = _pos;
            //clockHead.transform.rotation = Quaternion.Euler(0, 0, maxRot);
            animator.Play("Select NewButtonAnimation", 0, 0);
        }
        
    }
}
