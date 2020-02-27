using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour {
    public float damage = 20f;
    public float knockback = 20f;
    public float initialSize = 5f;

    //private SpriteBlink spriteBlink_;
    private CameraEffects cameraEffects_;

    private float lifetime_ = 1f;
    private float timeUntilShrink = 0.3f;
    private float elapsedTimeLifetime = 0f;
    private float elapsedTimeShrink = 0f;

    private void Awake() {
        //spriteBlink_ = GetComponent<SpriteBlink>();
        cameraEffects_ = Camera.main.GetComponent<CameraEffects>();
    }

    private void Start() {
        transform.localScale = Vector2.one * initialSize;
        //spriteBlink_.Blink(Color.white, 1f);
        cameraEffects_.Flash(0.2f);
        cameraEffects_.ShakeRandom(0.5f, 0.2f);
    }

    private void Update() {
        if(elapsedTimeLifetime <= lifetime_) {
            if(elapsedTimeLifetime >= timeUntilShrink) {
                transform.localScale = Vector2.one * Mathf.Lerp(initialSize, 0, elapsedTimeShrink / (lifetime_ - timeUntilShrink));
                elapsedTimeShrink += Time.deltaTime;
            }
            elapsedTimeLifetime += Time.deltaTime;
        } else {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Enemy" && other.TryGetComponent(out BaseEntity entity)) {
            entity.health.TakeDamage(damage);
            if(other.TryGetComponent(out Rigidbody2D rb)) {
                var dir = (other.transform.position - this.transform.position).normalized;
                rb.AddForce(dir * knockback, ForceMode2D.Impulse);
            }
        }

        if(other.tag == "Cartridge" && other.TryGetComponent(out Cartridge cartridge)) {
            cartridge.ForceExplode(0.1f);
        }
    }
}