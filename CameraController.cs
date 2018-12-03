using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;
    public float tempTargetTime = 5f;

    public float smooth = 5f;
    public float offset = 1f;

    void LateUpdate()
    {
        if (GameController.instance.CurrentState == GameController.GameState.Playing)
        {
            Vector3 targetPosition = new Vector3(target.position.x, target.position.y + offset, transform.position.z);

            transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * smooth);
        }
    }
}
