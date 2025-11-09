using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class UIWaveText : MonoBehaviour
{
    [SerializeField] private GlobalEventsSO globalEventsSO;
    [SerializeField] private TextMeshProUGUI waveText;

    private void OnEnable()
    {
        globalEventsSO.WaveEvents.OnWaveStarted += HandleWaveStarted;
    }

    private void OnDisable()
    {
        globalEventsSO.WaveEvents.OnWaveStarted -= HandleWaveStarted;
    }

    private void HandleWaveStarted(int waveIndex)
    {
        waveText.text = "Wave " + waveIndex;
        DOTween.To(() => waveText.alpha, x => waveText.alpha = x, 1f, 0.5f)
            .OnComplete(() =>
            {
                DOTween.To(() => waveText.alpha, x => waveText.alpha = x, 0f, 0.5f).SetDelay(1f);
            });
    }
}
