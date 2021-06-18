using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    [SerializeField] private int Damage;
    [SerializeField] private Transform bulletShotPosRight;
    [SerializeField] private Transform bulletShotPosLeft;
    [SerializeField] private GameObject bulletPrefab;

    [SerializeField] private float bulletResetSpeed;
    [SerializeField] private float bulletSpeed;

    private bool canShootAgain = true;
    private PlayerMovement movement;
    private Transform bulletStartPos;
    private float bulletStartSpeed;

    private void Start()
    {
        bulletStartSpeed = bulletSpeed;
        movement = GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && canShootAgain)
        {
            StopAllCoroutines();
            canShootAgain = false;
            Shoot();
            StartCoroutine(C_ResetAttack());
        }

        Animations();
    }

    private void Animations()
    {
        if (movement.isLookingRight)
        {
            bulletStartPos = bulletShotPosRight;
            bulletSpeed = bulletStartSpeed;
        }
        else if (!movement.isLookingRight)
        {
            bulletStartPos = bulletShotPosLeft;
            bulletSpeed = bulletStartSpeed * -1f;
        }
    }



    IEnumerator C_ResetAttack()
    {
        yield return new WaitForSeconds(bulletResetSpeed);
        canShootAgain = true;
    }


    private void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, bulletStartPos.position, Quaternion.identity);

        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();

        rb.AddForce(new Vector2(bulletSpeed, 0));
    }
}
