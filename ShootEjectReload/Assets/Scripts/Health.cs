#pragma warning disable 649
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public float startHp = 10;
    private float curHp_;
    private RandomBlinker blinker_;
    private bool hasBlinker_ = false;
    [SerializeField]
    private GameObject explosionPrefab_;

    private void Start() {
        if (TryGetComponent(out RandomBlinker blinker)){
            blinker_ = blinker;
            hasBlinker_ = true;
        }

        curHp_ = startHp;
    }

    public void TakeDamage(float damage){
        if (hasBlinker_){
            blinker_.Blink();
        }

        curHp_ -= damage;
        if (curHp_ <= 0){
            Die();
        }
    }

    private void Die(){
        Instantiate(explosionPrefab_, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
