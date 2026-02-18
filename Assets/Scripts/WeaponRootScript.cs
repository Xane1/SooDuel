using UnityEngine;
using UnityEngine.InputSystem;

public class WeaponMouseFollow2D : MonoBehaviour
{
    [Header("Refs")]
    [SerializeField] private Transform WeaponPivot;  // pivot on the body (torso/neck)
    [SerializeField] private Camera cam;

    [Header("Limits")]
    public float maxReach = 1.4f;
    public float minReach = 0.25f;
    public float maxUpAngle = 180f;
    public float maxDownAngle = -180f;

    [Header("Smoothing")]
    public float moveSpeed = 20f;
    public float rotateSpeed = 720f;
    private float reachForward;
    private float reachUp;
    void Awake()
    {
        if (cam == null) cam = Camera.main;
        if (WeaponPivot == null && transform.parent != null) WeaponPivot = transform.parent; // works if WeaponRoot is parented to torso
        if (WeaponPivot == null)
            Debug.LogError("WeaponMouseFollow2D: Assign a Shoulder transform (pivot on the body).");
    }

    void Update()
    {
        if (cam == null || WeaponPivot == null) return;

        // mouse → world
        Vector2 ms = Mouse.current.position.ReadValue();
        float z = Mathf.Abs(cam.transform.position.z - WeaponPivot.position.z);
        Vector3 mw = cam.ScreenToWorldPoint(new Vector3(ms.x, ms.y, z));
        mw.z = WeaponPivot.position.z;

        // vector from shoulder to mouse
        Vector2 v = (Vector2)mw - (Vector2)WeaponPivot.position;

        // angle clamp
        float a = Mathf.Atan2(v.y, v.x) * Mathf.Rad2Deg;
        a = Mathf.Clamp(a, maxDownAngle, maxUpAngle);
        Vector2 dir = new(Mathf.Cos(a * Mathf.Deg2Rad), Mathf.Sin(a * Mathf.Deg2Rad));

        // distance clamp
        float upDot = Mathf.Clamp01(Vector2.Dot(Vector2.up, dir));
        float maxReachDynamic = Mathf.Lerp(reachForward, reachUp, upDot);

        float d = v.magnitude;
        float dist = Mathf.Clamp(d, minReach, maxReachDynamic);
        float softZone = 0.20f * maxReachDynamic;
        if (dist > maxReachDynamic)
        {
            float over = dist - maxReachDynamic;
            float t = Mathf.Clamp01(over / softZone);
            dist = maxReachDynamic + Mathf.Lerp(0f, 0.12f * maxReachDynamic, t * t);
        }
        Vector2 targetPos = (Vector2)WeaponPivot.position + dir * dist;

        // move + rotate
        transform.position = Vector2.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);
        Quaternion targetRot = Quaternion.AngleAxis(a, Vector3.forward);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRot, rotateSpeed * Time.deltaTime);
    }
}
