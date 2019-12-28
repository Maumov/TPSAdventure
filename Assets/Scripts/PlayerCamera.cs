using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    public Transform target;
    public Transform aimTarget;
    public Vector3 positionOffSet;
    public Vector3 aim, unAim;
    public LayerMask aimLayerMask;

    Camera cam;
    RaycastHit hit;

    //RootMotion.FinalIK.AimIK aimIK;
    //public Transform aimingTransform;
    
    // Start is called before the first frame update
    void Start()
    {
        cam = GetComponent<Camera>();
        //aimIK = target.parent.GetComponent<RootMotion.FinalIK.AimIK>();
        transform.localPosition = positionOffSet;
    }

    private void Update() {
        transform.localPosition = positionOffSet;
        //aimingTransform = aimIK.transform;
    }


    // Update is called once per frame
    void LateUpdate()
    {
        //Position();
        //Rotation();
        AimCheck();
    }

    void Position() {
        Vector3 newPos = target.position;
        newPos += target.rotation * positionOffSet;
        transform.position = newPos;
    }

    //void Rotation() {
    //    transform.LookAt(target.position + (target.rotation * lookAtOffSet));
    //}

    void AimCheck() {
        if(Physics.Raycast(transform.position + (transform.forward * (-positionOffSet.z + 0.7f)), transform.forward, out hit, cam.farClipPlane, aimLayerMask)) {
            aimTarget.position = hit.point;
            //Debug.Log(hit.collider.name);
        } else {
            aimTarget.position = transform.position + transform.forward * cam.farClipPlane;
        }
    }
}
