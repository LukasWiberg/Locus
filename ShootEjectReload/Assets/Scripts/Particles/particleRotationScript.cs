using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class particleRotationScript : MonoBehaviour {
    private ParticleSystem.MainModule main;

    private void Start() {
        main = GetComponent<ParticleSystem>().main;
    }

    // Update is called once per frame
    private void Update() {
        float rot = transform.rotation.eulerAngles.z;
        rot += 105;
        if(rot < 0) {
            rot += 360;
        }
        main.startRotation = -rot / 57.3f;
    }
}