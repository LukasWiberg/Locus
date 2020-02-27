using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEntity : MonoBehaviour {
    public float maxHealth;
    public float currentHealth;
    public Health health { get; set; }
    public RandomBlinker blinker { get; private set; }

    [SerializeField]
    private GameObject explosionPrefab;

    private void Start() {
        blinker = gameObject.AddComponent<RandomBlinker>();
        bool hasBlinker = true;
        health = new Health(maxHealth, Die, blinker.Blink, hasBlinker);
    }

    private void Die() {
        Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    private void Update() {
        currentHealth = health.currentHealth;
    }
}