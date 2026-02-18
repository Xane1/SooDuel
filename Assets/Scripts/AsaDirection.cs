using UnityEngine;

public class AsaDirection : MonoBehaviour
{
    private Transform m_transform;
    [SerializeField] private Transform player_transform;
    
    private void Start()
    {
        m_transform = this.transform;
    }

    // Update is called once per frame
    private void LAMouse()
    {
        Vector2 direction = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - m_transform.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle - 90f, Vector3.forward);
        m_transform.rotation = rotation;
    }
    // Update is called once per frame
    void LateUpdate()
    {
        LAMouse();
    }
}
