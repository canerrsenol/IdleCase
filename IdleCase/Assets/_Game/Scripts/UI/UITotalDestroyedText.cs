using DG.Tweening;
using TMPro;
using UnityEngine;

public class UITotalDestroyedText : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI totalDestroyedText;

    private readonly string baseText = "Total Destroyed : ";

    void OnEnable()
    {
        int totalDestroyed = SaveManager.Instance.playerProgress.totalDefeatedEnemyCount;
        int x = 0;
        DOTween.To(() => x, value =>
        {
            x = value;
            totalDestroyedText.text = baseText + x.ToString();
        }, totalDestroyed, 1f).SetEase(Ease.Linear);
    }
}
