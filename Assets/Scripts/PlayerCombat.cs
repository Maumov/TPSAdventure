using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public int currentWeapon;
    public List<GameObject> Weapons;
    public GameObject currentWeaponEquipped;
    Weapon weapon;
    public Transform weaponHold;
    public GameObject slashPlane;
    bool fire1;
    float fire2;
    public bool isAiming;
    float x, y;
    float scrollWheel;
    RootMotion.FinalIK.FullBodyBipedIK bipedIK;
    RootMotion.FinalIK.AimIK aimIK;
    Animator anim;
    PlayerCamera playerCamera;
    // Start is called before the first frame update
    void Start() {
        playerCamera = GetComponentInChildren<PlayerCamera>();
        anim = GetComponentInChildren<Animator>();
        bipedIK = GetComponentInChildren<RootMotion.FinalIK.FullBodyBipedIK>();
        aimIK = GetComponentInChildren<RootMotion.FinalIK.AimIK>();
        anim.SetBool("Aiming", true);
        aimIK.solver.IKPositionWeight = 1f;
        EquipWeapon();
    }

    // Update is called once per frame
    void Update() {
        GetInputs();
        Aim();
        Shoot();
        WeaponChange();
    }

    void GetInputs() {
        if(weapon.isAutomatic) {
            fire1 = Input.GetButton("Fire1");
        } else {
            fire1 = Input.GetButtonDown("Fire1");
        }
        if(Input.GetButtonDown("Fire2")) {
            isAiming = true;
        }
        if(Input.GetButtonUp("Fire2")) {
            isAiming = false;
        }
        fire2 = Input.GetAxis("Fire2");
        scrollWheel = Input.GetAxis("Mouse ScrollWheel");
    }

    void Aim() {
        if(isAiming) {
            playerCamera.positionOffSet = playerCamera.aim;
        } else {
            playerCamera.positionOffSet = playerCamera.unAim;
        }
        if(currentWeapon == 0) {
            //is Sword
            playerCamera.positionOffSet = playerCamera.unAim;
            Vector2 mousePosition = new Vector2(Input.mousePosition.x / Screen.width, Input.mousePosition.y / Screen.height);

            //Calculate the angle to fire the sword

            ///
        } else {
            //is Gun Fire
        }
        
    }

    void Shoot() {
        if(fire1) {
            ShotFired();
        }
    }

    [ContextMenu("Equip Weapon")] 
    public void EquipWeapon() {
        if(currentWeaponEquipped != null) {
            Destroy(currentWeaponEquipped);
            bipedIK.solver.leftHandEffector.positionWeight = 0f;
            bipedIK.solver.leftHandEffector.rotationWeight = 0f;
            bipedIK.solver.leftHandEffector.target = null;
        }
        anim.SetBool("Sword", false);
        if(currentWeapon == 0) {
            //  is the Sword
            currentWeaponEquipped = (GameObject)Instantiate(Weapons[currentWeapon], weaponHold);
            weapon = currentWeaponEquipped.GetComponent<Weapon>();
            aimIK.solver.transform = null;

            anim.SetBool("Sword", true);
        } else {
            // Gun
            currentWeaponEquipped = (GameObject)Instantiate(Weapons[currentWeapon], weaponHold);
            weapon = currentWeaponEquipped.GetComponent<Weapon>();
            aimIK.solver.transform = weapon.muzzle;
            SetHandPosition();
        }
        
    }

    public void ShotFired() {
        if(weapon.Fire()) {
            anim.SetTrigger("Fire");
        }
    }

    public void NextWeapon() {
        currentWeapon++;
        if(currentWeapon == Weapons.Count) {
            currentWeapon = 0;
        }
        EquipWeapon();
    }

    public void PreviousWeapon() {
        currentWeapon--;
        if(currentWeapon < 0) {
            currentWeapon = Weapons.Count-1;
        }
        EquipWeapon();
    }

    void WeaponChange() {
        if(scrollWheel > 0) {
            NextWeapon();
        }
        if(scrollWheel < 0) {
            PreviousWeapon();
        }
    }

    void SetHandPosition() {
        Transform handPosition = currentWeaponEquipped.transform.GetChild(currentWeaponEquipped.transform.childCount - 1).transform;
        bipedIK.solver.leftHandEffector.target = handPosition;
        bipedIK.solver.leftHandEffector.positionWeight = 1f;

        //if(weapon.handsUsed == 2) {
        //    bipedIK.solver.leftHandEffector.rotationWeight = 1f;
        //} else {
        //    bipedIK.solver.leftHandEffector.rotationWeight = 0f;
        //}
        bipedIK.solver.leftHandEffector.rotationWeight = 0f;

        anim.SetInteger("HandsOnWeapon", weapon.handsUsed);
    }

}

