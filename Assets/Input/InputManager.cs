using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public static Vector2 Movement;
    public static InputAction interactAction;
    public static InputAction finAction;

    private PlayerInput playerInput;
    private InputAction moveAction;

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        moveAction = playerInput.actions["Move"];
        interactAction = playerInput.actions["Interact"];
        finAction = playerInput.actions["Fin"];
    }

    private void Update()
    {
        Movement = moveAction.ReadValue<Vector2>();

        if (finAction.WasPressedThisFrame())
        {
            Actions.ToggleFinApp();
        }
    }
}
