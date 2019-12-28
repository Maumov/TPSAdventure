using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Stats : MonoBehaviour
{
    public RootMotion.FinalIK.RagdollUtility ragdollUtility;
    public float Health;
    CharacterController characterController;
    RootMotion.FinalIK.AimIK aimIK;
    public Collider[] colliders;
    public UnityEvent OnDead;

    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        aimIK = GetComponentInChildren<RootMotion.FinalIK.AimIK>();
        colliders = GetComponentsInChildren<Collider>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ReceiveDamage(float damage) {
        Health -= damage;
        if(Health < 0) {
            Dead();
        }

    }

    void Dead() {
        ragdollUtility.EnableRagdoll();
        if(characterController != null) {
            characterController.enabled = false;
        }
        aimIK.enabled = false;
        OnDead.Invoke();
    }

    void Revive() {
    
    }
}
