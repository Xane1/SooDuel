using UnityEngine;

public class DamageFlashScript : MonoBehaviour
{
   [SerializeField] private float flashDuration = 1f;

   private SpriteRenderer[] _spriteRenderers;
   private MaterialPropertyBlock _materialPropertyBlock;
   private float FlashFactor;

   private void Start()
   {
      _materialPropertyBlock = new MaterialPropertyBlock();
      _spriteRenderers = GetComponentsInChildren<SpriteRenderer>(true);
   }

   private void Update()
   {
      if (FlashFactor <= 0)
      {
         return;
      }
      FlashFactor = Mathf.Lerp(FlashFactor, 0f, Time.deltaTime * flashDuration);
      if (FlashFactor < 0.01f)
      {
         FlashFactor = 0f;
      }
      ApplyFlashFactor();
   }
   
   public void Flash()
   {
      FlashFactor = 1f;
      ApplyFlashFactor();
   }

   private void ApplyFlashFactor()
   {
      foreach (var renderer in _spriteRenderers)
      {
         renderer.GetPropertyBlock(_materialPropertyBlock);
         _materialPropertyBlock.SetFloat("_FlashFactor", FlashFactor);
         renderer.SetPropertyBlock(_materialPropertyBlock);
      }
   }
}
