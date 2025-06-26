using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Info : BasicScreen
{
    [SerializeField] private Button _profileButton;
    [SerializeField] private Button _homeButton;
    [SerializeField] private Button _privacyButton;

    private void Start()
    {
        _profileButton.onClick.AddListener(ProfileButton);
        _homeButton.onClick.AddListener(HomeButton);
        _privacyButton.onClick.AddListener(Privacy);
    }
    private void OnDestroy()
    {
        _profileButton.onClick.RemoveListener(ProfileButton);
        _homeButton.onClick.RemoveListener(HomeButton);
        _privacyButton.onClick.RemoveListener(Privacy);
    }

    public override void ResetScreen()
    {
    }

    public override void SetScreen()
    {
    }

    private void ProfileButton()
    {
        UIManager.Instance.ShowScreen(ScreenTypes.Profile);
    }
    private void HomeButton()
    {
        UIManager.Instance.ShowScreen(ScreenTypes.Home);
    }
    private void Privacy()
    {
        Application.OpenURL("https://www.privacypolicies.com/live/dd4b6f5e-9464-47f2-98ad-54f7b8e2ed5a");
    }
}
