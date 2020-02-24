#pragma warning disable 649
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 7f;
    private bool porstideRotate90 = false;
    private Rigidbody2D rb_;
    private Vector2 speed_;
    private Transform spriteTransform_;
    private Transform cannonTransform_;
    private Transform firingPoint_;
    private Transform hatchTransform_;
    private Transform ejectPoint_;
    private Transform engineTransform_;
    private Animator cannonAnim_;
    [SerializeField]
    private GameObject bulletPrefab_;
    [SerializeField]
    private Ammunition ammo_;
    private CameraEffects cameraEffects_;
    private Engine engine_;

    private void Start() {
        rb_ = GetComponent<Rigidbody2D>();
        spriteTransform_ = transform.GetChild(0);
        cannonTransform_ = transform.GetChild(1);
        firingPoint_ = cannonTransform_.GetChild(1);
        hatchTransform_ = transform.GetChild(2);
        ejectPoint_ = hatchTransform_.GetChild(1);
        engineTransform_ = transform.GetChild(3);
        engine_ = engineTransform_.GetComponent<Engine>();
        cannonAnim_ = cannonTransform_.GetComponent<Animator>();
        cameraEffects_ = Camera.main.GetComponent<CameraEffects>();
    }

    private void Update() {
        LookAtCursor();
        Shoot();
        Eject();
        Reload();
        Thrust();
        PortsideRotate90();
    }

    private void FixedUpdate() {
        Move();
    }

    private void LookAtCursor() {
        Vector3 dir = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        angle -= porstideRotate90 ? 0 : 90;
        spriteTransform_.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        cannonTransform_.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        hatchTransform_.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        engineTransform_.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    private void Move() {
        speed_ = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")) * moveSpeed * Time.deltaTime;
        
        rb_.AddForce(speed_, ForceMode2D.Impulse);
    }

    private void Shoot() {
        if (Input.GetButtonDown("Fire1") && ammo_.TryToUseAmmo(1)){
            cannonAnim_.SetTrigger("recoil");
            Vector3 dir = (Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position)).normalized;
            cameraEffects_.Shake(0.3f, 0.1f, dir);
            Instantiate(bulletPrefab_, firingPoint_.position, cannonTransform_.rotation);
        }
    }

    private void Eject() {
        if (Input.GetKeyUp(KeyCode.E)){
            ammo_.Eject(ejectPoint_.position, ejectPoint_.up, ejectPoint_.rotation);
        }
    }

    private void Reload() {
        if (Input.GetKeyDown(KeyCode.R)){
            ammo_.Reload();
        }
    }

    private void Thrust(){
        if (Input.GetKey(KeyCode.Space)){
            engine_.ChargeEngine();
        }
        if (Input.GetKeyUp(KeyCode.Space)){
            engine_.EngineThrust();
        }
    }

    private void PortsideRotate90(){
        if (Input.GetButtonDown("Fire2")){
            porstideRotate90 = true;
        }
        if (Input.GetButtonUp("Fire2")){
            porstideRotate90 = false;
        }
    }

}
