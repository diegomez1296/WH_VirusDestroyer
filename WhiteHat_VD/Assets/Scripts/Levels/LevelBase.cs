﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelBase : MonoBehaviour
{
    public const float DEFAULT_ORTOGRAPHIC_SIZE = 6.5f;
    [Header("Camera options")]
    public bool isAnimation;
    public float speedOfAnim = 0.1f;
    public float ortographicSize;
    [Space]
    [Header("Background Image options")]
    public Sprite bgSprite;
    public Vector2 bgScale;

    protected virtual void Start()
    {
        Camera.main.orthographicSize = DEFAULT_ORTOGRAPHIC_SIZE;
    }

    private void OnEnable()
    {
        if(bgSprite != null)
        {
            GameController.Instance.SetCurrentBackground(bgSprite, bgScale);
        }
    }
    private void Update()
    {
        if (isAnimation)
        {
            if (ortographicSize > DEFAULT_ORTOGRAPHIC_SIZE)
            {
                Camera.main.orthographicSize += speedOfAnim * Time.deltaTime;
                if (Camera.main.orthographicSize >= ortographicSize)
                    isAnimation = false;
            }
            else
            {
                Camera.main.orthographicSize -= speedOfAnim * Time.deltaTime;
                if (Camera.main.orthographicSize <= ortographicSize)
                    isAnimation = false;
            }
        }
    }

}
