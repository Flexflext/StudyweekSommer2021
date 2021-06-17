using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundUpgrade : MonoBehaviour
{
    public TypeOfUpgrade Type;
    public float MaxTime;

    [SerializeField] private float upDownSpeed;
    private float currentUpDownSpeed;
    private Vector3 startPos;

    private void Start()
    {
        currentUpDownSpeed = upDownSpeed;
    }

    private void Update()
    {
        transform.position += Vector3.up * currentUpDownSpeed * Time.deltaTime;

        if (transform.position.y >= startPos.y + .15f)
        {
            currentUpDownSpeed = upDownSpeed * -1f;
        }
        else if (transform.position.y <= startPos.y - .15f)
        {
            currentUpDownSpeed = upDownSpeed;
        }
    }
}
