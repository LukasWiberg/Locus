using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioObject : MonoBehaviour
{
    public AudioClip clip;
    private AudioSource audio_;

    private void Awake() {
        audio_ = GetComponent<AudioSource>();
    }

    public void SetParams(AudioClip clip){
        audio_.clip = clip;
        if (audio_.clip == null){
            Debug.LogError("AudioObject created without audio clip!");
        }
        audio_.Play();
    }
    public void SetParams(AudioClip clip, float volume){
        audio_.clip = clip;
        audio_.volume = Mathf.Clamp01(volume);
        if (audio_.clip == null){
            Debug.LogError("AudioObject created without audio clip!");
        }
        audio_.Play();
    }

    private void LateUpdate() {
        if (!audio_.isPlaying){
            Destroy(gameObject);
        }
    }
}
