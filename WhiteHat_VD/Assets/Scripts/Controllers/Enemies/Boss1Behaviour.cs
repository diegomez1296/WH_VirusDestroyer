﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss1Behaviour : MonoBehaviour {

    [Header("Enemy Controls")]
    public bool isMoving;
    public bool isMortal;
    public bool isShooting;

    [Header("Enemy Options")]
    public float speedOfRotation;
    public float speedOfMoving;

    private PlayerController player;
    private BulletEnemyTowerController enemyBullet;
    private float actualShotTime;

    //Moving
    private int movingDirection = 1;

    //PlayerFollowing
    private Vector2 direction;
    private float angle;
    private Quaternion rotation;



    private void Start() {
        actualShotTime = ShootTime();
        enemyBullet = GetComponentInChildren<BulletEnemyTowerController>();
        enemyBullet.gameObject.SetActive(false);
        player = FindObjectOfType<PlayerController>();
    }

    // Update is called once per frame
    void Update() {
        if (isMoving) PlayerFollowing();
        if (isShooting) Shooting();
    }

    private void PlayerFollowing() {
        direction = -player.transform.position + this.transform.position;
        angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, speedOfRotation * Time.deltaTime);
    }

    private void Moving() {
        Vector3 direction = new Vector3(0, movingDirection, 0);
        transform.Translate(direction * speedOfMoving * Time.deltaTime);
    }

    private void Shooting() {
        if (actualShotTime <= 0) {
            var copyEnemyBullet = Instantiate(enemyBullet, this.transform.parent);
            copyEnemyBullet.transform.position = enemyBullet.transform.position;
            copyEnemyBullet.transform.rotation = enemyBullet.transform.rotation;
            copyEnemyBullet.isMoving = true;
            copyEnemyBullet.gameObject.SetActive(true);
            actualShotTime = ShootTime();
        }
        else
            actualShotTime -= Time.deltaTime;
    }

    private float ShootTime() {
        float shootTime = GetRandomValue();
        return shootTime;
    }

    private float GetRandomValue() {
        float randValue = Random.Range(2.0f, 6.0f);
        return randValue >= 1 ? randValue : 1;
    }
}
