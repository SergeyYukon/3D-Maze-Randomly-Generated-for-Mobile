using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Records : MonoBehaviour
{
    [SerializeField] private TMP_Text[] records;
    [SerializeField] private TMP_Text[] namesRecord;
    private Names[] names;

    public void ShowTableRecord()
    {
        names = TableRecords();
        SetPlace();
        ShowPlace();
    }

    private Names[] TableRecords()
    {
        names = new Names[records.Length + 1];
        int minFakeRecord = 0;
        const int fakeRecordCoeff = 4;
        for (int i = 0; i < names.Length; i++)
        {
            names[i] = new Names();
            if (i != names.Length - 1)
            {
                names[i].name = "Other Player";
                names[i].record = minFakeRecord + fakeRecordCoeff;
                minFakeRecord = names[i].record;
            }
            else
            {
                names[i].name = "You";
                names[i].record = PlayerPrefs.GetInt("Record", 0);
            }
        }
        return names;
    }

    private void SetPlace()
    {
        Names temp = new Names();
        for (int i = 0; i < names.Length; i++)
        {
            for (int j = 0; j < names.Length - i - 1; j++)
            {
                if(names[j].record < names[j + 1].record)
                {
                    temp = names[j];
                    names[j] = names[j + 1];
                    names[j + 1] = temp;
                }
            }
        }
    }

    private void ShowPlace()
    {
        for (int i = 0; i < names.Length - 1; i++)
        {
            namesRecord[i].text = names[i].name;
            records[i].text = names[i].record.ToString();
        }
    }
}
