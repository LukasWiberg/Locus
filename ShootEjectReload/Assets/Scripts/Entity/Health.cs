#pragma warning disable 649

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health {
    public float maxHealth { get; private set; }
    public float currentHealth { get; private set; }
    private bool hasBlinker { get; set; }
    private Action Blink;
    private Action Die;

    public Health(float maxHealth, Action die, Action blink, bool hasBlinker) {
        this.maxHealth = maxHealth;
        currentHealth = maxHealth;
        Die = die;
        Blink = blink;
        this.hasBlinker = hasBlinker;
    }

    public void heal(float health) {
        currentHealth += health;
        if(currentHealth > maxHealth) {
            currentHealth = maxHealth;
        }
    }

    public void TakeDamage(float damage) {
        currentHealth -= damage;
        if(currentHealth <= 0) {
            Die();
        } else if(hasBlinker) {
            Blink();
        }
    }
}