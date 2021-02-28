using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveFreely : MonoBehaviour
{
    [SerializeField] private Vector3 movingDirection;
    [SerializeField] private float speed;
    void Update() {
        transform.Translate(movingDirection * speed * Time.deltaTime);
    }
}
