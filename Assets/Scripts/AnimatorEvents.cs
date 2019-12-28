using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorEvents : MonoBehaviour
{
    PlayerCombat playerCombat;
    // Start is called before the first frame update
    void Start()
    {
        playerCombat = GetComponent<PlayerCombat>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FootL() {
    
    }

    void FootR() {

    }

    void Shot() {
        playerCombat.ShotFired(); 
    }
}
