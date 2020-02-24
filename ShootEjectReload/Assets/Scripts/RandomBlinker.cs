using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomBlinker : MonoBehaviour
{
    private SpriteRenderer rend_;
    private Color originalColor_;

    private void Start() {
        rend_ = GetComponentInChildren<SpriteRenderer>();
        originalColor_ = rend_.color;
    }

    public void Blink(){
        StartCoroutine(BlinkCO(0.2f));
    }
    public void Blink(float duration){
        StartCoroutine(BlinkCO(duration));
    }

    private IEnumerator BlinkCO(float duration) {
        while (duration > 0)
        {
            rend_.color = new Color(Random.value, Random.value, Random.value);
            duration -= Time.deltaTime;
            yield return null;
        }
        rend_.color = originalColor_;
    }
}
