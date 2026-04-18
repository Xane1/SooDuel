using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class AttackTelegraphScript : MonoBehaviour
{
    public bool isReady = false;
    public bool fail = false;

    public bool player1Target;
    public bool player2Target;

    public CircleCollider2D circleCollider;
    private EdgeCollider2D p1StickBody;
    private EdgeCollider2D p2StickBody;

    private Gamepad p1Gamepad;
    private Gamepad p2Gamepad;


    void OnEnable()
    {
        circleCollider = GetComponent<CircleCollider2D>();
        isReady = false;

        p1StickBody = GameObject.FindGameObjectWithTag("Player1").GetComponentInChildren<EdgeCollider2D>();
        p2StickBody = GameObject.FindGameObjectWithTag("Player2").GetComponentInChildren<EdgeCollider2D>();

        if (Gamepad.all.Count > 0) p1Gamepad = Gamepad.all[0];
        if (Gamepad.all.Count > 1) p2Gamepad = Gamepad.all[1];
    }

    public void TelegraphReady()
    {
        isReady = true;
    }

    public void TelegraphStop()
    {
        //  fail = true;
        isReady = false;
        gameObject.SetActive(false);
    }

    public void P1Success()
    {
        isReady = false;
        Transform sibling = transform.parent.Find("P1Success");

        sibling.gameObject.SetActive(true);
        gameObject.SetActive(false);

    }

    public void P2Success()
    {
        isReady = false;
        Transform sibling = transform.parent.Find("P2Success");

        sibling.gameObject.SetActive(true);
        gameObject.SetActive(false);
    }

    void Update()
    {
        /* if (!isReady || circleCollider == null) return;

         if (p1Gamepad != null && p1Gamepad.rightShoulder.wasPressedThisFrame)
         {
             if (!player1Target && p2StickBody != null && p2StickBody.IsTouching(circleCollider))
             {
                 P2Success();
             }
         }

         if (p2Gamepad != null && p2Gamepad.rightShoulder.wasPressedThisFrame)
         {
             if (!player2Target && p1StickBody != null && p1StickBody.IsTouching(circleCollider))
             {
                 P1Success();
             }
         }
     } */
    }
}