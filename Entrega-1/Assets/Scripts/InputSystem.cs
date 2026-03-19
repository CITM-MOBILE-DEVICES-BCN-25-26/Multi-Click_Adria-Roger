using UnityEngine;
using System;

public class InputSystem : MonoBehaviour
{
    public event Action OnButtonPressed;
    public event Action OnButtonReleased;

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            OnButtonPressed?.Invoke();
        }
        if(Input.GetKeyUp(KeyCode.Space))
        {
            OnButtonReleased?.Invoke();
        }
    }
}
