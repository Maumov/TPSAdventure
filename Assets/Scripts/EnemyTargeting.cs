using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTargeting : MonoBehaviour
{
    public List<Transform> posibleTargets;
    public Vector3 currentTargetPosition;

    public Vector3 viewPosition;
    public Transform currentTarget;
    public SphereCollider targetingAdquisitionRadius;
    public float adquisitionRadius = 10f;

    Ray ray;
    RaycastHit hit;
    public LayerMask layerMask;
    EnemyCombat combat;
    // Start is called before the first frame update
    void Start() {
        combat = GetComponent<EnemyCombat>();
        targetingAdquisitionRadius.radius = adquisitionRadius;
        StartCoroutine(CurrentTarget());
        StartCoroutine(LastPositionOfTarget());
    }

    IEnumerator LastPositionOfTarget() {
        while(enabled) {
            if(currentTarget != null) {
                currentTargetPosition = currentTarget.position;
            }
            yield return new WaitForSecondsRealtime(3f);
        }
    }

    IEnumerator CurrentTarget() {
        while(enabled) {
            if(posibleTargets.Count <= 0) {
                currentTarget = null;
                combat.AimPosition.transform.parent = null;
                yield return null;
            } else {
                if(currentTarget != null) {
                    //Can still attack current target??
                    Stats s = currentTarget.GetComponentInParent<Stats>();
                    HitBox hitbox = CheckCollidersOfTarget(s);
                    if(hitbox != null) {
                        currentTarget = hitbox.transform;
                        combat.AimPosition.transform.parent = currentTarget;
                        combat.AimPosition.transform.localPosition = Vector3.zero;
                        
                        //is still hittable!!! Keep Attacking !!!
                    } else {
                        currentTarget = null;
                        combat.AimPosition.transform.parent = null;
                    }
                    yield return new WaitForSecondsRealtime(1f);
                } else {
                    //Search for next Target
                    FindNewTarget();
                    yield return new WaitForSecondsRealtime(1f);
                }
            }
        }
        yield return null;
    }

    void FindNewTarget() {
        for(int i = 0; i < posibleTargets.Count; i++) {
            if(isInFrontOfMe(posibleTargets[i].position)) {
                Stats s = posibleTargets[i].GetComponent<Stats>();
                HitBox hitbox = CheckCollidersOfTarget(s);
                if(hitbox != null) {
                    currentTarget = hitbox.transform;
                    combat.AimPosition.transform.parent = currentTarget;
                    combat.AimPosition.transform.localPosition = Vector3.zero;
                    return;
                }
            }
        }
    }

    HitBox CheckCollidersOfTarget(Stats stats) {
        Collider[] colliders = stats.colliders;
        float maxDamage = 0;
        HitBox hitbox = null;
        ray.origin = transform.position + (transform.rotation * viewPosition);
        for(int c = 0; c < colliders.Length; c++) {
            ray.direction = colliders[c].transform.position - ray.origin;
            if(Physics.Raycast(ray, out hit, 100f, layerMask)) {
                Stats s = hit.collider.GetComponentInParent<Stats>();
                if(s != null) {
                    if(s.Equals(colliders[c].GetComponentInParent<Stats>())) {
                        HitBox h = hit.collider.GetComponent<HitBox>();
                        if(h.damageBonus >= maxDamage) {
                            hitbox = h;
                            maxDamage = h.damageBonus;
                        }
                    }
                }
            }
        }
        return hitbox;
    }

    bool isInFrontOfMe(Vector3 targetPosition) {
        return Vector3.Angle(transform.forward, targetPosition - transform.position) < 80f;
    }

    public void AddTarget(Transform newTarget) {
        if(newTarget != transform) {
            if(!posibleTargets.Contains(newTarget)) {
                posibleTargets.Add(newTarget);
            }
        }
    }

    public void RemoveTarget(Transform newTarget) {
        if(newTarget != transform) {
            if(posibleTargets.Contains(newTarget)) {
                posibleTargets.Remove(newTarget);
            }
        }
    }

}
