using UnityEngine;
using UnityEngine.UI;
//using PrimeTween;

public class SplashSceneManager : MonoBehaviour
{
    [SerializeField] private Image fillImage;
    [SerializeField] private float loadingDuration = 2f;
    [SerializeField] private GameObject loadingScreen;

//     private void Start()
//     {
//         // Başlangıçta bar'ı sıfırla
//         fillImage.fillAmount = 0;

//         Tween fillTween = Tween.Custom(
//     fillImage.fillAmount,
//     1f,
//     loadingDuration,
//     value => fillImage.fillAmount = value,
//     Ease.OutCubic
// ).OnComplete(() =>
// {
//     loadingScreen.SetActive(false);
//     LevelManager.I.LoadLevel();
// });
//     }
}
