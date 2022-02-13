using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private static UIManager _instance;
    public static UIManager Instance => _instance;

    #region SerializeField and Public

    [Header("In Game")]
    [SerializeField] TextMeshProUGUI diamond;
    [SerializeField] TextMeshProUGUI level;
    [SerializeField] TextMeshProUGUI lostMoney;
    [SerializeField] GameObject startPanel;
    [SerializeField] GameObject pausePanel;
    [SerializeField] Button pauseButton;
    public GameObject VictoryPanel;
    public GameObject GameOverPanel;


    [Header("Before Game")]

    [SerializeField] Button increaseHealtButton;
    [SerializeField] Button increaseCoefficientMoneyButton;
    [SerializeField] Button marketButton;
    [SerializeField] Button returnMainMenuButton;
    [SerializeField] Button playButton;
    [SerializeField] TextMeshProUGUI maxHealtText;
    [SerializeField] TextMeshProUGUI maxCoefficientMoneyText;
    [SerializeField] TextMeshProUGUI money;
    [SerializeField] TextMeshProUGUI marketTotalMoney;
    [SerializeField] GameObject marketPanel;
    [SerializeField] GameObject menuPanel;

    #endregion




    #region MonoBehaviours
    private void Awake()
    {
        _instance = this;

        if (playButton != null)
        {
            playButton.onClick.AddListener(Play);
        }
        if (increaseHealtButton != null)
        {
            increaseHealtButton.onClick.AddListener(IncreaseHealt);
        }
        if (increaseCoefficientMoneyButton != null)
        {
            increaseCoefficientMoneyButton.onClick.AddListener(IncreaseCoefficient);
        }
        if (marketButton != null)
        {
            marketButton.onClick.AddListener(Market);
        }
        if (returnMainMenuButton != null)
        {
            returnMainMenuButton.onClick.AddListener(ReturnMainMenuButton);
        }
    }
    private void Start()
    {
        if(pauseButton != null)
        {
            pauseButton.gameObject.SetActive(true);
            GameManager.Instance.Diamond = 0;
        }
    }

    private void Update()
    {
        TotalMoney();
        IncreaseCoefficientText();
        IncreaseHealtText();
        if (pausePanel != null)
        {
            if (pausePanel.activeInHierarchy)
            {
                GameManager.Instance.IsPaused = true;
            }
            else
            {
                GameManager.Instance.IsPaused = false;
            }
        }

    }

    #endregion

    #region PublicFunction

    public void Play()
    {
        SceneManager.LoadScene(GameManager.Instance.Level);
    }


    public void StartGame()
    {
        startPanel.SetActive(false);
        pauseButton.gameObject.SetActive(true);
        GameManager.Instance.IsStart = true;
        GameManager.Instance.IsGameOver = false;
    }

    public void PauseGame()
    {
        if (!pausePanel.activeInHierarchy)
        {
            pausePanel.SetActive(true);
        }
        else
        {
            pausePanel.SetActive(false);
        }

    }

    public void LoadCurrentScene()
    {
        SceneManager.LoadScene(GameManager.Instance.Level);
    }

    public void IncreaseHealt()
    {
        if (GameManager.Instance.Money >= 10 && GameManager.Instance.HealtRefValue <= 140)
        {
            GameManager.Instance.Money -= 10;
            GameManager.Instance.HealtRefValue += 10;
            maxHealtText.text = $"Healt \n {GameManager.Instance.HealtRefValue}";
        }
    }

    public void IncreaseCoefficient()
    {
        if (GameManager.Instance.Money >= 10 && GameManager.Instance.CoefficientMoney <= 4)
        {
            GameManager.Instance.Money -= 10;
            GameManager.Instance.CoefficientMoney++;
            maxCoefficientMoneyText.text = $"Money \n X {GameManager.Instance.CoefficientMoney}";
        }
    }
    public void IncreaseCoefficientText()
    {
        if (maxCoefficientMoneyText != null)
        {
            maxCoefficientMoneyText.text = $"Money \n X {GameManager.Instance.CoefficientMoney}";
        }
    }

    public void LostMoneyText()
    {
        lostMoney.text = $"Lost Money : {GameManager.Instance.Diamond}";
    }

    public void IncreaseHealtText()
    {
        if (maxHealtText != null)
        {
            maxHealtText.text = $"Healt \n {GameManager.Instance.HealtRefValue}";
        }
    }

    public void Market()
    {
        menuPanel.SetActive(false);
        marketPanel.SetActive(true);
    }

    public void ReturnMainMenuButton()
    {
        marketPanel.SetActive(false);
        menuPanel.SetActive(true);
    }

    public void ExitApplication()
    {
        Application.Quit();
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void NextLevel()
    {
        SceneManager.LoadScene(GameManager.Instance.Level);
    }

    public void ScoreUpdate(int diamondValue)
    {
        diamond.text = $"{diamondValue}";
    }

    public void TotalMoney()
    {
        if (money != null)
        {
            money.text = $"{GameManager.Instance.Money}";
            marketTotalMoney.text = $"{GameManager.Instance.Money}";

        }
    }

    public void Level()
    {
        level.text = $"Level \n {GameManager.Instance.Level}";
    }
    #endregion

}
