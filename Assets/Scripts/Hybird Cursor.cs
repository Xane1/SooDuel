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

    [SerializeField] private Transform playerTransform; 
    [SerializeField] private float cursorRadius = 3f;
    
    private bool onBeatTarget = false;
    private Rigidbody2D stickBody;
    
    private Vector2 smoothedStick;
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
       //checks targets to see whether to slow down or not
       
       //slows down when over beatTarget
        if (other.CompareTag("CursorSlow"))
        {
            onBeatTarget = true;
            speedMultiplier = 0.4f;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("CursorSlow"))
        {
            onBeatTarget = false;
            speedMultiplier = 1f; // restore normal speed
        }
    }
    
    //New way of detecting beat hits
    private void TryHitBeat()
    {
        // Get the Collider2D on this object
        Collider2D myCollider = GetComponent<Collider2D>();

        // Create a list to store results
        ContactFilter2D filter = new ContactFilter2D();
        filter.useTriggers = true; // Include trigger colliders
        Collider2D[] results = new Collider2D[10]; // Adjust size if needed

        // Fill results with all colliders currently overlapping this collider
        int hitCount = myCollider.Overlap(filter, results);

        for (int i = 0; i < hitCount; i++)
        {
            BeatTargetScript beat = results[i].GetComponent<BeatTargetScript>();
            if (beat != null)
            {
                if (beat.isGreen)
                    beat.BeatHit();
                else
                    beat.BeatFail();
            }
        }
    }
    void OnHit()
    {
        TryHitBeat();
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
        
        if (Gamepad.current == null || playerTransform == null)
            return;

        // Read stick input and apply deadzone
        Vector2 stickValue = Gamepad.current.rightStick.ReadValue();
        stickValue = ApplyRadialDeadzone(stickValue, 0.2f);

        // Smooth the stick input slightly
        smoothedStick = Vector2.Lerp(smoothedStick, stickValue, 10f * Time.deltaTime);

        // Calculate cursor offset in world space
        Vector3 offset = new Vector3(
            stickValue.x * cursorRadius,
            stickValue.y * cursorRadius,
            0f
        );

        // Target cursor position in world space
        Vector3 targetWorldPos = playerTransform.position + offset;

        Vector2 nextPosition = Vector2.SmoothDamp(worldCursor.position, targetWorldPos, ref currentVelocity, smoothTime);
        
        // Smoothly move the cursor
        worldCursor.position =  Vector2.Lerp(worldCursor.position, nextPosition, speedMultiplier);

        //screenPosition = Vector2.Lerp(screenPosition, nextPosition, speedMultiplier);
        // Optionally update virtual mouse for systems that rely on it
        if (virtualMouse != null)
        {
            Vector2 screenPos = mainCamera.WorldToScreenPoint(worldCursor.position);
            InputState.Change(virtualMouse.position, screenPos);
            InputState.Change(virtualMouse.delta, currentVelocity * Time.deltaTime);
        }
    }
    
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
    
        Gizmos.DrawWireSphere(playerTransform.position, cursorRadius);
        Gizmos.DrawSphere(worldCursor.position, 0.5f);
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
