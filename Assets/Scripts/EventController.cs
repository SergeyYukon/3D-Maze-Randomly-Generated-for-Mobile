using System;

public class EventController
{
    public static Action OnSetWidthHeightEvent;
    public static Action OnFinishEvent;
    public static Action OnLevelEndEvent;
    public static Action OnTimeIsOverEvent;
    public static Action<float> OnBonusTimeEvent;
    public static Action OnTickSoundEvent;
    public static Action OnPauseEvent;
    public static Action OnPlayEvent;
    public static Action OnTutorialEvent;

    public static void ResetEvents()
    {
        OnSetWidthHeightEvent = null;
        OnFinishEvent = null;
        OnLevelEndEvent = null;
        OnTimeIsOverEvent = null;
        OnBonusTimeEvent = null;
        OnTickSoundEvent = null;
        OnPauseEvent = null;
        OnPlayEvent = null;
        OnTutorialEvent = null;
    }

    public static void OnSetWidthHeight() => OnSetWidthHeightEvent?.Invoke();

    public static void OnFinish() => OnFinishEvent?.Invoke();

    public static void OnLevelEnd() => OnLevelEndEvent?.Invoke();

    public static void OnTimeIsOver() => OnTimeIsOverEvent?.Invoke();

    public static void OnBonusTime(float bonusTime) => OnBonusTimeEvent?.Invoke(bonusTime);

    public static void OnTickSound() => OnTickSoundEvent?.Invoke();

    public static void OnPause() => OnPauseEvent?.Invoke();

    public static void OnPlay() => OnPlayEvent?.Invoke();

    public static void OnTutorial() => OnTutorialEvent?.Invoke();
}
