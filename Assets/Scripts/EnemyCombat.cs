using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCombat : MonoBehaviour
{
    public int currentWeapon;
    public List<GameObject> Weapons;
    public GameObject currentWeaponEquipped;
    Weapon weapon;
    public Transform weaponHold;


    RootMotion.FinalIK.FullBodyBipedIK bipedIK;
    RootMotion.FinalIK.AimIK aimIK;
    Animator anim;
    public GameObject AimPosition;
    EnemyTargeting enemyTargeting;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponentInChildren<Animator>();
        bipedIK = GetComponentInChildren<RootMotion.FinalIK.FullBodyBipedIK>();
        aimIK = GetComponentInChildren<RootMotion.FinalIK.AimIK>();
        enemyTargeting = GetComponent<EnemyTargeting>();
        anim.SetBool("Aiming", true);
        aimIK.solver.IKPositionWeight = 1f;
        EquipWeapon();
        AimPosition.transform.SetParent(null);
    }

    // Update is called once per frame
    void LateUpdate()
    {
        TargetInSight();
    }

    void TargetInSight() {
        if(enemyTargeting.currentTarget != null) {
            transform.LookAt(new Vector3(AimPosition.transform.position.x, transform.position.y, AimPosition.transform.position.z));
            Shoot();
        }
    }

    void Shoot() {
        ShotFired();
    }
    public void ShotFired() {
        if(weapon.Fire()) {
            anim.SetTrigger("Fire");
        }
    }
    public void EquipWeapon() {
        if(currentWeaponEquipped != null) {
            Destroy(currentWeaponEquipped);
            bipedIK.solver.leftHandEffector.positionWeight = 0f;
            bipedIK.solver.leftHandEffector.rotationWeight = 0f;
            bipedIK.solver.leftHandEffector.target = null;
        }

        currentWeaponEquipped = (GameObject)Instantiate(Weapons[currentWeapon], weaponHold);
        weapon = currentWeaponEquipped.GetComponent<Weapon>();
        SetHandPosition();
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
