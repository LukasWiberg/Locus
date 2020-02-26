using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ParticleInstancer {

    public static GameObject GenerateColourParticle(Sprite sprite, Color color, Vector2 scale, Vector2 position, Vector2 velocity, float totalLifetime) {
        GameObject go = new GameObject();
        go.AddComponent<ColourParticle>().Initiate(sprite, color, scale, position, velocity, totalLifetime);
        return go;
    }

    public static GameObject[] GenerateColourParticles(int amount, int degreeSpread, float velocitySpread, float lifetimeRange, Sprite sprite, Color color, Vector2 scale, Vector2 position, Vector2 velocity, float totalLifetime) {
        GameObject[] ret = new GameObject[amount];
        //Random rand = new Mathf.Random();
        Random rand = new Random();
        Vector2 normalizedVelocity = velocity.normalized;
        for(int i = 0; i < amount; i++) {
            GenerateColourParticle(sprite, color, scale, position, velocity * (1 + ((Random.value * velocitySpread) - velocitySpread / 2)), totalLifetime * (1 + ((Random.value * lifetimeRange) - lifetimeRange / 2)));
        }
        return ret;
    }
}