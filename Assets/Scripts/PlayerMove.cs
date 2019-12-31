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

    public float horizontal;
    public float virtualHorizontal;
    public float vertical;
    public float virtualVertical;

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
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");
        //Snapping
        if(Mathf.Abs(horizontal - virtualHorizontal) >= 1f) {
            virtualHorizontal = 0f;
        }
        if(Mathf.Abs(vertical - virtualVertical) >= 1f) {
            virtualVertical = 0f;
        }
        virtualHorizontal += AxisToDestination(virtualHorizontal, horizontal);
        virtualVertical += AxisToDestination(virtualVertical, vertical);

        virtualHorizontal = Mathf.Clamp(virtualHorizontal, -1f, 1f);
        virtualVertical = Mathf.Clamp(virtualVertical, -1f, 1f);
        if(Mathf.Abs(virtualHorizontal) < 0.08f && horizontal == 0f) {
            virtualHorizontal = 0f;
        }
        if(Mathf.Abs(virtualVertical) < 0.08f && vertical == 0f) {
            virtualVertical = 0f;
        }
        turn = Input.GetAxisRaw("Mouse X");
        look = -Input.GetAxisRaw("Mouse Y");
        crouch = Input.GetButton("Crouch");
    }

    float AxisToDestination(float axis, float input) {
        float value = 0;
        if(input - axis < 0) {
            value = -1f;
        }
        if(input - axis > 0) {
            value = 1f;
        }
        
        return value * Time.unscaledDeltaTime * 3f;
    }

    private void Move() {

        //Vector3 direction = horizontal * transform.right + vertical * transform.forward;
       // direction.Normalize();
        //characterController.Move(direction * speed * Time.deltaTime);
    }

    void Rotate() {
        transform.Rotate(transform.up, turn * turnSpeed * Time.unscaledDeltaTime);
        if(look != 0f ) {
            float angle = Vector3.SignedAngle(transform.forward, viewAngle.forward, transform.right);
            if(look < 0f) {
                if(angle > maxAngle ) {
                    viewAngle.RotateAround(transform.position, transform.right, look * turnSpeed * Time.unscaledDeltaTime);
                }
            }
            if( look > 0f) {
                if(angle < minAngle) {
                    viewAngle.RotateAround(transform.position, transform.right, look * turnSpeed * Time.unscaledDeltaTime);
                }
            }
        }        
    }

    void Animate() {
        anim.SetFloat("Vertical", virtualVertical);
        anim.SetFloat("Horizontal", virtualHorizontal);
        anim.SetBool("Crouching", crouch);
    }

}
