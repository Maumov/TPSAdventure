using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Weapon : MonoBehaviour
{
    public int handsUsed = 1;
    public bool isAutomatic = false;
    public GameObject bullet;
    public Transform muzzle;
    public ParticleSystem ShootingEffect;
    public float cooldown = 1f;
    float nextShot;
    Vector3 position;
    Vector3 forward;
    // Start is called before the first frame update
    void Start()
    {
        nextShot = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        position = muzzle.transform.position;
        forward = muzzle.transform.forward;
    }

    public bool Fire() {
        if(nextShot < Time.time) {
            if(bullet != null) {
                //is Fire Weapon
                GameObject go = Instantiate(bullet, position, Quaternion.LookRotation(forward, Vector3.up));
                if(ShootingEffect != null) {
                    ShootingEffect.Play();
                }
                nextShot = Time.time + cooldown;
                return true;
            } else {
                //is Melee
                return false;
            }
            //GameObject go = Instantiate(bullet, muzzle.transform.position, muzzle.transform.rotation);
            
        } else {
            return false;
        }
        
    }
}
