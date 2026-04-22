using UnityEngine;
using UnityEngine.InputSystem;

public class BeatTargetScript : MonoBehaviour
{
    public bool isGreen = false;
    private CircleCollider2D circleCollider;
    private Rigidbody2D stickBody;
    
    public delegate void BeatFailAction();
    public static event BeatFailAction OnBeatFail;
    
    public delegate void BeatSuccessAction();
    public static event BeatSuccessAction OnBeatSuccess;
    
    void Start()
    {
        circleCollider = GetComponent<CircleCollider2D>();

        if (PlayerReference.LocalPlayer != null)
        {
            stickBody = PlayerReference.LocalPlayer.GetComponent<Rigidbody2D>();
        }
    }

    public void BeatReady()
    {
        isGreen = true;
        circleCollider.radius = 0.3f;
    }
    
    public void BeatFail()
    {
        Transform sibling = transform.parent.Find("FailBeat");

        Animator childAnimator = sibling.GetComponent<Animator>();
        OnBeatFail?.Invoke();
        sibling.gameObject.SetActive(true);
        childAnimator.SetTrigger("UniFail");
        Destroy(gameObject);
    }
    
    public void P1BeatFail()
    {
        Transform sibling = transform.parent.Find("FailBeat");

        Animator childAnimator = sibling.GetComponent<Animator>();
        OnBeatFail?.Invoke();
        sibling.gameObject.SetActive(true);
        childAnimator.SetTrigger("P1Fail");
        Destroy(gameObject);
    }

    public void P2BeatFail()
    {
        Transform sibling = transform.parent.Find("FailBeat");

        Animator childAnimator = sibling.GetComponent<Animator>();
        OnBeatFail?.Invoke();
        sibling.gameObject.SetActive(true);
        childAnimator.SetTrigger("P2Fail");
        Destroy(gameObject);
    }
    
    public void P1BeatHit()
    {
        Transform sibling = transform.parent.Find("WinBeat");
        
        Animator childAnimator = sibling.GetComponent<Animator>();
        OnBeatSuccess?.Invoke();
        sibling.gameObject.SetActive(true);
        childAnimator.SetTrigger("P1Win");
        ScoreManager.instance.P1AddPoints(100);
        Destroy(gameObject);
    }
    
    public void P2BeatHit()
    {
        Transform sibling = transform.parent.Find("WinBeat");
        
        Animator childAnimator = sibling.GetComponent<Animator>();
        OnBeatSuccess?.Invoke();
        sibling.gameObject.SetActive(true);
        childAnimator.SetTrigger("P2Win");
        ScoreManager.instance.P2AddPoints(100);
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
               P1BeatHit();
               
            }
            else if (circleCollider.OverlapPoint(mousePos))
            {
                P1BeatFail();
                ScoreManager.instance.P1AddPoints(-100);
            }
        }
        //Controller Input 
        /*
        if (Gamepad.current != null && Gamepad.current.leftShoulder.wasPressedThisFrame)
        {
            if (stickBody != null && circleCollider != null && stickBody.IsTouching(circleCollider))
            {
                GameObject.FindGameObjectsWithTag("Player1");
                if (isGreen)
                {
                    P1BeatHit();
                }
                else
                {
                    P1BeatFail();
                    ScoreManager.instance.P1AddPoints(-100);
                }
            }
            if (stickBody != null && circleCollider != null && stickBody.IsTouching(circleCollider))
            {
                GameObject.FindGameObjectsWithTag("Player2");
                if (isGreen)
                {
                    P2BeatHit();
                }
                else
                {
                    P2BeatFail();
                    ScoreManager.instance.P2AddPoints(-100);
                }
            }
        } */
    } 
}