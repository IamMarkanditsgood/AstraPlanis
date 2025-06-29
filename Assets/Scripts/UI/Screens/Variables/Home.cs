using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Home : BasicScreen
{
    [SerializeField] private AvatarManager avatarManager;
    [SerializeField] private Button _profileButton;
    [SerializeField] private Button _infoButton;
    [SerializeField] private Button _infoGodButton;
    [SerializeField] private Button _quizButton;
    [SerializeField] private Button _factsButton;
    [SerializeField] private Button _nextButton;
    [SerializeField] private Button _prevButton;

    [SerializeField] private TMP_Text _name;
    [SerializeField] private TMP_Text _planetName;

    [SerializeField] private Image _god;
    [SerializeField] private string[] _godNames;
    [SerializeField] private Sprite[] _godImages;
    [SerializeField] private Planets[] gods;

    private int _currentGod;

    private void Start()
    {
        PlayerPrefs.DeleteAll();
        _currentGod = 0;

        _profileButton.onClick.AddListener(ProfileButton);
        _infoButton.onClick.AddListener(InfoButton);
        _infoGodButton.onClick.AddListener(GodInfoButton);
        _quizButton.onClick.AddListener(QuizButton);
        _factsButton.onClick.AddListener(FactButton);

        _nextButton.onClick.AddListener(Next);
        _prevButton.onClick.AddListener(Prev);
    }
    private void OnDestroy()
    {
        _profileButton.onClick.RemoveListener(ProfileButton);
        _infoButton.onClick.RemoveListener(InfoButton);
        _infoGodButton.onClick.RemoveListener(GodInfoButton);
        _quizButton.onClick.RemoveListener(QuizButton);
        _factsButton.onClick.RemoveListener(FactButton);
        _nextButton.onClick.RemoveListener(Next);
        _prevButton.onClick.RemoveListener(Prev);
    }

    public override void ResetScreen()
    {
    }

    public override void SetScreen()
    {
        avatarManager.SetSavedPicture();
        ConfigScreen();
    }

    private void ConfigScreen()
    {
        _name.text = PlayerPrefs.GetString("Name", "User Name");

        for (int i = 0; i < gods.Length; i++)
        {
            if (gods[_currentGod] == gods[i])
            {
                _god.sprite = _godImages[i];
                _planetName.text = _godNames[i];
            }
        }
    }

    private void ProfileButton()
    {
        UIManager.Instance.ShowScreen(ScreenTypes.Profile);
    }
    private void InfoButton()
    {
        UIManager.Instance.ShowScreen(ScreenTypes.Info);
    }
    private void GodInfoButton()
    {
        PlanetInfo godInfo =  (PlanetInfo) UIManager.Instance.GetScreen(ScreenTypes.GodInfo);
        godInfo.Init(gods[_currentGod]);

        UIManager.Instance.ShowScreen(ScreenTypes.GodInfo);
    }
    private void QuizButton()
    {
        Quiz godInfo = (Quiz)UIManager.Instance.GetScreen(ScreenTypes.Quiz);
        godInfo.Init(gods[_currentGod]);
        UIManager.Instance.ShowScreen(ScreenTypes.Quiz);
    }
    private void FactButton()
    {
        GodFacts godInfo = (GodFacts)UIManager.Instance.GetScreen(ScreenTypes.Facts);
        godInfo.Init(gods[_currentGod]);
        UIManager.Instance.ShowScreen(ScreenTypes.Facts);
    }
    private void Next()
    {
        if(_currentGod < gods.Length - 1)
            _currentGod++;

        ConfigScreen();
    }
    private void Prev()
    {
        if (_currentGod > 0)
            _currentGod--;

        ConfigScreen();
    }
}
