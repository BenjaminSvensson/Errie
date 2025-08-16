using UnityEngine;
public class WorldEndEvent : MonoBehaviour
{
    public int endHour = 18;

    void OnEnable()
    {
        GameClock.Instance.OnTimeChanged += CheckEnd;
    }

    void OnDisable()
    {
        GameClock.Instance.OnTimeChanged -= CheckEnd;
    }

    void CheckEnd(int hour, int minute)
    {
        if (hour >= endHour)
        {
            Debug.Log("💀 World ended!");
            // Fade to black, restart loop, etc.
            GameClock.Instance.OnTimeChanged -= CheckEnd;
        }
    }
}
