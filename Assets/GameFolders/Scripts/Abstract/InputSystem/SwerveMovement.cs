using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwerveMovement
{
    public void SwerveAmountX(Transform playerTransform, float swerveSpeedX, float maxSwerveAmount)
    {
        float swerveAmountX = swerveSpeedX * Time.deltaTime * GameManager.Instance.SwerveInputSystem.MoveFactorX;
        swerveAmountX = Mathf.Clamp(swerveAmountX, -maxSwerveAmount, maxSwerveAmount);
        playerTransform.Translate(swerveAmountX, 0, 0);
    }
}
