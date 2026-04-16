using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class AttackTelegraphScript : MonoBehaviour
{
    public bool isReady = false;
    public bool fail = false;

    private CircleCollider2D circleCollider;
    private EdgeCollider2D stickBody;

    void Start()
    {
        circleCollider = GetComponent<CircleCollider2D>();

        if (PlayerReference.LocalPlayer != null)
        {
            stickBody = PlayerReference.LocalPlayer.GetComponent<EdgeCollider2D>();
        }
    }

    public void TelegraphReady()
    {
        isReady = true;
    }

    public void TelegraphStop()
    {
      //  fail = true;
        gameObject.SetActive(false);
    }

    public void P1Success()
    {
        Transform sibling = transform.parent.Find("P1Success");

        sibling.gameObject.SetActive(true);
        gameObject.SetActive(false);
        
    }

    public void P2Success()
    {
        Transform sibling = transform.parent.Find("P2Success");

        sibling.gameObject.SetActive(true);
        gameObject.SetActive(false);
    }

    void Update()
    {
        if (Gamepad.current != null && Gamepad.current.rightShoulder.wasPressedThisFrame)
        {
            if (stickBody != null && circleCollider != null && stickBody.IsTouching(circleCollider))
            {
                GameObject.FindGameObjectsWithTag("Player1");
                if (isReady)
                {
                    P1Success();
                }

            }

            if (stickBody != null && circleCollider != null && stickBody.IsTouching(circleCollider))
            {
                GameObject.FindGameObjectsWithTag("Player2");
                if (isReady)
                {
                    P2Success();
                }
            }
        }
    }
}