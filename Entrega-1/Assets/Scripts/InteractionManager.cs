using UnityEngine;
using System;
using System.Threading.Tasks;
using System.Threading;

public class InteractionManager : MonoBehaviour
{
    [SerializeField] private float waitInterval = 0.5f;
    [SerializeField] private float longPressThreshold = 1f;

    private InputSystem inputSystem;
    
    private bool isPressed = false;
    private bool isWaitingForDouble = false;
    private float pressStartTime = 0f;

    private CancellationTokenSource cancellationTokenSource;
    
    void Start()
    {
        inputSystem = FindAnyObjectByType<InputSystem>();
        inputSystem.OnButtonPressed += HandleButtonPressed;
        inputSystem.OnButtonReleased += HandleButtonReleased;
    }

    private async void HandleButtonPressed()
    {
        isPressed = true;
        pressStartTime = Time.time;

        if(isWaitingForDouble)
        {
            isWaitingForDouble = false;
            cancellationTokenSource?.Cancel();
            Debug.Log("Double Press");
            return;
        }

        cancellationTokenSource?.Cancel();
        cancellationTokenSource?.Dispose();
        cancellationTokenSource = new CancellationTokenSource();

        await HandlePress(cancellationTokenSource.Token);
    }
    private void HandleButtonReleased()
    {
        isPressed = false;
    }

    private async Task HandlePress(CancellationToken cancellationToken)
    {
        try
        {
            while (isPressed)
            {
                if (Time.time - pressStartTime >= longPressThreshold)
                {
                    Debug.Log("Long Press");
                    return; 
                }
                await Task.Yield();
                cancellationToken.ThrowIfCancellationRequested();
            }
            isWaitingForDouble = true;
            await Task.Delay(TimeSpan.FromSeconds(waitInterval), cancellationToken);

            if (isWaitingForDouble)
            {
                isWaitingForDouble = false;
                Debug.Log("Short Press");
            }
        }
        catch (OperationCanceledException)
        {
            // Aquest Catch és necessari per evitar errors en consola quan es cancel·la
            // el Task al fer doble press o al detectar un long press.
        }
    }

    private void OnDestroy()
    {
        if (inputSystem != null)
        {
            inputSystem.OnButtonPressed -= HandleButtonPressed;
            inputSystem.OnButtonReleased -= HandleButtonReleased;
        }
        cancellationTokenSource?.Cancel();
        cancellationTokenSource?.Dispose();
    }
}

    