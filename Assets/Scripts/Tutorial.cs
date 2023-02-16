using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    [SerializeField] private GameObject tutorial;
    private bool isTutorial;

    private void Update()
    {
        if (!GameManager.IsCutScene && !isTutorial)
        {
            EventController.OnTutorial();
            tutorial.SetActive(true);
            isTutorial = true;
        }
    }

    public void Continue()
    {
        EventController.OnTutorial();
    }

}
