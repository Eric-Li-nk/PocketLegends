using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UITools : MonoBehaviour
{
    public TextMeshProUGUI FPSText;
    public TextMeshProUGUI TimerText;

    private float timer = 0f;
    private float updateTimer;
    [SerializeField]
    private float updateFrequency = 0.5f;
    private int lastFrameIndex;
    private float[] frameDeltaTime;

    private void Awake()
    {
        frameDeltaTime = new float[50];
        updateTimer = updateFrequency;
    }

    void Update()
    {
        timer += Time.deltaTime;
        UpdateTimer();
        UpdateFPSText();
    }

    private void UpdateFPSText()
    {
        int frameDeltaTimeArrayLength = frameDeltaTime.Length;
        
        frameDeltaTime[lastFrameIndex] = Time.unscaledDeltaTime;
        lastFrameIndex = (lastFrameIndex + 1) % frameDeltaTimeArrayLength;
        
        float total = 0f;
        foreach (float t in frameDeltaTime)
            total += t;
        int fps = (int)(frameDeltaTimeArrayLength / total);

        updateTimer += Time.unscaledDeltaTime;
        if (updateTimer >= updateFrequency)
        {
            FPSText.SetText("FPS : " + fps.ToString());
            updateTimer = 0;
        }
        

    }

    private void UpdateTimer()
    {
        float minute = Mathf.FloorToInt(timer / 60f);
        float second = Mathf.FloorToInt(timer % 60f);
        TimerText.SetText(minute.ToString("00") + ":" + second.ToString("00"));
    }
}
