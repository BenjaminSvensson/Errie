using UnityEngine;

public class TimeTrigger : MonoBehaviour
{
    [Header("Target Time")]
    [Range(0, 23)] public int targetHour;
    [Range(0, 59)] public int targetMinute;

    public UnityEngine.Events.UnityEvent onTimeReached;

    private void OnEnable()
    {
        GameClock.OnMinuteChanged += CheckTime;
    }

    private void OnDisable()
    {
        GameClock.OnMinuteChanged -= CheckTime;
    }

    private void CheckTime(int hour, int minute)
    {
        if (hour == targetHour && minute == targetMinute)
        {
            onTimeReached.Invoke();
        }
    }
}
