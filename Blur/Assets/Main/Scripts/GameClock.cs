using UnityEngine;
using System;

public class GameClock : MonoBehaviour
{
    [Header("Clock Settings")]
    public int startHour = 0;
    public int startMinute = 0;
    public float realSecondsPerGameMinute = 1f; // 1 real sec = 1 in-game minute

    public static int CurrentHour { get; private set; }
    public static int CurrentMinute { get; private set; }

    private float timer;

    public static event Action<int, int> OnMinuteChanged;

    private void Start()
    {
        CurrentHour = startHour;
        CurrentMinute = startMinute;
    }

    private void Update()
    {
        timer += Time.deltaTime;

        if (timer >= realSecondsPerGameMinute)
        {
            timer = 0f;
            AdvanceMinute();
        }
    }

    private void AdvanceMinute()
    {
        CurrentMinute++;
        if (CurrentMinute >= 60)
        {
            CurrentMinute = 0;
            CurrentHour++;
            if (CurrentHour >= 24)
                CurrentHour = 0;
        }

        OnMinuteChanged?.Invoke(CurrentHour, CurrentMinute);
    }
}
