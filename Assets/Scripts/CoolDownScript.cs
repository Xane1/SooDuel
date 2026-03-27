using UnityEngine;
using System.Collections;

public class CoolDownScript : MonoBehaviour
{
    [SerializeField] GameObject attackTelegraph;
    private float coolDownTime = 6f;

    private float nextAttackTime;
    public bool IsCoolDown => Time.time < nextAttackTime;
    public void StartCoolDown()
    {
        attackTelegraph.SetActive(false);
        nextAttackTime = Time.time + coolDownTime;
        StartCoroutine(ReEnableTelegraph());
    }

    private IEnumerator ReEnableTelegraph()
    {
        yield return new WaitForSeconds(coolDownTime);
        attackTelegraph.SetActive(true);
    }
}
