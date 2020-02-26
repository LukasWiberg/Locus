using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEntity : MonoBehaviour {
    public int maxHealth;
    public Health health { get; set; }
    public RandomBlinker blinker;

    [SerializeField]
    private GameObject explosionPrefab;

    private void Start() {
        bool hasBlinker = false;
        if(TryGetComponent(out RandomBlinker blinker)) {
            this.blinker = blinker;
            hasBlinker = true;
        }
        health = new Health(maxHealth, Die, Blink, hasBlinker);
    }

    private void Die() {
        Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    private void Blink() {
        blinker.Blink();
    }
}