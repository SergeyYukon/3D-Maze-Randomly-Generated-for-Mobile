using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class UI : MonoBehaviour
{
    [SerializeField] private GameObject timeIsOver;
    [SerializeField] private TMP_Text textUI;
    [SerializeField] private TMP_Text textNumbersCompleted;
    [SerializeField] private TMP_Text textNumbersTime;
    [SerializeField] private TMP_Text textNumbersRecord;
    private int record;

    private void Awake()
    {       
        EventController.OnTimeIsOverEvent += TimeIsOver;
    }

    private void Start()
    {
       // PlayerPrefs.DeleteAll();
        textUI.text = "Level " + (GameManager.Level + 1).ToString();
    }

    private void TimeIsOver()
    {
        timeIsOver.SetActive(true);
        Record();
        textNumbersCompleted.text = GameManager.Level.ToString();
        textNumbersTime.text = TimeSpan.FromSeconds(Timer.AllTime).ToString(@"mm\:ss\:ff");
        textNumbersRecord.text = PlayerPrefs.GetInt("Record").ToString();
    }

    private void Record()
    {
        if (GameManager.Level > PlayerPrefs.GetInt("Record", 0)) 
        {
            record = GameManager.Level;
            PlayerPrefs.SetInt("Record", record);
        }
    }
}
