using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

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

    public void TelegraphOver()
    {
        isReady = false;
    }

    public void TelegraphStop()
    {
        gameObject.SetActive(false);
    }

    public bool P1Success()
    {
        isReady = false;
        Transform sibling = transform.parent.Find("P1Success");

        sibling.gameObject.SetActive(true);
        gameObject.SetActive(false);

        RumbleManager.instance.RumblePulse(0.5f, 0.5f, 0.15f, p1Gamepad);
        RumbleManager.instance.RumblePulse(0.8f, 0.8f, 0.15f, p2Gamepad);
        
        return (true);
    }

    public bool P2Success()
    {
        isReady = false;
        
        Transform sibling = transform.parent.Find("P2Success");

        sibling.gameObject.SetActive(true);
        gameObject.SetActive(false);
        
        RumbleManager.instance.RumblePulse(0.5f, 0.5f, 0.15f, p2Gamepad);
        RumbleManager.instance.RumblePulse(0.8f, 0.8f, 0.15f, p1Gamepad);
        
        return (true);
    }

    void Update()
    {
        
    }

}