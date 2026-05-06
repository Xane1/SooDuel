using UnityEngine;

public class GameCursor : MonoBehaviour
{
    [SerializeField] private Texture2D cursorTexture;
    
    [SerializeField] private Vector2 hotSpot = Vector2.zero;
    
    void Start()
    {
        Cursor.SetCursor(cursorTexture, hotSpot, CursorMode.Auto);
        new Vector2(cursorTexture.width * 0.5f, cursorTexture.height * 0.5f);
    }
}
