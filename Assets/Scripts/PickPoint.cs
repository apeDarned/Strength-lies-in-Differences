using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickPoint : MonoBehaviour
{
    [Header("Positional Shadow Caster")]
    [SerializeField] private GameObject positionalShadow;
    private void Awake() {
        GameObject parentPoint = new GameObject("parentPoint");
        transform.SetParent(parentPoint.transform);
    }
    private void Start() {
        Ray r = new Ray(transform.position, Vector3.down);
        RaycastHit h;
        if (Physics.Raycast(r, out h)) {
            var t = Instantiate(positionalShadow, h.point, Quaternion.identity);
            t.transform.SetParent(transform.parent);
        }
    }

    [Space][Header("Rotate")]
    [SerializeField] private float x;
    [SerializeField] private float y;
    [SerializeField] private float z;
    [SerializeField] private float rotSpeed;
    private void Update() {
        transform.Rotate(new Vector3(x, y, z) * rotSpeed * Time.deltaTime);
    }

    [Space][Header("Point Attributes")]
    [SerializeField] private int pointValue;
    [SerializeField] private bool enemyCanEat;
    public bool playerTwoPoint;
    private void OnTriggerEnter(Collider other) {
        switch (other.tag) {
            case "enemy":
                if (enemyCanEat) {
                    GameManager.gManager.EnemyPoints(pointValue);
                    transform.parent.gameObject.SetActive(false);
                    //Destroy(gameObject.transform.parent);
                }

                break;
            case "Legs":
                if (playerTwoPoint) {

                } else {
                    AudioManager.audioManager.PointSound();
                    GameManager.gManager.Points(pointValue);
                    transform.parent.gameObject.SetActive(false);
                    //Destroy(gameObject.transform.parent);
                }
                break;
            case "Arms":
                if (!playerTwoPoint) {

                } else {
                    AudioManager.audioManager.PointSound();
                    GameManager.gManager.Points(pointValue);
                    transform.parent.gameObject.SetActive(false);
                    //Destroy(gameObject.transform.parent);
                }
                break;
                

        }
    }
}

