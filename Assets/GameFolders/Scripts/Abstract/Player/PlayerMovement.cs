using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement
{
    public void ForwardMove(Transform playerTransform, float speed)
    {
        playerTransform.Translate(Vector3.forward * speed * Time.deltaTime);
    }
}
