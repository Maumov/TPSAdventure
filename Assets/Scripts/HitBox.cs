using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBox : MonoBehaviour
{
    public float damageBonus = 20f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void GetDamage(float damage) {
        GetComponentInParent<Stats>().ReceiveDamage(damage * damageBonus);
    }
}
