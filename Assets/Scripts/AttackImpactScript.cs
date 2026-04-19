using UnityEngine;
using System.Collections;
public class AttackImpactScript : MonoBehaviour
{
    private HybridCursor hybridCursorScript;
    private AttackTelegraphScript atckScript;
    
    [SerializeField] private int belongsToPlayer; 

    private void OnTriggerEnter2D(Collider2D other) 
    { 
        //if this attack belongs to player 1 it should only hurt player 2 and vice versa
        if (belongsToPlayer == 1 && other.CompareTag("Player2"))
        {
            hybridCursorScript = other.gameObject.GetComponentInChildren<HybridCursor>();
            atckScript = other.gameObject.GetComponentInChildren<AttackTelegraphScript>();
            if (hybridCursorScript != null && atckScript.P1Success())
            {
                hybridCursorScript.TriggerHurt();
            }
        }
        else if (belongsToPlayer == 2 && other.CompareTag("Player1"))
        { 
            hybridCursorScript = other.gameObject.GetComponentInChildren<HybridCursor>();
            atckScript = other.gameObject.GetComponentInChildren<AttackTelegraphScript>();
            if (hybridCursorScript != null && atckScript.P2Success())
            {
                hybridCursorScript.TriggerHurt();
            }
        }
    }
}
