using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private Rigidbody rbPlayer;
    [SerializeField] private GameObject rollingSoundObject;
    [SerializeField] private AudioSource rollingSound;
    [SerializeField] private AudioSource bubbleBonusSound;
    [SerializeField] private AudioSource timeIsOverSound;
    [SerializeField] private AudioSource timerTickSound;

    private void Awake()
    {
        rbPlayer = rbPlayer.GetComponent<Rigidbody>();
        EventController.OnBonusTimeEvent += CollectBonusSound;
        EventController.OnTickSoundEvent += TimerTickPlay;
        EventController.OnTimeIsOverEvent += TimeIsOver;
        EventController.OnFinishEvent += TimerTickPause;
    }

    private void Update()
    {
        PlayerRollingSound();
    }

    private void CollectBonusSound(float bonusTime)
    {
        bubbleBonusSound.pitch = Random.Range(0.9f, 1.1f);
        bubbleBonusSound.Play();
    }

    private void TimerTickPlay() => timerTickSound.Play();

    private void TimerTickPause() => timerTickSound.Pause();

    private void TimeIsOver()
    {
        timerTickSound.Pause();
        timeIsOverSound.Play();
    }

    private void PlayerRollingSound()
    {
        const float silentVolume = 0.25f;
        if (rbPlayer.velocity.magnitude > 1f)
        {
            rollingSoundObject.SetActive(true);
            rollingSound.pitch = Random.Range(0.9f, 1.1f);
            if (rbPlayer.velocity.magnitude < 5f)
            {
                if (rollingSound.volume < silentVolume) rollingSound.volume += Time.deltaTime;
                else if (rollingSound.volume > silentVolume) rollingSound.volume -= Time.deltaTime;
            }
            else if (rbPlayer.velocity.magnitude > 5f)
            {
                rollingSound.volume += Time.deltaTime;
            }
        }
        else
        {
            rollingSound.volume -= Time.deltaTime;
            if (rollingSound.volume == 0) rollingSoundObject.SetActive(false);
        }
    }
}
