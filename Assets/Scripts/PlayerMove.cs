using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{

    public float speed;
    public float turnSpeed;

    public Transform viewAngle;
    public float minAngle;
    public float maxAngle;

    float horizontal;
    float vertical;
    float turn;
    float look;
    bool crouch;

    //CharacterController characterController;
    Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        //characterController = GetComponent<CharacterController>();
        anim = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        GetInputs();
        //Move();
        Rotate();
        Animate();
    }

    private void GetInputs() {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
        turn = Input.GetAxis("Mouse X");
        look = -Input.GetAxis("Mouse Y");
        crouch = Input.GetButton("Crouch");
    }

    private void Move() {

        //Vector3 direction = horizontal * transform.right + vertical * transform.forward;
       // direction.Normalize();
        //characterController.Move(direction * speed * Time.deltaTime);
    }

    void Rotate() {
        transform.Rotate(transform.up, turn * turnSpeed * Time.deltaTime);
        if(look != 0f ) {
            float angle = Vector3.SignedAngle(transform.forward, viewAngle.forward, transform.right);
            if(look < 0f) {
                if(angle > maxAngle ) {
                    viewAngle.RotateAround(transform.position, transform.right, look * turnSpeed * Time.deltaTime);
                }
            }
            if( look > 0f) {
                if(angle < minAngle) {
                    viewAngle.RotateAround(transform.position, transform.right, look * turnSpeed * Time.deltaTime);
                }
            }
        }        
    }

    void Animate() {
        anim.SetFloat("Vertical", vertical);
        anim.SetFloat("Horizontal", horizontal);
        anim.SetBool("Crouching", crouch);
    }

}
