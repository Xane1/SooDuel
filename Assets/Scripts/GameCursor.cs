using UnityEngine;
using UnityEngine.InputSystem;

public class GameCursor : MonoBehaviour
{
    [SerializeField] private Texture2D cursorTexture;
    
    [SerializeField] private Vector2 hotSpot = Vector2.zero;
    
    void Start()
    {
        Cursor.SetCursor(cursorTexture, hotSpot, CursorMode.Auto);
        new Vector2(cursorTexture.width * 0.5f, cursorTexture.height * 0.5f);
        Cursor.visible = true;
    }


    void Update()
    {
        if (Gamepad.current.leftShoulder.wasReleasedThisFrame)
        {
            Cursor.visible = false;
        }
        else if (Mouse.current != null)
        {
            Cursor.visible = true;
        }
    } 
}
