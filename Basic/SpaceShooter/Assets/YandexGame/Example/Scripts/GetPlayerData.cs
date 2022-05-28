using UnityEngine;
using UnityEngine.UI;
using YG;

public class GetPlayerData : MonoBehaviour
{
    [SerializeField] ImageLoadYG imageLoad;
    [SerializeField] Text textPlayerData;
    [SerializeField] Text textEnvirData;

    private void OnEnable()
    {
        YandexGame.GetDataEvent += DebugData;
    }
    private void OnDisable()
    {
        YandexGame.GetDataEvent -= DebugData;
    }

    private void Start()
    {
        if (YandexGame.StartGame)
        {
            DebugData();
        }
    }

    void DebugData()
    {
        textPlayerData.text = "playerName - " + YandexGame.PlayerName +
            "\n\nplayerId - " + YandexGame.PlayerId +
            "\n\nauth - " + YandexGame.Auth +
            "\nstartGame - " + YandexGame.StartGame +
            "\nadBlock - " + YandexGame.AdBlock +
            "\ninitializedLB - " + YandexGame.InitializedLB +
            "\nphotoSize - " + YandexGame.PhotoSize;

        if (imageLoad != null && YandexGame.Auth)
            imageLoad.Load(YandexGame.PlayerPhoto);

        textEnvirData.text = "domain - " + YandexGame.EnvironmentData.domain +
            "\ndeviceType - " + YandexGame.EnvironmentData.deviceType +
            "\nisMobile - " + YandexGame.EnvironmentData.isMobile +
            "\nisDesktop - " + YandexGame.EnvironmentData.isDesktop +
            "\nisTablet - " + YandexGame.EnvironmentData.isTablet +
            "\nisTV - " + YandexGame.EnvironmentData.isTV;
    }
}
