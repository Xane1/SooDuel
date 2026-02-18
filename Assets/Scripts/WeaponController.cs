using UnityEngine;

public class WeaponController : MonoBehaviour
{
    [SerializeField] Transform handBone;         // R hand bone
    [SerializeField] Transform weapon;           // Asa
    [SerializeField] Transform rightGrip;        // empty child on weapon
    [SerializeField] Transform leftGrip;         // empty child on weapon
    [SerializeField] Transform rightIKTarget;    // IK target
    [SerializeField] Transform leftIKTarget;     // IK target

    void LateUpdate()
    {
        // 1. Position weapon pivot at the hand bone
        transform.position = handBone.position;

        // 2. Rotate weapon toward mouse
        Vector3 mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 dir = mouse - transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle - 90f, Vector3.forward);

        // 3. Move IK targets to weapon grips
        rightIKTarget.position = rightGrip.position;
        rightIKTarget.rotation = rightGrip.rotation;

        leftIKTarget.position = leftGrip.position;
        leftIKTarget.rotation = leftGrip.rotation;
    }
}

