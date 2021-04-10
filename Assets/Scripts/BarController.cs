﻿using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class BarController : MonoBehaviour
{
    public Image moneyBar;
    public GameObject goldPile;
    void Update()
    {
        if (moneyBar.fillAmount >= 0.5f)
            goldPile.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("3");
        else if (moneyBar.fillAmount <= 0.5f && moneyBar.fillAmount >= 0.1f)
            goldPile.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("2");
        else if (moneyBar.fillAmount <= 0)
            goldPile.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("1");
    }

    public void changeBarValue(Image bar)
    {
        bar.fillAmount += 0.1f;
    }
    public void _changeBarValue(Image bar)
    {
        bar.fillAmount -= 0.1f;
    }
}
