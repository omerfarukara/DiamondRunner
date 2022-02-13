using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] Vector3 offsetPos;
    [SerializeField] Vector3 offsetPosFinish;
    [SerializeField] float delay;

    void LateUpdate()
    {
        if (GameManager.Instance.IsGameOver) return;
        transform.position = Vector3.Lerp(transform.position, GameManager.Instance.PlayerController.transform.position + offsetPos, 0.2f);
    }


    public void FinishState()
    {
        StartCoroutine(FinishStateCoroutine());
    }

    IEnumerator FinishStateCoroutine()
    {
        float t = delay;
        while (t > 0)
        {
            t -= Time.deltaTime;
            transform.position = Vector3.Lerp(transform.position, GameManager.Instance.PlayerController.transform.position + offsetPosFinish, 0.3f / (t + 0.03f));
            yield return null;
        }
        transform.position = GameManager.Instance.PlayerController.transform.position + offsetPosFinish;
    }

}
