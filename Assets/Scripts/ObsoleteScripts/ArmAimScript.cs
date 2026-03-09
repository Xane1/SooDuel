using UnityEngine;
using UnityEngine.U2D.IK;

public class ArmAimScript : MonoBehaviour
{
    public Transform RightHandTarget;  
    public Transform LeftHandTarget;
    public Camera cam;       
    
    public LimbSolver2D leftArmSolver;
    public Transform leftBicep;
    public float flipThreshold = 60f;

    public float flipYOffset = 0.0f;
    
    void Update()
    {
        if (cam == null)
            cam = Camera.main;

        Vector3 mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = RightHandTarget.position.z;

        // Move the right hand target to the mouse position
        RightHandTarget.position = mousePos;

        // Rotate the right hand target (and weapon) toward the mouse
        Vector2 direction = (mousePos - RightHandTarget.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        RightHandTarget.rotation = Quaternion.Euler(0f, 0f, angle);
        
        LeftHandTarget.position = RightHandTarget.position;
        LeftHandTarget.rotation = RightHandTarget.rotation;
        
      
        Vector3 mouseWorld = cam.ScreenToWorldPoint(Input.mousePosition);
        mouseWorld.z = leftBicep.position.z;

        bool aimingUp = mouseWorld.y > (leftBicep.position.y + flipYOffset);

        leftArmSolver.flip = aimingUp;
    }
}
