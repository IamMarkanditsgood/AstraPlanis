using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Popup : BasicPopup
{
    [SerializeField] private Button close;

    private void Start()
    {
        close.onClick.AddListener(Close);
    }

    private void OnDestroy()
    {
        close.onClick.RemoveListener(Close);
    }
    public override void ResetPopup()
    {
    }

    public override void SetPopup()
    {
    }

    private void Close()
    {
        UIManager.Instance.ShowScreen(ScreenTypes.Home);
        Hide();
    }
}
