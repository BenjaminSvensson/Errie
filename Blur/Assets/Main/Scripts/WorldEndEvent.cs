using UnityEngine;

public class WorldEndEvent : MonoBehaviour
{
    [Header("End Time")]
    [Range(0, 23)] public int endHour = 23;
    [Range(0, 59)] public int endMinute = 59;

    private void OnEnable()
    {
        GameClock.OnMinuteChanged += CheckEnd;
    }

    private void OnDisable()
    {
        GameClock.OnMinuteChanged -= CheckEnd;
    }

    private void CheckEnd(int hour, int minute)
    {
        if (hour == endHour && minute == endMinute)
        {
            TriggerWorldEnd();
        }
    }

    private void TriggerWorldEnd()
    {
        Debug.Log("World ends here!");
        // TODO: fade to black, game over, cutscene, etc.
    }
}
