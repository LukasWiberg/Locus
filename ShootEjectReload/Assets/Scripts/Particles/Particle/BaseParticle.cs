using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseParticle : MonoBehaviour {
    public float totalLifetime { get; set; }
    public float remainingLifetime { get; set; }
    public new Rigidbody2D rigidbody { get; set; }

    public void Initiate(Vector2 position, Vector2 velocity, float totalLifetime) {
        this.totalLifetime = totalLifetime;
        remainingLifetime = totalLifetime;
        transform.position = position;
        rigidbody = gameObject.AddComponent<Rigidbody2D>();
        rigidbody.velocity = velocity;
    }

    public void Update() {
        remainingLifetime -= Time.deltaTime;
        if(remainingLifetime <= 0) {
            Destroy(gameObject);
        }
    }
}