using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    
    public float damage;
    public float speed;
    public float lifeSpan = 20f;
    Ray ray;
    RaycastHit hit;
    public LayerMask layerMask;

    public GameObject OnHitFX;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, lifeSpan);
    }

    
    // Update is called once per frame
    void Update()
    {
        Vector3 newPosition = transform.position + transform.forward * speed * Time.deltaTime;
        checkCollision();
        transform.position = newPosition;
    }

    void checkCollision() {
        float delta = speed * Time.deltaTime;
        ray.origin = transform.position;
        ray.direction = transform.forward;

        if(Physics.Raycast(ray, out hit, delta, layerMask)) {
            //Hitted a hitBox
            HitBox hitBox = hit.collider.GetComponent<HitBox>();
            if(hitBox != null) {
                hitBox.GetDamage(damage);
                hitBox.gameObject.GetComponentInParent<RootMotion.FinalIK.HitReaction>().Hit(hit.collider, ray.direction * damage, hit.point);
            }
            //Hitted an object
            Rigidbody rb = hit.collider.GetComponent<Rigidbody>();
            if(rb != null) {
                rb.AddForceAtPosition(ray.direction * damage, hit.point,ForceMode.VelocityChange);
            }
            //Particles on Hit
            if(OnHitFX != null) {
                GameObject go = (GameObject)Instantiate(OnHitFX, transform.position, Quaternion.LookRotation(hit.normal, Vector3.up));
                Destroy(go.gameObject, go.GetComponent<ParticleSystem>().duration + 1f);
            }
            //Debug.Log("Hitted " + hit.collider.name);
            Destroy(gameObject);
        }
    }
    private void OnDestroy() {
        
    }
}
