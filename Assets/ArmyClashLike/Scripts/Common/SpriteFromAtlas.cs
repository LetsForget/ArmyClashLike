using UnityEngine;
using UnityEngine.U2D;

namespace Common
{
    public class SpriteFromAtlas : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer spriteRenderer;

        public void SetSpriteAtlas(SpriteAtlas atlas)
        {
            var spriteName = spriteRenderer.sprite.name;
            spriteRenderer.sprite = atlas.GetSprite(spriteName);
        }

        private void OnValidate()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }
    }
}