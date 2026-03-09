using UnityEngine;

public class BoneConstraint2D : MonoBehaviour
{
    public float minAngle = -30f;
    public float maxAngle = 150f;
    
    void LateUpdate()  // Runs AFTER CCD Solver
    {
        // Get current local rotation angle
        float currentAngle = transform.localEulerAngles.z;
        
        // Convert from 0-360 to -180 to 180
        if (currentAngle > 180f) 
            currentAngle -= 360f;
        
        // Clamp it
        float clampedAngle = Mathf.Clamp(currentAngle, minAngle, maxAngle);
        
        // Apply clamped rotation (Z axis only for 2D)
        transform.localEulerAngles = new Vector3(0, 0, clampedAngle);
    }
}