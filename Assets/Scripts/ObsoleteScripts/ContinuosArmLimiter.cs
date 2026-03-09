using UnityEngine;

public class ContinuousArmLimiter : MonoBehaviour
{
    [Header("References")]
    public Transform shoulder;          // Shoulder position (R Bicep or its parent)
    public Transform ikTarget;          // cursorPoint that CCD follows
    
    [Header("Forbidden Zone")]
    [Range(0, 360)] public float zoneStart = 90f;    // Start of forbidden zone (degrees)
    [Range(0, 360)] public float zoneEnd = 270f;     // End of forbidden zone
    
    [Header("Settings")]
    public float maxReach = 3f;          // Max arm length
    public float boundaryWidth = 5f;     // Soft boundary width in degrees
    
    private Vector3 lastValidPosition;
    private bool wasInForbiddenZone = false;
    private float lastValidAngle;
    
    void Start()
    {
        if (ikTarget)
        {
            lastValidPosition = ikTarget.position;
            lastValidAngle = GetTargetAngle();
        }
    }
    
    void LateUpdate()
    {
        if (!shoulder || !ikTarget) return;
        
        Vector3 shoulderPos = shoulder.position;
        Vector3 targetPos = ikTarget.position;
        Vector3 toTarget = targetPos - shoulderPos;
        
        // Limit reach distance first
        float distance = toTarget.magnitude;
        if (distance > maxReach)
        {
            targetPos = shoulderPos + (toTarget.normalized * maxReach);
            toTarget = targetPos - shoulderPos;
            distance = maxReach;
        }
        
        // Calculate current angle (0-360)
        float currentAngle = Mathf.Atan2(toTarget.y, toTarget.x) * Mathf.Rad2Deg;
        if (currentAngle < 0) currentAngle += 360f;
        
        // Check if in forbidden zone
        bool isInForbiddenZone = IsAngleInForbiddenZone(currentAngle);
        
        // Calculate angle change from last valid angle
        float angleChange = Mathf.DeltaAngle(lastValidAngle, currentAngle);
        
        if (isInForbiddenZone)
        {
            // We're in forbidden zone!
            
            if (!wasInForbiddenZone)
            {
                // Just entered forbidden zone - clamp to boundary
                float boundaryAngle = GetNearestBoundaryAngle(currentAngle);
                Vector3 boundaryDir = AngleToDirection(boundaryAngle);
                lastValidPosition = shoulderPos + boundaryDir * Mathf.Min(distance, maxReach);
                lastValidAngle = boundaryAngle;
                
                ikTarget.position = lastValidPosition;
                wasInForbiddenZone = true;
                return;
            }
            else
            {
                // Already in forbidden zone
                // Check if trying to move DEEPER into forbidden zone
                float nearestBoundary = GetNearestBoundaryAngle(currentAngle);
                float boundaryDist = Mathf.Abs(Mathf.DeltaAngle(currentAngle, nearestBoundary));
                
                // If moving toward center of forbidden zone, resist
                if (boundaryDist < 45f) // Moving toward center
                {
                    // Push back toward boundary
                    Vector3 boundaryDir = AngleToDirection(nearestBoundary);
                    Vector3 boundaryPos = shoulderPos + boundaryDir * distance;
                    
                    ikTarget.position = Vector3.Lerp(ikTarget.position, boundaryPos, Time.deltaTime * 10f);
                    lastValidPosition = ikTarget.position;
                    lastValidAngle = nearestBoundary;
                }
                else
                {
                    // Moving parallel to boundary or out - allow some movement
                    // but keep at boundary distance
                    Vector3 boundaryDir = AngleToDirection(nearestBoundary);
                    ikTarget.position = shoulderPos + boundaryDir * distance;
                    lastValidPosition = ikTarget.position;
                    lastValidAngle = nearestBoundary;
                }
            }
        }
        else
        {
            // In allowed zone
            if (wasInForbiddenZone)
            {
                // Check if we're moving AWAY from forbidden zone
                float nearestBoundary = GetNearestBoundaryAngle(currentAngle);
                float angleToBoundary = Mathf.DeltaAngle(currentAngle, nearestBoundary);
                
                // Only allow exit if moving away from boundary
                if (Mathf.Sign(angleChange) != Mathf.Sign(angleToBoundary))
                {
                    // Moving away from boundary - allow movement
                    lastValidPosition = targetPos;
                    lastValidAngle = currentAngle;
                    ikTarget.position = targetPos;
                }
                else
                {
                    // Trying to re-enter forbidden zone - stay at boundary
                    Vector3 boundaryDir = AngleToDirection(nearestBoundary);
                    ikTarget.position = shoulderPos + boundaryDir * distance;
                    lastValidPosition = ikTarget.position;
                    lastValidAngle = nearestBoundary;
                }
            }
            else
            {
                // Normal movement in allowed zone
                lastValidPosition = targetPos;
                lastValidAngle = currentAngle;
                ikTarget.position = targetPos;
            }
            
            wasInForbiddenZone = false;
        }
    }
    
