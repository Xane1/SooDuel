using Unity.VisualScripting;
using UnityEngine;

public class StickCursorScript : MonoBehaviour
{
    private Transform m_transform;
    
    private void Start()
    {
        m_transform = this.transform;
    }
    public Transform leftUpper, leftFore, leftHand;
    public Transform rightUpper, rightFore, rightHand;

    float leftReach, rightReach, reachUp, reachForward;

    [ContextMenu("ComputeReach")]
    void ComputeReach()
    {
        float LU = Vector2.Distance(leftUpper.position, leftFore.position);
        float LF = Vector2.Distance(leftFore.position, leftHand.position);
        float RU = Vector2.Distance(rightUpper.position, rightFore.position);
        float RF = Vector2.Distance(rightFore.position, rightHand.position);

        leftReach  = LU + LF;
        rightReach = RU + RF;

        float hardCap = Mathf.Min(leftReach, rightReach);


        reachForward = 1.20f * hardCap;
        reachUp      = 1.30f * hardCap;

        Debug.Log($"Reach caps set. forward={reachForward:F2}, up={reachUp:F2}");
    }

    private void LAMouse()
    {
        Vector2 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        m_transform.rotation = rotation;
    }
    // Update is called once per frame
    void Update()
    {
        LAMouse();
    }
}
