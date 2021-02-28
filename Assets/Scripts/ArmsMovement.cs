using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmsMovement : MonoBehaviour
{
    [SerializeField] private CharacterController CCA;
    [SerializeField] private float speed;
    [SerializeField] private float rotateSpeed;
    public Vector3 direction;
    [SerializeField] private OverLegs oLegs;
    private void Update()
    {
        var inputH = Input.GetAxis("HorizontalArms");
        var inputV = Input.GetAxis("VerticalArms");

        direction = new Vector3(inputH, 0, inputV) * speed * Time.deltaTime;

        CCA.Move(direction + oLegs.newDir);

        if (direction == Vector3.zero) return;
        var rotation = Quaternion.LookRotation(direction);


        transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, rotateSpeed);

    }


    List<Ray> rays = new List<Ray>();
    List<RaycastHit> hits = new List<RaycastHit>();

    private void FixedUpdate()
    {

    }
}
