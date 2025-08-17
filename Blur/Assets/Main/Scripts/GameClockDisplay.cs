using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class GameClockDisplay : MonoBehaviour
{
    [Tooltip("Assign any number of TMP Texts (UI or 3D).")]
    public List<TMP_Text> clockTexts = new List<TMP_Text>();

    private void OnEnable()
    {
        GameClock.OnMinuteChanged += UpdateClocks;
        UpdateClocks(GameClock.CurrentHour, GameClock.CurrentMinute); // refresh immediately
    }

    private void OnDisable()
    {
        GameClock.OnMinuteChanged -= UpdateClocks;
    }

    private void UpdateClocks(int hour, int minute)
    {
        string formatted = $"{hour:00}:{minute:00}";

        foreach (var text in clockTexts)
        {
            if (text != null)
                text.text = formatted;
        }
    }
}
