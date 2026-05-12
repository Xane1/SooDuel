using UnityEngine;

public class SkinSelect : MonoBehaviour
{
    public Sprite AncientSkin;
    public Sprite MedievalSkin;

    private SpriteRenderer spriteRenderer;

   private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    
    void Start()
    {
        if (MapManager.Instance != null)
        {
            if (MapManager.Instance.CurrentStage == MapManager.Stage.Ancient)
            {
                spriteRenderer.sprite = AncientSkin;
            }

            if (MapManager.Instance.CurrentStage == MapManager.Stage.Medieval)
            {
                spriteRenderer.sprite = MedievalSkin;
            }
        }
    }
}
