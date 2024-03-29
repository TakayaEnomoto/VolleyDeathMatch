﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camShake : MonoBehaviour
{
    public float shakeTimer;
    public Vector2 intensity;
    Vector3 defaultPos;
    public float maxDU;
    public Vector2 maxSA;

    public static camShake me;

    private void Awake()
    {
        if (me != null)
        {
            Destroy(gameObject);
            return;
        }
        me = this;
        defaultPos = transform.position;
    }



    // Update is called once per frame
    void Update()
    {
        if (shakeTimer > 0)
        {
            transform.position = defaultPos + new Vector3(Random.Range(-intensity.x, intensity.x),
                                                          Random.Range(-intensity.y, intensity.y));
            shakeTimer -= Time.deltaTime;

        }
        else if (shakeTimer < 0)
        {
            transform.position = defaultPos;
            shakeTimer = 0;
        }else if(shakeTimer == 0)
        {
            defaultPos = new Vector3 (transform.position.x, transform.position.y, transform.position.z);
        }


    }


    public void ShakeScreen(Vector2 shakeAmount, float duration)
    {
        if (duration >= maxDU)
            duration = maxDU;
        if (shakeAmount.x >= maxSA.x)
            shakeAmount.x = maxSA.x;
        if (shakeAmount.y >= maxSA.y)
            shakeAmount.y = maxSA.y;


        shakeTimer = duration;
        intensity = shakeAmount;


    }
}