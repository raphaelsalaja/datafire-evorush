// Copyright (c) 2016 Unity Technologies. MIT license - license_unity.txt
// #NVJOB Alpha Flashing UI. MIT license - license_nvjob.txt
// #NVJOB Nicholas Veselov - https://nvjob.github.io
// #NVJOB Alpha Flashing UI v2.3 - https://nvjob.github.io/unity/alpha-flashing-text

using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[HelpURL("https://nvjob.github.io/unity/alpha-flashing-text")]
[AddComponentMenu("Tools/AlphaFlashingUI/AlphaFlashingText")]

///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
public class AlphaFlashingText : MonoBehaviour
{
    private float[] chAlpha;

    //---------------------------------

    private Color[] currentColor;
    private int textCount;
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    [Header("Settings")]
    public List<TextAlpha> textList;
    private Text[] thisText;

    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    private void Awake()
    {
        //---------------------------------

        textCount = textList.Count;

        //---------------------------------

        currentColor = new Color[textCount];
        thisText = new Text[textCount];
        chAlpha = new float[textCount];

        //---------------------------------

        for (var num = 0; num < textCount; num++)
        {
            thisText[num] = textList[num].text.GetComponent<Text>();
            currentColor[num] = thisText[num].color;
        }

        //---------------------------------
    }

    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    private void LateUpdate()
    {
        //---------------------------------

        for (var num = 0; num < textCount; num++)
        {
            chAlpha[num] = textList[num].minAlpha + Mathf.PingPong(Time.time / textList[num].timerAlpha, textList[num].maxAlpha - textList[num].minAlpha);
            thisText[num].color = new Color(currentColor[num].r, currentColor[num].g, currentColor[num].b, Mathf.Clamp(chAlpha[num], 0.0f, 1.0f));
        }

        //---------------------------------
    }

    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
}

///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

[Serializable]
public class TextAlpha
{
    public float maxAlpha = 0.9f;
    public float minAlpha = 0.1f;
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    public Text text;
    public float timerAlpha = 1.0f;

    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
}