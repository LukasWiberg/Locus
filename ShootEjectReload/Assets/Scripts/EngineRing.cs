using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EngineRing : MonoBehaviour
{
    public float force = 5f;
    public float lifetime = 0.5f;
    private float growthTime_ = 0.3f;
    private float elapsedGrowthTime_ = 0f;
    private float startSize_ = 0.3f;
    private Rigidbody2D rb_;

    private void Awake() {
        rb_ = GetComponent<Rigidbody2D>();
    }

    private void OnEnable() {
        rb_.AddForce(-transform.right * force, ForceMode2D.Impulse);
        transform.localScale = Vector3.one * startSize_;
    }

    private void Update() {
        Grow();

        if (lifetime >= 0){
            lifetime -= Time.deltaTime;
        }
        else{
            Destroy(gameObject);
        }
    }

    private void Grow() {
        if (elapsedGrowthTime_ <= growthTime_){
            transform.localScale = Vector3.one * Mathf.Lerp(startSize_, 1f, elapsedGrowthTime_ / growthTime_);
            elapsedGrowthTime_ += Time.deltaTime;
        }
    }
}
