#pragma warning disable 649
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraEffects : MonoBehaviour
{
    private Vector3 originalCameraPosition_;
    private float magnitude_ = 0;
    private Vector3 direction_ = Vector3.up;
    [SerializeField]
    private Image flashImage_;

    private void LateUpdate() {
        originalCameraPosition_ = new Vector3(transform.root.position.x, transform.root.position.y, -10);
    }

    public void Shake(float magnitude, float duration, Vector3 direction){
        StartCoroutine(ShakeCO(magnitude, duration, direction));
    }

    public void ShakeRandom(float magnitude, float duration){
        StartCoroutine(ShakeRandomCO(magnitude, duration));
    }

    private IEnumerator ShakeCO(float magnitude, float duration, Vector3 direction) {
        while (duration > 0)
        {
            transform.position = originalCameraPosition_ + direction * Random.value * magnitude;
            duration -= Time.deltaTime;
            yield return null;
        }
        transform.position = originalCameraPosition_;
    }

    private IEnumerator ShakeRandomCO(float magnitude, float duration) {
        while (duration > 0)
        {
            transform.position = originalCameraPosition_ + new Vector3(Random.value, Random.value, Random.value) * magnitude;
            duration -= Time.deltaTime;
            yield return null;
        }
        transform.position = originalCameraPosition_;
    }

    public void StartShake(float magnitude, Vector3 direction) {
        magnitude_ = magnitude;
        direction_ = direction;
        InvokeRepeating("CameraShake", 0, .01f);
    }

    private void CameraShake()
    {
        transform.position = originalCameraPosition_ + direction_ * Random.value * magnitude_;
    }

    public void StopShake() {
        CancelInvoke("CameraShake");
        transform.position = originalCameraPosition_;
    }

    public void Flash(){
        StartCoroutine(FlashCO(0.1f));
    }
    public void Flash(float duration){
        StartCoroutine(FlashCO(duration));
    }

    private IEnumerator FlashCO(float duration){
        flashImage_.enabled = true;
        var startColor = flashImage_.color;
        var endColor = startColor;
        endColor.a = 0;
        float elapsedTime = 0;

        while (elapsedTime <= duration)
        {
            flashImage_.color = Color.Lerp(startColor, endColor, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        flashImage_.color = startColor;
        flashImage_.enabled = false;
    }
}
