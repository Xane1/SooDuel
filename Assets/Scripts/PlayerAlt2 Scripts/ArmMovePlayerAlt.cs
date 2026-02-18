using UnityEngine;

using UnityEngine;
using UnityEngine.InputSystem;

public class StickIK2D : MonoBehaviour
{
    public Camera cam;
    public Transform cursorPoint;

    void Update()
    {
        if (Mouse.current == null)
            return;

        Vector2 mousePos = Mouse.current.position.ReadValue();

        Vector3 screenPos = new Vector3(
            mousePos.x,
            mousePos.y,
            Mathf.Abs(cam.transform.position.z)
        );

        Vector3 worldPos = cam.ScreenToWorldPoint(screenPos);

        cursorPoint.position = worldPos;
    }
}
