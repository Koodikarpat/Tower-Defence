﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : TurretBase {

    public Sprite[] sprites;
    private SpriteRenderer renderer;

    // Use this for initialization
    void Start()
    {
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
        gameMaster = GameObject.Find("GameMaster");
        renderer = transform.GetChild(0).GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (target == null)
            return;

        if (target.position.x - transform.position.x >=5)
        {
            renderer.sprite = sprites[2];
        }
        if (target.position.x - transform.position.x <=-5)
        {
            renderer.sprite = sprites[1];
        }
        if(target.position.x - transform.position.x>-5 && target.position.x - transform.position.x <5)
        {
            renderer.sprite = sprites[0];
        }

        if (fireCountdown <= 0f)
        { 
            Shoot();
            fireCountdown = 1f / fireRate;
        }

        fireCountdown -= Time.deltaTime;
    }
}
