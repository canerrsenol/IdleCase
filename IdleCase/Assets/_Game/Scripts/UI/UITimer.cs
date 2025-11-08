using TMPro;
using UnityEngine;

public class UITimer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timerText;

    [SerializeField] private GlobalEventsSO globalEventsSO;

    private readonly string timerPrefix = "Remaining Time: ";

    private void OnEnable()
    {
        globalEventsSO.TimerEvents.RemainingTime += UpdateTimer;
    }

    private void OnDisable()
    {
        globalEventsSO.TimerEvents.RemainingTime -= UpdateTimer;
    }

    private void UpdateTimer(int remainingTime)
    {
        timerText.text = timerPrefix + remainingTime.ToString();
    }
}
