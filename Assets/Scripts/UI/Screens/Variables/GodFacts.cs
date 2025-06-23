using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GodFacts : BasicScreen
{
    [SerializeField] private Button _backButton;
    [SerializeField] private Button _okButton;

    public Image bg;

    [Serializable]
    public class PlanetFactData
    { 
        public Planets planets;
        public string name;
        public Sprite image;
        public string[] facts;
    }
    [SerializeField] private PlanetFactData[] factsData;

    [SerializeField] private TMP_Text _factInfo;
    [SerializeField] private TMP_Text _godName2;

    [SerializeField] private GameObject _factPanel;
    [SerializeField] private GameObject _timerPanel;
    [SerializeField] private TMP_Text _timer;
    private const string LastClaimTimeKey = "LastClaimTime";
    private TimeSpan rewardCooldown = TimeSpan.FromHours(24);

    private Planets _currentGod;

    private void Start()
    {
        _backButton.onClick.AddListener(BackButton);
        _okButton.onClick.AddListener(BackButton);
    }

    private void OnDestroy()
    {
        _backButton.onClick.RemoveListener(BackButton);
        _okButton.onClick.RemoveListener(BackButton);
    }

    private void Update()
    {
        DateTime lastClaimTime = GetLastClaimTime();
        DateTime nextClaimTime = lastClaimTime + rewardCooldown;
        TimeSpan timeRemaining = nextClaimTime - DateTime.Now;
        _timer.text = $"{timeRemaining.Hours:D2}:{timeRemaining.Minutes:D2}:{timeRemaining.Seconds:D2}";
    }

    public void Init(Planets currentGod)
    {
        _currentGod = currentGod;
    }

    public override void ResetScreen()
    {

    }

    public override void SetScreen()
    {
        DateTime lastClaimTime = GetLastClaimTime();
        DateTime nextClaimTime = lastClaimTime + rewardCooldown;
        TimeSpan timeRemaining = nextClaimTime - DateTime.Now;

        if (timeRemaining <= TimeSpan.Zero)
        {
            _factPanel.SetActive(true);
            _timerPanel.SetActive(false);
            GiveFact();
        }
        else
        {
            
            _factPanel.SetActive(false);
            _timerPanel.SetActive(true);
        }


    }

    private void GiveFact()
    {
        for (int i = 0; i < factsData.Length; i++)
        {
            if (factsData[i].planets == _currentGod)
            {
                _godName2.text = "Interesting Fact\r\nabout " + factsData[i].name;

                _factInfo.text = GetFact(factsData[i]);
                bg.sprite = factsData[i].image;
            }
        }

        PlayerPrefs.SetString(LastClaimTimeKey, DateTime.Now.ToString());
        PlayerPrefs.Save();
    }

    private string GetFact(PlanetFactData planetFactData)
    {
        int random = UnityEngine.Random.Range(0, planetFactData.facts.Length);
       return planetFactData.facts[random];
    }

    private void BackButton()
    {
        UIManager.Instance.ShowScreen(ScreenTypes.Home);
    }
    private DateTime GetLastClaimTime()
    {
        string lastClaimStr = PlayerPrefs.GetString(LastClaimTimeKey, string.Empty);
        return string.IsNullOrEmpty(lastClaimStr) ? DateTime.MinValue : DateTime.Parse(lastClaimStr);
    }
}