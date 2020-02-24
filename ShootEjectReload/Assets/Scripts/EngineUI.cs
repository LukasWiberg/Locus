using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EngineUI : MonoBehaviour
{
    public float rechargeDuration = 1f;
    private float charge_;
    [HideInInspector]
    public float chargeMax;
    private RectTransform chargeFill_;
    private RectTransform rechargeFill_;
    private bool canThrust_ = true;

    private void Start() {
        chargeFill_ = transform.GetChild(0).GetComponent<RectTransform>();
        rechargeFill_ = transform.GetChild(1).GetComponent<RectTransform>();
    }

    public void Charge(float charge) {
        charge_ = charge;
        chargeFill_.localScale = new Vector3(charge, chargeFill_.localScale.y, chargeFill_.localScale.z);
    }

    public bool CanThrust(){
        return canThrust_;
    }

    public void Recharge(){
        StartCoroutine(RechargeCO());
    }

    private IEnumerator RechargeCO() {
        canThrust_ = false;
        chargeFill_.localScale = new Vector3(0, chargeFill_.localScale.y, chargeFill_.localScale.z);
        float elapsedTime = 0f;
        float duration = (charge_ / chargeMax);
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            rechargeFill_.localScale = new Vector3(duration * (1 - (elapsedTime / duration)), 
            rechargeFill_.localScale.y, rechargeFill_.localScale.z);
            yield return null;
        }
        rechargeFill_.localScale = new Vector3(0, rechargeFill_.localScale.y, rechargeFill_.localScale.z);
        canThrust_ = true;
    }
}
