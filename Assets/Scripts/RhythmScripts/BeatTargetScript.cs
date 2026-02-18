using UnityEngine;
using UnityEngine.InputSystem;

public class BeatTargetScript : MonoBehaviour
{
    private bool isGreen = false;
    private CircleCollider2D circleCollider;
    private Rigidbody2D stickBody;
    
    public delegate void BeatFailAction();
    public static event BeatFailAction OnBeatFail;
    
    public delegate void BeatSuccessAction();
    public static event BeatSuccessAction OnBeatSuccess;

    public bool beatFailed = false;
    public bool beatSuccess = false;

    void Start()
    {
        circleCollider = GetComponent<CircleCollider2D>();
        
        GameObject stick = GameObject.FindGameObjectWithTag("Player");
        stickBody = stick.GetComponent<Rigidbody2D>();
        
        if (stickBody != null)
        {
            if (stick != null)
                stickBody = stick.GetComponent<Rigidbody2D>();
        }
    }

    public void BeatReady()
    {
        isGreen = true;
        circleCollider.radius = 0.07f;
    }

    public void BeatFail()
    {
        Transform sibling = transform.parent.Find("FailBeat");

        OnBeatFail?.Invoke();
        beatFailed = true;
        sibling.gameObject.SetActive(true);
        Destroy(gameObject);
    }

    public void BeatHit()
    {
        Transform sibling = transform.parent.Find("WinBeat");
        
        OnBeatSuccess?.Invoke();
        beatSuccess = true;
        sibling.gameObject.SetActive(true);
        ScoreManager.instance.AddPoints();
        Destroy(gameObject);
    }

    void Update()
    {
        //Mouse Input
        
        if (Pointer.current == null)
            return;
        
        if (Pointer.current.press.wasPressedThisFrame)
        {
            Vector2 mouseScreenPos = Mouse.current.position.ReadValue();
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(mouseScreenPos);

            if (circleCollider.OverlapPoint(mousePos) && isGreen)
            {
               BeatHit();
               
            }
            else if (circleCollider.OverlapPoint(mousePos))
            {
                BeatFail();
            }
        }
        //Controller Input 
        
       if (Gamepad.current != null && Gamepad.current.leftShoulder.wasPressedThisFrame)
       {

           if (stickBody.IsTouching(circleCollider))
           {
               if (isGreen) 
                   BeatHit();
               else
               {
                   BeatFail();
               }
           }
        }
    }
}