﻿using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameOverPanelUI : MonoBehaviour
{
    public TextMeshProUGUI result;
    public Image img;

    public void SetColor(byte r, byte g, byte b)
    {
        Color32 color = new Color32(r, g, b, 52);
        img.color = color;
    }

    public void OnClick(int choose)
    {
        switch (choose)
        {
            case 0:
                GameController.Instance.StartNewGame(true);
                break;
            case 1:
                HeadPanelController.Instance.startPanel.CloseSubPanels();
                HeadPanelController.Instance.startPanel.ChangeVisibility(true);
                GameController.Instance.wasStart = false;
                break;
        }
    }
}