    bool IsAngleInForbiddenZone(float angle)
    {
        // Handle wrap-around (e.g., zone from 270 to 90)
        if (zoneStart > zoneEnd)
        {
            return angle >= zoneStart || angle <= zoneEnd;
        }
        else
        {
            return angle >= zoneStart && angle <= zoneEnd;
        }
    }
    
    float GetNearestBoundaryAngle(float currentAngle)
    {
        float distToStart = Mathf.Abs(Mathf.DeltaAngle(currentAngle, zoneStart));
        float distToEnd = Mathf.Abs(Mathf.DeltaAngle(currentAngle, zoneEnd));
        
        return (distToStart < distToEnd) ? zoneStart : zoneEnd;
    }
    
    Vector3 AngleToDirection(float angle)
    {
        float rad = angle * Mathf.Deg2Rad;
        return new Vector3(Mathf.Cos(rad), Mathf.Sin(rad), 0);
    }
    
    float GetTargetAngle()
    {
        if (!shoulder || !ikTarget) return 0;
        
        Vector3 toTarget = ikTarget.position - shoulder.position;
        float angle = Mathf.Atan2(toTarget.y, toTarget.x) * Mathf.Rad2Deg;
        return angle < 0 ? angle + 360f : angle;
    }
    
  /*  void OnDrawGizmosSelected()
    {
        if (!shoulder || !showDebug) return;
        
        Vector3 center = shoulder.position;
        
        // Draw max reach circle
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(center, maxReach);
        
        // Draw forbidden zone
        Gizmos.color = new Color(1f, 0f, 0f, 0.3f);
        
        int segments = 30;
        float startRad = zoneStart * Mathf.Deg2Rad;
        float endRad = zoneEnd * Mathf.Deg2Rad;
        float angleStep = (endRad - startRad) / segments;
        
        if (zoneStart > zoneEnd)
        {
            startRad = zoneStart * Mathf.Deg2Rad;
            endRad = (zoneEnd + 360f) * Mathf.Deg2Rad;
            angleStep = (endRad - startRad) / segments;
        }
        
        for (int i = 0; i < segments; i++)
        {
            float angle1 = startRad + i * angleStep;
            float angle2 = startRad + (i + 1) * angleStep;
            
            Vector3 dir1 = new Vector3(Mathf.Cos(angle1), Mathf.Sin(angle1), 0);
            Vector3 dir2 = new Vector3(Mathf.Cos(angle2), Mathf.Sin(angle2), 0);
            
            Gizmos.DrawLine(center + dir1 * maxReach, center + dir2 * maxReach);
            Gizmos.DrawLine(center, center + dir1 * maxReach);
        }
        
        // Draw current target
        if (ikTarget)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawLine(center, ikTarget.position);
            Gizmos.DrawWireSphere(ikTarget.position, 0.1f);
        }
    } */
}