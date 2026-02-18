using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.InputSystem.Users;

public class HybridCursor : MonoBehaviour
{
    [SerializeField] private PlayerInput playerInput;
    [SerializeField] private Transform worldCursor;
    [SerializeField] private Camera mainCamera;
    
    [SerializeField] private float speedMultiplier = 1f;
    [SerializeField] private Vector3 offSet = new Vector3(0f, 0f, 0f);

    [SerializeField] private float smoothTime = 0.05f;

    private bool onBeatTarget = false;
    private Rigidbody2D stickBody;
    
    private Vector2 currentVelocity;
    private Vector2 targetPosition;
    
    private Mouse virtualMouse;
    private Vector2 screenPosition;
    private bool usingGamepad;
    private bool previousMouseState;
    
    private void OnEnable()
    {
        if (virtualMouse == null || !virtualMouse.added)
            virtualMouse = (Mouse)InputSystem.AddDevice("VirtualMouse");

        InputUser.PerformPairingWithDevice(virtualMouse, playerInput.user);

        screenPosition = new Vector2(Screen.width / 2f, Screen.height / 2f);
        InputState.Change(virtualMouse.position, screenPosition);

        InputSystem.onAfterUpdate += UpdateCursor;
    }

    private void OnDisable()
    {
        InputSystem.onAfterUpdate -= UpdateCursor;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
       //checks targets to see whetehr to slow down or not
       
       //slows down when over beatTarget
        if (other.CompareTag("BeatTarget"))
        {
            onBeatTarget = true;
            speedMultiplier = 0.3f;
            Debug.Log("Entered BeatTarget!");
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("BeatTarget"))
        {
            onBeatTarget = false;
            speedMultiplier = 1f; // restore normal speed
            Debug.Log("Exited BeatTarget!");
        }
    }
    
    private void UpdateCursor()
    {
        if (virtualMouse == null)
            return;

        DetectInputSource();

        if (usingGamepad)
            UpdateGamepadCursor();
        else
            UpdateRealMouseCursor();
    }

    private void DetectInputSource()
    {
        if (Gamepad.current != null &&
            Gamepad.current.rightStick.ReadValue().sqrMagnitude > 0.01f)
        {
            usingGamepad = true;
            Cursor.visible = false;
        }

        if (Mouse.current != null &&
            Mouse.current.delta.ReadValue().sqrMagnitude > 0.01f)
        {
            usingGamepad = false;
            Cursor.visible = true;
        }
    }

    private void UpdateGamepadCursor()
    {
        if (Gamepad.current == null)
            return;

        Vector2 stickValue = Gamepad.current.rightStick.ReadValue();
        stickValue = ApplyRadialDeadzone(stickValue, 0.2f);
        //Joystick based movement
        targetPosition = new Vector2(
            (stickValue.x + 1f) * 0.5f * Screen.width,
            (stickValue.y + 1f) * 0.5f * Screen.height
        );
        
      //  float adjustedSmoothTime = smoothTime/speedMultiplier;
        
        //Smoothing
        Vector2 nextPosition = Vector2.SmoothDamp(
            screenPosition,
            targetPosition,
            ref currentVelocity,
            smoothTime
        );

        // Apply slowdown here
        screenPosition = Vector2.Lerp(
            screenPosition,
            nextPosition,
            speedMultiplier
        );
        InputState.Change(virtualMouse.position, screenPosition);
        InputState.Change(virtualMouse.delta, currentVelocity * Time.deltaTime);

        MoveWorldCursor(screenPosition);
    }

    private void UpdateRealMouseCursor()
    {
        if (Mouse.current == null)
            return;

        screenPosition = Mouse.current.position.ReadValue();
        MoveWorldCursor(screenPosition);
    }

    private void MoveWorldCursor(Vector2 screenPos)
    {
        if (worldCursor == null || mainCamera == null)
            return;

        Vector3 worldPos = mainCamera.ScreenToWorldPoint(screenPos);
        worldPos.z = 0f;
        worldCursor.position = worldPos + offSet;
    }
    private Vector2 ApplyRadialDeadzone(Vector2 input, float deadzone)
    {
        float magnitude = input.magnitude;

        if (magnitude < deadzone)
            return Vector2.zero;

        float scaledMagnitude = (magnitude - deadzone) / (1f - deadzone);

        return input.normalized * scaledMagnitude;
    }
}
