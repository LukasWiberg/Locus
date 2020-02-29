using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleSystemBackground : MonoBehaviour {
    public ParticleSystem[] systems;
    public Vector2 size;

    private void Start() {
        for(int i = 0; i < systems.Length; i++) {
            ParticleSystem sys = systems[i];
            ParticleSystem.ShapeModule shape = sys.shape;
            shape.scale = size;
            ParticleSystem.EmissionModule emitter = sys.emission;
            emitter.rateOverTime = new ParticleSystem.MinMaxCurve(emitter.rateOverTime.constant * (size.y / 10));
            ParticleSystem.MainModule main = sys.main;
            main.maxParticles = main.maxParticles * Mathf.RoundToInt((size.y / 10));
            sys.Play();
        }
    }
}