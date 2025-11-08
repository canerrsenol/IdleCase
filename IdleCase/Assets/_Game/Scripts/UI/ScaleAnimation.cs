using DG.Tweening;
using UnityEngine;

public class ScaleAnimation : MonoBehaviour
{
    [SerializeField] private float animationDuration = 0.75f;
    [SerializeField] private float scaleMultiplier = 1.1f;    

    void OnEnable()
    {
        transform.localScale = Vector3.zero;

        DOTween.To(() => transform.localScale, x => transform.localScale = x, Vector3.one, animationDuration)
            .SetEase(Ease.OutBack)
            .OnComplete(() =>
            {
                DOTween.To(() => transform.localScale, x => transform.localScale = x, Vector3.one * scaleMultiplier, animationDuration)
                    .SetEase(Ease.InOutSine)
                    .SetLoops(-1, LoopType.Yoyo);
            });
    }

    void OnDisable()
    {
        DOTween.Kill(transform);
    }
}
