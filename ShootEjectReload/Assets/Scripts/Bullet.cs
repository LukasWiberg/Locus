#pragma warning disable 649

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {
    public float force = 30f;
    public float damage = 10f;
    public bool pierce = false;
    private Rigidbody2D rb_;
    private float lifetime_ = 2f;
    private AudioSource audio_;
    private GameObject spriteObj_;
    private Collider2D col_;

    private void Awake() {
        rb_ = GetComponent<Rigidbody2D>();
        audio_ = GetComponent<AudioSource>();
        spriteObj_ = transform.GetChild(0).gameObject;
        col_ = GetComponent<Collider2D>();
    }

    private void OnEnable() {
        rb_.AddForce(transform.up * force, ForceMode2D.Impulse);
    }

    private void Update() {
        if(lifetime_ > 0) {
            lifetime_ -= Time.deltaTime;
        } else {
            Destroy(gameObject);
        }
    }

    private void DestroyAfterAudioFinished(GameObject obj) {
        StartCoroutine(DestroyAfterAudioFinishedCO(obj));
    }

    private IEnumerator DestroyAfterAudioFinishedCO(GameObject obj) {
        rb_.velocity = Vector3.zero;
        spriteObj_.SetActive(false);
        col_.enabled = false;
        var duration = audio_.clip.length;

        while(duration > 0) {
            duration -= Time.deltaTime;
            yield return null;
        }
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Enemy" && other.TryGetComponent(out BaseEntity entity)) {
            entity.health.TakeDamage(damage);
            if(!pierce) {
                DestroyAfterAudioFinished(gameObject);
            }
        }

        if(other.tag == "CartridgeRing" && other.transform.root.TryGetComponent(out Cartridge cartridge)) {
            if(!cartridge.TryExplode()) {
                var dir = (other.transform.position - this.transform.position).normalized;
                other.attachedRigidbody.AddForce(dir * 15f, ForceMode2D.Impulse);
                cartridge.PlayEmptyHitSound();
            }
            DestroyAfterAudioFinished(gameObject);
        }
    }
}