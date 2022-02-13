using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    private static GameController _instance;
    public static GameController Instance => _instance;

    #region MonoBehaviours
    private void Awake()
    {
        _instance = this;
    }

    private void Start()
    {
        GameManager.Instance.PlayerMovement = new PlayerMovement();
        GameManager.Instance.SwerveMovement = new SwerveMovement();
        GameManager.Instance.SwerveInputSystem = GameObject.FindGameObjectWithTag(Constants.Tag.PLAYER).GetComponent<SwerveInputSystem>();
        GameManager.Instance.PlayerController = GameObject.FindGameObjectWithTag(Constants.Tag.PLAYER).GetComponent<PlayerController>();
        GameManager.Instance.CameraController = GameObject.FindGameObjectWithTag(Constants.Tag.MAINCAMERA).GetComponent<CameraController>();
        GameManager.Instance.ParticleEffectSystem = GameObject.FindGameObjectWithTag(Constants.Tag.PLAYER).GetComponent<ParticleEffectSystem>();
        GameManager.Instance.Healt = GameManager.Instance.PlayerController.healtBar.maxValue;
        GameManager.Instance.LevelProgressUI = GameObject.FindGameObjectWithTag(Constants.Tag.LEVEL_BAR).GetComponent<LevelProgressUI>();
        GameManager.Instance.PlayerController.healtBar.value = GameManager.Instance.HealtRefValue;
    }

    #endregion

    #region IEnumerators
    public IEnumerator HealtDeacrease()
    {
        float currentHealt = 35;
        while (currentHealt > 0)
        {
            GameManager.Instance.PlayerController.healtBar.value -= Time.deltaTime * GameManager.Instance.PlayerController.CoefficientDeacreaseHealt;
            currentHealt -= Time.deltaTime * GameManager.Instance.PlayerController.CoefficientDeacreaseHealt;
            yield return null;
        }
        GameManager.Instance.Healt = GameManager.Instance.PlayerController.healtBar.value;
    }


    public IEnumerator DiamondLerpCoroutine(Transform diamonTransform, GameObject diamond)
    {
        float t = .5f;
        while (t > 0)
        {
            t -= Time.deltaTime;
            diamonTransform.position = Vector3.Lerp(diamonTransform.position, GameManager.Instance.PlayerController.DiamondRefTransform.position, GameManager.Instance.PlayerController.LerpTime);
            yield return null;
        }
        Destroy(diamond);

    }
    #endregion

}
