using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LegsJump : MonoBehaviour
{
    [Header("Gravity and Jump")]
    public float gravity;
    public Vector3 fallSpeed;
    public float height;
    [SerializeField] private Transform groundChkr;
    [SerializeField] private float grndDist;
    [SerializeField] private LayerMask layerMask;
    [HideInInspector]
    public bool isGround;
    [SerializeField] private CharacterController controller;
    bool jumping;
    [SerializeField] private OverLegs oLegs;

    [Space][Header("HalfOrange")]
    [SerializeField] private Transform arms;
    [SerializeField] private ArmsJump armsJump;

    private void Update() {
        isGround = Physics.CheckSphere(groundChkr.position, grndDist, layerMask);
        if (isGround && fallSpeed.y < 0) {
            fallSpeed.y = -0.1f;
        }
        if (Input.GetButtonDown("Jump") && isGround) {
            jumping = true;
        }
    }


    private void FixedUpdate() {
        if (jumping) {
            jumping = false;
            fallSpeed.y = Mathf.Sqrt(height * -2.0f * gravity * Time.deltaTime);//As this only occurs once, we may change this for a constant
            if (oLegs.aboveLegs) {
                armsJump.fallSpeed += fallSpeed;
            }
        }
        fallSpeed.y += (gravity) * Mathf.Pow(Time.deltaTime, 2);
        controller.Move(fallSpeed);
    }

}
