using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicMovement : MonoBehaviour
{
    public CharacterController CCL;
    [SerializeField] private float speed;
    [SerializeField] private float rotateSpeed;
    public Vector3 direction;
    
    private void Update() {
        
        var inputH = Input.GetAxis("HorizontalLegs");
        var inputV = Input.GetAxis("VerticalLegs");

        direction = new Vector3(inputH, 0, inputV) * speed * Time.deltaTime;

        CCL.Move(direction);

        
        if (direction == Vector3.zero) return;
        var rotation = Quaternion.LookRotation(direction);
        

        transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, rotateSpeed);

    }

    
    List<Ray> rays = new List<Ray>();
    List<RaycastHit> hits = new List<RaycastHit>();
    
    private void FixedUpdate() {
            
    }

}
