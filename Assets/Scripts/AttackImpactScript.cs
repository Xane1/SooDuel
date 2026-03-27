using UnityEngine;
using System.Collections;
public class AttackImpactScript : MonoBehaviour
{
    private HybridCursor hybridCursorScript;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] private int belongsToPlayer; 

    private void OnTriggerEnter2D(Collider2D other) 
    { 
        // If this attack belongs to player 1, it should only hurt player 2 and vice versa
        if (belongsToPlayer == 1 && other.CompareTag("Player2"))
        {
            HybridCursor hybridCursorScript = other.gameObject.GetComponentInChildren<HybridCursor>();
            if (hybridCursorScript != null)
            {
                hybridCursorScript.TriggerHurt();
            }
        }
        else if (belongsToPlayer == 2 && other.CompareTag("Player1"))
        {
            HybridCursor hybridCursorScript = other.gameObject.GetComponentInChildren<HybridCursor>();
            if (hybridCursorScript != null)
            {
                hybridCursorScript.TriggerHurt();
            }
        }
    }
}
