using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HowToPlay : MonoBehaviour
{
    private TextMeshProUGUI text_;

    private void Start() {
        text_ = GetComponent<TextMeshProUGUI>();
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)){
            text_.enabled = !text_.enabled;
        }
    }
}
