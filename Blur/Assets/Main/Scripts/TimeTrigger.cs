using UnityEngine;

public class TimeTrigger : MonoBehaviour
{
    [Header("Trigger Time")]
    public int triggerHour = 14;
    public int triggerMinute = 30;

    private bool triggered = false;

    void OnEnable()
    {
        GameClock.Instance.OnTimeChanged += CheckTime;
    }

    void OnDisable()
    {
        GameClock.Instance.OnTimeChanged -= CheckTime;
    }

    void CheckTime(int hour, int minute)
    {
        if (!triggered && hour == triggerHour && minute == triggerMinute)
        {
            triggered = true;
            TriggerEvent();
        }
    }

    void TriggerEvent()
    {
        Debug.Log($"{gameObject.name} triggered at {triggerHour:D2}:{triggerMinute:D2}");
        // Do your event: enemy spawns, power shuts down, cutscene, etc.
    }
}
