using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockbackZone : MonoBehaviour
{
    private Collider2D col_;
    private Transform engineTransform_;
    private float force_;

    private void Start() {
        col_ = GetComponent<Collider2D>();
        col_.enabled = false;
        engineTransform_ = transform.parent;
    }

    public void Knockback(float force) {
        StartCoroutine(KnockbackCO(0.1f));
        force_ = force;
    }

    private IEnumerator KnockbackCO(float duration) {
        col_.enabled = true;
        while (duration > 0)
        {
            duration -= Time.deltaTime;
            yield return null;
        }
        col_.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        //Call on all tags except ones below
        if (other.tag != "Cartridge"){
            if (other.TryGetComponent(out Rigidbody2D rb)){
                var dir = (other.transform.position - engineTransform_.position).normalized;
                rb.AddForce(dir * force_, ForceMode2D.Impulse);
            }
            else if (other.transform.root.TryGetComponent(out Rigidbody2D rbRoot)){
                var dir = (other.transform.position - engineTransform_.position).normalized;
                rbRoot.AddForce(dir * force_, ForceMode2D.Impulse);
            }
        }
    }

}
