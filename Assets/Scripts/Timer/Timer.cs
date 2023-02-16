using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class Timer : MonoBehaviour
{
    [SerializeField] private float startTimer;
    [SerializeField] private TMP_Text textTimer;
    [SerializeField] private TMP_Text textCollectedBonusTime;
    [SerializeField] private TMP_Text startContdown;
    private static float currentTimer;
    private float collectedBonusTime;
    private float currentBonusTime;
    private bool isCollectBonusTime;
    private bool isTimeOver;
    private bool isTickTimer;
    public static float AllTime{ get; private set; }

    private void Awake()
    {
        EventController.OnBonusTimeEvent += CollectedBonusTime;
        EventController.OnLevelEndEvent += PlusBonusTime;
    }

    private void Start()
    {
        isTimeOver = false;
        isTickTimer = false;
        StartCoroutine("StartCountdown");
        collectedBonusTime = 0;
        if (GameManager.Level == 0)
        { 
            currentTimer = startTimer;
            AllTime = startTimer;
        }
    }

    private void Update()
    {
        if (!GameManager.IsCutScene && !isTimeOver) 
        { 
            MainTimer(); 
        }
        ShowTime();
        if (isCollectBonusTime) BonusTime();
    }

    private void MainTimer()
    {
        if (currentTimer > 0)
        {
            currentTimer -= Time.deltaTime;
        }
        else
        {
            currentTimer = 0;
            EventController.OnTimeIsOver();
            isTimeOver = true;
        }
       
        const float tickSoundTime = 10;
        if (currentTimer < tickSoundTime && currentTimer > 0 && !isTickTimer) 
        { 
            EventController.OnTickSound();
            isTickTimer = true;
        }
    }

    private void ShowTime()
    {
        textTimer.text = TimeSpan.FromSeconds(currentTimer).ToString(@"mm\:ss\:ff");
        textCollectedBonusTime.text = "+ " + collectedBonusTime.ToString("0.00");
    }

    private void CollectedBonusTime(float bonusTime)
    {
        currentBonusTime += bonusTime;
        isCollectBonusTime = true;
    }

    private void BonusTime()
    {
        float coeff = 1.5f;
        if (collectedBonusTime < currentBonusTime)
        { 
            collectedBonusTime += Time.deltaTime * coeff; 
        }
        else
        {
            collectedBonusTime = currentBonusTime;
            isCollectBonusTime = false;
        }
    }

    private void PlusBonusTime()
    {
        AllTime += collectedBonusTime;
        currentTimer += collectedBonusTime;
    }

    private IEnumerator StartCountdown()
    {
        const int wait = 1;
        yield return new WaitForSeconds(wait);
        startContdown.text = "3";
        yield return new WaitForSeconds(wait);
        startContdown.text = "2";
        yield return new WaitForSeconds(wait);
        startContdown.text = "1";
        yield return new WaitForSeconds(wait);
        startContdown.text = "GO!";
        yield return new WaitForSeconds(wait);
        startContdown.text = "";
    }
}
