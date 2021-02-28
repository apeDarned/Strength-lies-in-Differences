using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed;
    public float damage;

    private void Start() {
        //Destroy(gameObject, 2);
    }

    private void Update() {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }
    private void OnCollisionEnter(Collision collision) {
        Debug.Log("Destroy");
        Destroy(gameObject);
    }

}
