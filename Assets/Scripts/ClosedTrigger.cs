using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClosedTrigger : MonoBehaviour
{
    [SerializeField] private LevelWall lWall;
    private void OnTriggerEnter(Collider other) {
        if (other.tag == "cameraTag") {
            if (lWall.open) {
                lWall.open = false;
                lWall.CloseWall();
            } else {
                
            }
        }
    }
}
