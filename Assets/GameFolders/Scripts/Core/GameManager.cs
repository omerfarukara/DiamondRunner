using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance => _instance;

    #region Scripts
    PlayerController _playerController;
    PlayerMovement _playerMovement;
    AnimationController _animationController;
    SwerveInputSystem _swerveInputSystem;
    SwerveMovement _swerveMovement;
    CameraController _cameraController;
    ParticleEffectSystem _particleEffectSystem;
    LevelProgressUI _levelProgressUI;
    #endregion

    #region Fields
    int _diamond;
    float _healt;
    bool _isGameOver;
    bool _isStart;
    bool _isPaused;

    #endregion

    #region Property
    public PlayerController PlayerController
    {
        get
        {
            return _playerController;
        }
        set
        {
            _playerController = value;
        }
    }

    public PlayerMovement PlayerMovement
    {
        get
        {
            return _playerMovement;
        }
        set
        {
            _playerMovement = value;
        }
    }

    public SwerveInputSystem SwerveInputSystem
    {
        get
        {
            return _swerveInputSystem;
        }
        set
        {
            _swerveInputSystem = value;
        }
    }

    public SwerveMovement SwerveMovement
    {
        get
        {
            return _swerveMovement;
        }
        set
        {
            _swerveMovement = value;
        }
    }

    public CameraController CameraController
    {
        get
        {
            return _cameraController;
        }
        set
        {
            _cameraController = value;
        }
    }

    public ParticleEffectSystem ParticleEffectSystem
    {
        get
        {
            return _particleEffectSystem;
        }
        set
        {
            _particleEffectSystem = value;
        }
    }

    public LevelProgressUI LevelProgressUI
    {
        get
        {
            return _levelProgressUI;
        }
        set
        {
            _levelProgressUI = value;
        }
    }
    #endregion

    #region Encapsulation
    public bool IsGameOver
    {
        get
        {
            return _isGameOver;
        }

        set
        {
            _isGameOver = value;
        }
    }

    public bool IsStart
    {
        get
        {
            return _isStart;
        }
        set
        {
            _isStart = value;
        }
    }
    public bool IsPaused
    {
        get
        {
            return _isPaused;
        }
        set
        {
            _isPaused = value;
        }
    }


    public int Money
    {
        get => PlayerPrefs.GetInt(Constants.Prefs.MONEY);
        set
        {
            PlayerPrefs.SetInt(Constants.Prefs.MONEY, value);
        }
    }

    public int Diamond
    {
        get
        {
            return _diamond;
        }
        set
        {
            _diamond = value;
            UIManager.Instance.ScoreUpdate(value);
        }
    }


    public int Level
    {
        get => PlayerPrefs.GetInt(Constants.Prefs.LEVEL);
        set
        {
            PlayerPrefs.SetInt(Constants.Prefs.LEVEL, value);
        }
    }
    public float Healt
    {
        get
        {
            return _healt;
        }
        set
        {
            _healt = value;
            if (value <= 0)
            {
                Dead();
            }
        }
    }

    public int CoefficientMoney
    {
        get => PlayerPrefs.GetInt(Constants.Prefs.COEFFICIENT_MONEY);
        set
        {
            PlayerPrefs.SetInt(Constants.Prefs.COEFFICIENT_MONEY, value);
        }
    }

    public int HealtRefValue
    {
        get => PlayerPrefs.GetInt(Constants.Prefs.HEALT_REF_VALUE);
        set
        {
            PlayerPrefs.SetInt(Constants.Prefs.HEALT_REF_VALUE, value);
        }
    }

    #endregion

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }

        if (HealtRefValue == 0)
        {
            HealtRefValue = 100;
        }
        if (CoefficientMoney == 0)
        {
            CoefficientMoney = 1;
        }
        if (Level == 0)
        {
            Level = 1;
        }
    }

    #region Function
    public void Victory()
    {
        Money += Diamond;
        Diamond = 0;
        Level++;
        IsGameOver = true;
        PlayerController.Victory();
        ParticleEffectSystem.VictoryConfetti();
        CameraController.FinishState();
        UIManager.Instance.Level();
        UIManager.Instance.VictoryPanel.SetActive(true);
        StartCoroutine(LevelProgressUI.LevelBarDecrease());
    }

    public void Dead()
    {
        IsGameOver = true;
        UIManager.Instance.LostMoneyText();
        UIManager.Instance.GameOverPanel.SetActive(true);
        PlayerController.Dead();
        Diamond = 0;
    }
    #endregion
}
