using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class GameClock : MonoBehaviour
{
    public static GameClock Instance;

    [Header("Clock Settings")]
    public int startHour = 8;
    public int startMinute = 0;
    public float timeScale = 60f; 
    // 1 real second = 1 in-game minute if timeScale = 60

    private int currentHour;
    private int currentMinute;
    private float timer;

    [Header("UI / World Displays")]
    public List<TextMeshProUGUI> uiClocks = new List<TextMeshProUGUI>(); // Canvas TMP
    public List<TextMeshPro> worldClocks = new List<TextMeshPro>(); // 3D TMP

    public delegate void TimeChanged(int hour, int minute);
    public event TimeChanged OnTimeChanged;

    void Awake()
    {
        if (Instance == null) Instance = this;
        currentHour = startHour;
        currentMinute = startMinute;
    }

    void Update()
    {
        timer += Time.deltaTime * timeScale;

        if (timer >= 60f)
        {
            timer -= 60f;
            currentMinute++;

            if (currentMinute >= 60)
            {
                currentMinute = 0;
                currentHour++;
                if (currentHour >= 24) currentHour = 0;
            }

            OnTimeChanged?.Invoke(currentHour, currentMinute);
            UpdateAllDisplays();
        }
    }

    private void UpdateAllDisplays()
    {
        string timeText = $"{currentHour:D2}:{currentMinute:D2}";

        // Update UI TMPs
        foreach (var uiClock in uiClocks)
        {
            if (uiClock != null)
                uiClock.text = timeText;
        }

        // Update 3D TMPs
        foreach (var worldClock in worldClocks)
        {
            if (worldClock != null)
                worldClock.text = timeText;
        }
    }

    public int GetHour() => currentHour;
    public int GetMinute() => currentMinute;
}
