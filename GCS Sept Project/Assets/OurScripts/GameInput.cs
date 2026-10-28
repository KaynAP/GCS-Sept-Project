using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameInput : MonoBehaviour
{
    public static GameInput Instance {get; private set;}

    private PlayerInputActions playerInput;

    public event EventHandler OnTestKeyPressed;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    private void Start()
    {
        playerInput = new PlayerInputActions();
        playerInput.Player.Enable();

        playerInput.Player.TestKey.performed += TestKeyPressed;
    }

    private void TestKeyPressed(InputAction.CallbackContext context)
    {
        OnTestKeyPressed?.Invoke(this, EventArgs.Empty);
    }
}
