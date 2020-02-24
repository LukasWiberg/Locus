#pragma warning disable 649
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Engine : MonoBehaviour
{
    public int maxRings = 3;
    public float delayBetweenRings = 0.2f;
    public float chargeMax = 1f;
    public float thrustForce = 20f;
    public float engineKnockbackForce = 5f;
    private float knockbackChargeMaxBonus = 15f;
    private float chargeMin = 0.1f;
    private float charge;
    [SerializeField]
    private EngineUI ui_;
    [SerializeField]
    private GameObject engineRingPrefab_;
    private Transform enginePoint_;
    private Transform enginePointTwo_;
    private KnockbackZone knockbackZone_;
    private Rigidbody2D rbParent_;
    private AudioSource thrustAudio_;

    private void Start() {
        charge = chargeMin;
        enginePoint_ = transform.GetChild(0);
        enginePointTwo_ = transform.GetChild(1);
        knockbackZone_ = transform.GetChild(2).GetComponent<KnockbackZone>();
        rbParent_ = GetComponentInParent<Rigidbody2D>();
        thrustAudio_ = GetComponent<AudioSource>();
        ui_.chargeMax = chargeMax;
    }

    public void ChargeEngine() {
        if (ui_.CanThrust()){
            if (charge < chargeMax){
                charge += Time.deltaTime;
            }
            else{
                charge = chargeMax;
            }
        ui_.Charge(charge);
        }
    }

    public void EngineThrust() {
        if (ui_.CanThrust()){
            StartCoroutine(EngineThrustCO());
        }
    }

    private IEnumerator EngineThrustCO() {
        thrustAudio_.Play();
        knockbackZone_.Knockback(engineKnockbackForce + (knockbackChargeMaxBonus * (charge / chargeMax)));
        rbParent_.velocity = Vector3.zero;
        rbParent_.AddForce(transform.up * charge * thrustForce, ForceMode2D.Impulse);
        ui_.Recharge();
        int curRings = 0;
        float delay = Mathf.Lerp(delayBetweenRings, 0.01f, charge / chargeMax);
        while (curRings < maxRings)
        {
            Instantiate(engineRingPrefab_, enginePoint_.position, transform.rotation * Quaternion.Euler(0, 0, 90));
            Instantiate(engineRingPrefab_, enginePointTwo_.position, transform.rotation * Quaternion.Euler(0, 0, 90));
            var elapsedTime = 0f;
            while (elapsedTime < delay)
            {
                elapsedTime += Time.deltaTime;
                yield return null;
            }
            curRings++;
            yield return null;
        }
        charge = chargeMin;

    }
}
