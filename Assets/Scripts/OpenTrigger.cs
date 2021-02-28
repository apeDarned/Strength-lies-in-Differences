using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenTrigger : MonoBehaviour
{
    [SerializeField] private LevelWall lWall;

    private void OnTriggerEnter(Collider other) {
        if (other.transform.tag == "cameraTag") {
            if (!lWall.open) {
                lWall.open = true;
                lWall.OpenWall();
            }
        }
        
    }

}
