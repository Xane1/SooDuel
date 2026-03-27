using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.InputSystem.Users;
using UnityEngine.SceneManagement;
using System.Collections;
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
    
    [SerializeField] private GameObject attackObject;
    
    [SerializeField] private CoolDownScript cooldown;
    
    [SerializeField] private float hurtDuration = 0.5f;
    
    private bool onBeatTarget;
    private Rigidbody2D stickBody;
    
    private Vector2 smoothedStick;
    private Vector2 currentVelocity;
    private Vector2 targetPosition;
    
    private Mouse virtualMouse;
    private Vector2 screenPosition;
    private bool usingGamepad;
    private bool previousMouseState;
    private bool isHurt = false;
    
    public int playerNumber;
    
    private void OnEnable()
    {
        if (virtualMouse == null || !virtualMouse.added) virtualMouse = (Mouse)InputSystem.AddDevice("VirtualMouse");

        InputUser.PerformPairingWithDevice(virtualMouse, playerInput.user);

        //below line moves virtual cursor to center of the screen
        screenPosition = new Vector2(Screen.width / 2f, Screen.height / 2f);
        InputState.Change(virtualMouse.position, screenPosition);

        //Runs UpdateCursor at end of each frame
        InputSystem.onAfterUpdate += UpdateCursor;
    }

    private void OnDisable()
    {
        //Disables UpdateCursor at end of each frame
        InputSystem.onAfterUpdate -= UpdateCursor;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
       //Slows down when over beatTarget
        if (other.CompareTag("CursorSlow"))
        {
            onBeatTarget = true;
            speedMultiplier = 0.4f;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        //Restores normal speed when no longer over beatTarget
        if (other.CompareTag("CursorSlow"))
        {
            onBeatTarget = false;
            speedMultiplier = 1f; 
        }
    }
    
    //Detecting the beats to be able to hit them from this script
    private void TryHitBeat()
    {
        //Gets the Collider2D on this object
        Collider2D myCollider = GetComponent<Collider2D>();

        //Creates a list to store results
        ContactFilter2D filter = new ContactFilter2D();
        //Includes trigger colliders
        filter.useTriggers = true; 
        //Stores overlapping Colliders
        Collider2D[] results = new Collider2D[10]; 
        //Fills results with all colliders currently overlapping this collider
        int hitCount = myCollider.Overlap(filter, results);

        for (int i = 0; i < hitCount; i++)
        {
            BeatTargetScript beat = results[i].GetComponent<BeatTargetScript>();
            if (beat != null)
            {
                if (playerNumber == 1)
                {
                    if (beat.isGreen)
                    {
                        beat.P1BeatHit();
                    }
                    else
                    {
                        beat.P1BeatFail();
                        ScoreManager.instance.P1AddPoints(-100);
                    }
                }
                
                if (playerNumber == 2)
                {
                    if (beat.isGreen)
                    {
                        beat.P2BeatHit();
                    }
                    else
                    {
                        beat.P2BeatFail();
                        ScoreManager.instance.P2AddPoints(-100);
                    }
                }
            }
        }
    }
    
    //Using Player Input Behaviour "Send Messages" so the TryHitBeat method functions
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
        {
            UpdateGamepadCursor();

        }
        else
        {
            UpdateRealMouseCursor();
        }
        if (Keyboard.current != null && Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            SceneManager.LoadScene("TitleScreen");
        }
    }

    private void DetectInputSource()
    
    {
        if (Gamepad.current != null && Gamepad.current.rightStick.ReadValue().sqrMagnitude > 0.01f)
        {
            usingGamepad = true;
            Cursor.visible = false;
        }
/*  if (Mouse.current != null && Mouse.current.delta.ReadValue().sqrMagnitude > 0.01f)
        {
            usingGamepad = false;
            Cursor.visible = true;
        } */
    }

    
    //Below method is called in UpdateCursor
    private void UpdateGamepadCursor()
    {
        if (isHurt) return;
        
        if (playerTransform == null)
            return;
        
        //Gets the individual gamepads of the players
        Gamepad gp = playerInput.user.pairedDevices[0] as Gamepad;
        if (gp == null)
            return;

        //Reads the stick input and applies dead zone
        Vector2 stickValue = gp.rightStick.ReadValue();
        //Deadzone so that tiny movements in the stick are ignored to prevent drift
        stickValue = ApplyRadialDeadzone(stickValue, 0.2f);

        //Smooths the stick input slightly
        smoothedStick = Vector2.Lerp(smoothedStick, stickValue, 10f * Time.deltaTime);

        //Calculates cursor offset in world space
        Vector3 offset = new Vector3(stickValue.x * cursorRadius, stickValue.y * cursorRadius, 0f);

        //Targets cursor position in world space
        Vector3 targetWorldPos = playerTransform.position + offset;
        
        //Smoothing of controller movement
        Vector2 nextPosition = Vector2.SmoothDamp(worldCursor.position, targetWorldPos, ref currentVelocity, smoothTime);
        
        //Smoothly move the cursor
        worldCursor.position =  Vector2.Lerp(worldCursor.position, nextPosition, speedMultiplier);

        //screenPosition = Vector2.Lerp(screenPosition, nextPosition, speedMultiplier);
        //Optionally update virtual mouse for systems that rely on it
    }
    
   private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
    
        Gizmos.DrawWireSphere(playerTransform.position, cursorRadius);
        Gizmos.DrawSphere(worldCursor.position, 0.5f);
    } 

   //Attack stuff below
   private void Update ()
   {
       if (isHurt) return;
       if (cooldown.IsCoolDown) return;
       Gamepad gp = playerInput.user.pairedDevices[0] as Gamepad;
    
       if (gp != null && gp.rightShoulder.wasPressedThisFrame)
       {
           StartCoroutine(ActivateAttack());
           cooldown.StartCoolDown();
       }
   }
   
   private IEnumerator ActivateAttack()
   {
       attackObject.SetActive(true);
       yield return new WaitForSeconds(2f);
       attackObject.SetActive(false);
   }
   public void TriggerHurt()
   {
       StartCoroutine(PlayerHurt());
   }
   private IEnumerator PlayerHurt()
   {
       this.enabled = false;
       yield return new WaitForSeconds(hurtDuration);
       this.enabled = true;
   }

   //Called in UpdateCursor so mouse cursor is usable
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
