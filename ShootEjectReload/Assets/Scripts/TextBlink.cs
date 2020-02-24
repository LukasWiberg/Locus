using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextBlink : MonoBehaviour
{
    private TextMeshProUGUI text_;    
    private Color originalColor_;
    private bool canBlink_ = true;

    private void Start() {
        text_ = GetComponentInChildren<TextMeshProUGUI>();
        originalColor_ = text_.color;
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
        canBlink_ = true;
        while (elapsedTime <= duration)
        {
            if (canBlink_){
                text_.color = Color.Lerp(color, originalColor_, elapsedTime / duration);
                elapsedTime += Time.deltaTime;
            }
            else{
                elapsedTime = duration + 1;
            }

            yield return null;
        }
        canBlink_ = true;
        text_.color = originalColor_;
    }
}
