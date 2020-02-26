using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColourParticle : BaseParticle {
    private Sprite sprite;
    private Color color;
    private SpriteRenderer spriteRenderer;

    public void Initiate(Sprite sprite, Color color, Vector2 scale, Vector2 position, Vector2 velocity, float totalLifetime) {
        base.Initiate(position, velocity, totalLifetime);
        this.sprite = sprite;
        this.color = color;
        transform.localScale = scale;
        spriteRenderer = gameObject.AddComponent<SpriteRenderer>();
        spriteRenderer.sprite = sprite;
        spriteRenderer.color = color;
    }

    private new void Update() {
        base.Update();
    }
}