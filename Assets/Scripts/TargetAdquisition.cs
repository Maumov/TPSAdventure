using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetAdquisition : MonoBehaviour
{
   
    private void OnTriggerEnter(Collider other) {
        Stats s = other.GetComponentInParent<Stats>();

        if(s != null) {
            if(!s.name.Contains("Enemy")) {
                GetComponentInParent<EnemyTargeting>().AddTarget(s.transform);
            }
 
        }
        
    }

    private void OnTriggerExit(Collider other) {
        Stats s = other.GetComponentInParent<Stats>();
        if(s != null) {
            GetComponentInParent<EnemyTargeting>().RemoveTarget(s.transform);
        }
    }
}
