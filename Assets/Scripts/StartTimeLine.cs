using UnityEngine;
using UnityEngine.Playables;

public class StartTimeLine : MonoBehaviour
{
    private PlayableDirector timeLine;

    private void Awake()
    {
        timeLine = GetComponent<PlayableDirector>();
        EventController.OnFinishEvent += OnTimeLine;
    }

    private void OnTimeLine()
    {
        timeLine.Play();
    }
}
