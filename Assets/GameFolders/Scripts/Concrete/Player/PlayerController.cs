using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    #region SerializeField
    [Header("|--Player Variables--|")]
    [SerializeField] float playerSpeed;
    [SerializeField] float backwardWaitTime;
    [SerializeField] float _coefficientDeacreaseHealt;


    [Header("|--Swerve Variables--|")]
    [SerializeField] float swerveSpeedX;
    [SerializeField] float maxSwerveAmount;

    [Header("|--Diamond Variables--|")]
    [SerializeField] Transform _diamondRefTransform;
    [SerializeField] float _lerpTime;
    [SerializeField] Canvas _canvas;
    #endregion

    Animator _animator;
    public Slider healtBar;

    #region MonoBehaviourMethods

    #region Encapsulations
    public float CoefficientDeacreaseHealt
    {
        get
        {
            return _coefficientDeacreaseHealt;
        }
        set
        {
            _coefficientDeacreaseHealt = value;
        }
    }

    public float LerpTime
    {
        get
        {
            return _lerpTime;
        }
        set
        {
            _lerpTime = value;
        }
    }

    public Transform DiamondRefTransform
    {
        get
        {
            return _diamondRefTransform;
        }
        set
        {
            _diamondRefTransform = value;
        }
    }
    #endregion


    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    void Update()
    {
        _canvas.planeDistance = 3;
        if (!GameManager.Instance.IsStart || GameManager.Instance.IsGameOver || GameManager.Instance.IsPaused) return;
        _canvas.planeDistance = 100;
        _animator.SetBool(Constants.AnimationsTag.RUN, true);
        GameManager.Instance.PlayerMovement.ForwardMove(this.transform, playerSpeed);
        GameManager.Instance.SwerveMovement.SwerveAmountX(this.transform, swerveSpeedX, maxSwerveAmount);
    }

    #endregion

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Constants.Tag.DIAMOND))
        {
            GameManager.Instance.Diamond += GameManager.Instance.CoefficientMoney * 1;
            StartCoroutine(GameController.Instance.DiamondLerpCoroutine(other.transform, other.gameObject));
        }
        if (other.CompareTag(Constants.Tag.OBSTACLE))
        {
            StartCoroutine(GameController.Instance.HealtDeacrease());
            StartCoroutine(BackwardTime());

        }
        if (other.CompareTag(Constants.AnimationsTag.FINISH_WALL))
        {
            GameManager.Instance.Victory();
        }
    }

    public void Victory()
    {
        _animator.SetBool(Constants.AnimationsTag.RUN,false);
        _animator.SetBool(Constants.AnimationsTag.VICTORY,true);
        transform.Rotate(0, 180, 0);
        transform.position = new Vector3(0, transform.localPosition.y, transform.localPosition.z);
    }

    public void Dead()
    {
        _animator.SetBool(Constants.AnimationsTag.DEAD,true);
    }

    #region IEnumerators

    IEnumerator BackwardTime()
    {
        _animator.SetTrigger(Constants.AnimationsTag.BACKWARD_JUMP);
        playerSpeed *= -1;
        yield return new WaitForSeconds(backwardWaitTime);
        playerSpeed *= -1;
    }


    #endregion

}
