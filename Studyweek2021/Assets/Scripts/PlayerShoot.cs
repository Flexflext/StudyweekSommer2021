using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    [SerializeField] private int Damage;
    [SerializeField] private Transform bulletShotPos;
    [SerializeField] private GameObject bulletPrefab;

    [SerializeField] private float bulletResetSpeed;
    [SerializeField] private float bulletSpeed;

    private bool canShootAgain = true;
    private PlayerMovement movement;
    private Vector3 bulletStartPos;
    private float bulletStartSpeed;

    private void Start()
    {
        bulletStartSpeed = bulletSpeed;
        bulletStartPos = bulletShotPos.position;
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
            bulletShotPos.position = bulletShotPos.position;
            bulletSpeed = bulletStartSpeed;
        }
        else if (!movement.isLookingRight)
        {
            bulletShotPos.position = new Vector3(bulletStartPos.x * -1f, bulletShotPos.position.y, bulletShotPos.position.z);
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
        GameObject bullet = Instantiate(bulletPrefab, bulletShotPos.position, Quaternion.identity);

        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();

        rb.AddForce(new Vector2(bulletSpeed, 0));
    }
}
