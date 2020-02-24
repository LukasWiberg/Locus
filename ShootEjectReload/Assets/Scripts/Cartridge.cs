#pragma warning disable 649
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cartridge : MonoBehaviour
{
    public float fillAmount = 1;
    public Vector2 force = Vector2.zero;
    private Transform fill_;
    private Rigidbody2D rb_;
    [SerializeField]
    private GameObject explosionPrefab_;
    private bool isEmpty_ = false;
    [SerializeField]
    private AudioClip emptyHitClip_;
    [SerializeField]
    private GameObject audioObjectPrefab_;

    private void Awake() {
        fill_ = transform.GetChild(1);
        rb_ = GetComponent<Rigidbody2D>();
    }

    private void Start() {
        fill_.localScale = new Vector3(fillAmount, fill_.localScale.y, fill_.localScale.z);
        isEmpty_ = (fillAmount == 0 ? true : false);
        rb_.AddForce(force, ForceMode2D.Impulse);
    }

    ///<summary>
    ///Will not explode if cartridge is empty
    ///</summary>
    public bool TryExplode() {
        if (!isEmpty_){
            Instantiate(explosionPrefab_, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
        return !isEmpty_;
    }
    public bool TryExplode(float duration) {
        if (!isEmpty_){
            StartCoroutine(ExplodeCO(duration));
        }
        return !isEmpty_;
    }

    public void ForceExplode() {
        Instantiate(explosionPrefab_, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
    public void ForceExplode(float duration) {
        StartCoroutine(ExplodeCO(duration));
    }

    public void PlayEmptyHitSound(){
        var obj = Instantiate(audioObjectPrefab_, transform.position, Quaternion.identity);
        obj.GetComponent<AudioObject>().SetParams(emptyHitClip_, 0.4f);
    }

    private IEnumerator ExplodeCO(float duration){
        while (duration >= 0)
        {
            duration -= Time.deltaTime;
            yield return null;
        }
        Instantiate(explosionPrefab_, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
