using UnityEngine;
using UnityEngine.InputSystem;

public class GameTargetCursorScript : MonoBehaviour
{
   public Transform aimTarget;
    
    public float aimSmooth = 10f;

    void Update()
    {
        Vector3 mouseScreenPos = Input.mousePosition;
        
        mouseScreenPos.z = Mathf.Abs(Camera.main.transform.position.z);

        Vector2 mouseWorldPos =
            Camera.main.ScreenToWorldPoint(mouseScreenPos);

        transform.position = Vector2.Lerp(
            transform.position,
            mouseWorldPos,
            Time.deltaTime * aimSmooth
        );
    }
}
