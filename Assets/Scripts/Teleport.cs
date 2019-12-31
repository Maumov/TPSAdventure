using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    public float slowdownFactor = 0.05f;

    public float timeToSelectTeleportPosition;
    public float currentTimeRemainingForTeleport;

    public KeyCode activateTeleportKey;

    bool teleportInput;
    public bool isTeleporting;

    Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        GetInputs();
        if(isTeleporting) {
            currentTimeRemainingForTeleport -= Time.unscaledDeltaTime;
        }
        if(currentTimeRemainingForTeleport <= 0f) {
            FinishTeleport();
        }
    }

    void GetInputs() {
        //teleportInput = Input.GetKey(activateTeleportKey);
        if(Input.GetKeyDown(activateTeleportKey)) {
            StartTeleport();
        } else if(Input.GetKeyUp(activateTeleportKey)) {
            FinishTeleport();
        }
    }

    void StartTeleport() {
        currentTimeRemainingForTeleport = timeToSelectTeleportPosition;
        isTeleporting = true;
        Time.timeScale = slowdownFactor;
        Time.fixedDeltaTime = Time.timeScale * .02f;
        //anim.SetFloat("TimeScale", );
    }

    IEnumerator Teleporting() {
        yield return null;
    }


    void FinishTeleport() {
        isTeleporting = false;
        //Time.timeScale += Time.unscaledDeltaTime * 2f;
        //Time.timeScale = Mathf.Clamp(Time.timeScale, 0f, 1f);
        Time.timeScale = 1f;
        Time.fixedDeltaTime = Time.timeScale * .02f;
        //anim.SetFloat("TimeScale", 1 / slowdownFactor);
    }

    IEnumerator FinishTeleporting() {

        yield return null;
    }
}
