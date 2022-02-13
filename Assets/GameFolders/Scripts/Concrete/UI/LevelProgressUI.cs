using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelProgressUI : MonoBehaviour
{

    [Header("UI references :")]
    [SerializeField] Image uiFillImage;
    [SerializeField] TextMeshProUGUI uiStartText;
    [SerializeField] TextMeshProUGUI uiEndText;
    [SerializeField] float coefficientDecreaseFillAmount;
    [SerializeField] float currentFillAmount;
    [SerializeField] float maxFillAmount;


    private void Start()
    {
        uiFillImage.fillAmount = currentFillAmount;
    }

    private void Update()
    {
        SetLevelTexts();
    }

    public void SetLevelTexts()
    {
        uiStartText.text = GameManager.Instance.Level.ToString();
        uiEndText.text = (GameManager.Instance.Level + 1).ToString();
    }

    public IEnumerator LevelBarDecrease()
    {
        while (currentFillAmount < maxFillAmount)
        {
            uiFillImage.fillAmount += Time.deltaTime * coefficientDecreaseFillAmount;
            yield return null;
        }
    }
}
