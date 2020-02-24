using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteBlink : MonoBehaviour
{
    private SpriteRenderer rend_;    
    private Color originalColor_;
    private bool canBlink_ = true;

    private void Awake() {
        rend_ = GetComponentInChildren<SpriteRenderer>();
    }

    private void Start() {
        originalColor_ = rend_.color;
    }
    
    public void Blink(Color color){
        StartCoroutine(BlinkCO(color, 0.2f));
    }
    public void Blink(Color color, float duration) {
        StartCoroutine(BlinkCO(color, duration));
    }

    public void StopBlink(){
        canBlink_ = false;
    }

    private IEnumerator BlinkCO(Color color, float duration) {
        float elapsedTime = 0;
        while (elapsedTime <= duration)
        {
            if (canBlink_){
                rend_.color = Color.Lerp(color, originalColor_, elapsedTime / duration);
                elapsedTime += Time.deltaTime;
            }
            else{
                elapsedTime = duration + 1;
            }
            yield return null;
        }
        canBlink_ = true;
        rend_.color = originalColor_;
    }
}
