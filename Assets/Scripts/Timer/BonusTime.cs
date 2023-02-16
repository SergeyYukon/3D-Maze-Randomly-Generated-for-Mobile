using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusTime : MonoBehaviour
{
    [SerializeField] private float bonusTime;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            EventController.OnBonusTime(bonusTime);
            Destroy(gameObject);
        }
    }
}
