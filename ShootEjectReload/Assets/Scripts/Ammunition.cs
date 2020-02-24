#pragma warning disable 649
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Ammunition : MonoBehaviour
{
    public int maxAmmo = 10;
    public float cartridgeEjectSpeed = 1f;
    public float ejectDuration = 0.2f;
    public float reloadDuration = 1f;
    private int curAmmo;
    private RectTransform ammoFill_;
    private RectTransform actionFill_;
    private bool canEject_ = true;
    private bool canReload_ = false;
    [SerializeField]
    private GameObject cartridgePrefab_;
    private CameraEffects cameraEffects_;
    private enum AmmoText{
        None,
        Eject,
        Ejecting,
        Reload,
        Reloading
    }
    private TextMeshProUGUI text_;
    private TextBlink textBlink_;
    private AudioSource ejectAudio_;
    private AudioSource reloadAudio_;
    private AudioSource emptyClipAudio_;

    private void Start() {
        curAmmo = maxAmmo;
        ammoFill_ = transform.GetChild(0).GetComponent<RectTransform>();
        actionFill_ = transform.GetChild(1).GetComponent<RectTransform>();
        cameraEffects_ = Camera.main.GetComponent<CameraEffects>();
        text_ = transform.GetChild(2).GetComponent<TextMeshProUGUI>();
        textBlink_ = text_.GetComponent<TextBlink>();
        ejectAudio_ = transform.GetChild(3).GetComponent<AudioSource>();
        reloadAudio_ = transform.GetChild(4).GetComponent<AudioSource>();
        emptyClipAudio_ = transform.GetChild(5).GetComponentInParent<AudioSource>();
    }

    private void DisplayText(AmmoText text) {
        switch (text)
        {
            case AmmoText.Eject:
                text_.text = "EJECT!";
                text_.margin = new Vector4(0, 0, 0, 0);
            break;
            case AmmoText.Ejecting:
                text_.text = "EJECTING...";
                text_.margin = new Vector4(28, 0, 0, 0);
            break;
            case AmmoText.Reload:
                text_.text = "RELOAD!";
                text_.margin = new Vector4(10, 0, 0, 0);
            break;
            case AmmoText.Reloading:
                text_.text = "RELOADING...";
                text_.margin = new Vector4(30, 0, 0, 0);
            break;
            default:
                text_.text = "";
            break;
        }
    }

    public void Reload() {
        if (canReload_){
            StartCoroutine(ReloadCO());
        }
    }

    private IEnumerator ReloadCO() {
        canReload_ = false;
        textBlink_.StopBlink();
        DisplayText(AmmoText.Reloading);
        reloadAudio_.Play();
        float elapsedTime = 0;
        while (elapsedTime <= reloadDuration)
        {
            actionFill_.localScale = new Vector3((elapsedTime / reloadDuration), actionFill_.localScale.y, actionFill_.localScale.z);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        curAmmo = maxAmmo;
        ammoFill_.localScale = new Vector3(1, ammoFill_.localScale.y, ammoFill_.localScale.z);
        actionFill_.localScale = new Vector3(0, actionFill_.localScale.y, actionFill_.localScale.z);
        DisplayText(AmmoText.None);
        canEject_ = true;
    }

    public void Eject(Vector3 position, Vector3 direction, Quaternion rotation) {
        if (canEject_){
            StartCoroutine(EjectCO(position, direction, rotation));
            cameraEffects_.Shake(0.3f, 0.15f, direction);
        }
    }

    private IEnumerator EjectCO(Vector3 position, Vector3 direction, Quaternion rotation) {
        canEject_ = false;
        textBlink_.StopBlink();
        DisplayText(AmmoText.Ejecting);
        ejectAudio_.Play();
        var obj = Instantiate(cartridgePrefab_, position, rotation);
        var cartridge = obj.GetComponent<Cartridge>();
        cartridge.fillAmount = (float)curAmmo / (float)maxAmmo;
        cartridge.force = direction.normalized * cartridgeEjectSpeed;
        ammoFill_.localScale = new Vector3(0, ammoFill_.localScale.y, ammoFill_.localScale.z);
        curAmmo = 0;

        float elapsedTime = 0;
        while (elapsedTime <= ejectDuration)
        {
            actionFill_.localScale = new Vector3((elapsedTime / ejectDuration), actionFill_.localScale.y, actionFill_.localScale.z);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        actionFill_.localScale = new Vector3(0, actionFill_.localScale.y, actionFill_.localScale.z);
        DisplayText(AmmoText.Reload);
        canReload_ = true;
    }

    ///<summary>
    ///Returns true if can shoot, false if not enough ammo remaining
    ///</summary>
    public bool TryToUseAmmo(int amount) {
        curAmmo -= amount;
        if (curAmmo < 0){
            curAmmo += amount;
            if (!canReload_ && canEject_){
                DisplayText(AmmoText.Eject);
            }
            if ((!canReload_ && canEject_) || canReload_){
                textBlink_.Blink(Color.red, 0.2f);
                emptyClipAudio_.Play();
            }
            return false;
        }

        ammoFill_.localScale = new Vector3(((float)curAmmo / (float)maxAmmo), ammoFill_.localScale.y, ammoFill_.localScale.z);
        return true;
    }
}
