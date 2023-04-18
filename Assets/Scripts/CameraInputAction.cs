using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraInputAction : MonoBehaviour
{
    public void OnMove(InputAction.CallbackContext context)
    {
        Debug.Log($"OnMove {context.ReadValue<Vector2>()}");
    }
}
