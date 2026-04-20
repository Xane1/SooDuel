using UnityEngine;
using System.Collections;

public class AttackTelegraphManager : MonoBehaviour
{
    public GameObject player1Telegraph; // drag in Inspector
    public GameObject player2Telegraph; // drag in Inspector

    private bool p1First;
    public IEnumerator Start()
    {
        // Wait for both players to be in the scene
       yield return new WaitUntil(() => GameObject.FindGameObjectWithTag("Player1") != null && GameObject.FindGameObjectWithTag("Player2") != null);
        
        p1First = Random.value > 0.5f;
    }

    public void TelegraphSequence()
    {
       // yield return new WaitForSeconds(15f);
        
            GameObject current = p1First ? player1Telegraph : player2Telegraph;

            current.SetActive(true);

            //yield return new WaitForSeconds(15f);

            p1First = !p1First;
    }
}