using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shaderScript : MonoBehaviour {

    // Start is called before the first frame update
    private void Start() {
        MaterialPropertyBlock props = new MaterialPropertyBlock();
        SpriteRenderer renderer;
        float r = Random.Range(0.0f, 1.0f);
        float g = Random.Range(0.0f, 1.0f);
        float b = Random.Range(0.0f, 1.0f);
        props.SetColor("_Color", new Color(r, g, b));

        renderer = gameObject.GetComponent<SpriteRenderer>();
        renderer.SetPropertyBlock(props);
    }

    // Update is called once per frame
    private void Update() {
    }
}