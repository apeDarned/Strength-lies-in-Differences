using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmsGrab : MonoBehaviour
{
    [SerializeField] private Transform hand;
    [SerializeField] private float handSize;
    [SerializeField] private LayerMask layerMask;
    public Collider[] isReachable;
    public List<Collider> grabed = new List<Collider>();
    public bool isGrabing;

    //I have to try this chunk with care...
    //I'm  making this a sigleton but I don't want it to be under DontDestroyOnLoad()...
    public static ArmsGrab aGrabClass;
    private void Awake() {
        if (!aGrabClass) {
            aGrabClass = this;
        } else {
            Destroy(this.gameObject);
        }
    }

    private void Update() {
        isReachable = Physics.OverlapSphere(hand.position, handSize, layerMask);
        if (isReachable.Length > 0 && !isGrabing) {
            Debug.Log("Reach");
            if (Input.GetButtonDown("Grab")) {
                Debug.Log("Grabing");
                foreach (Collider c in isReachable) {
                    grabed.Add(c);
                    
                    c.transform.SetParent(hand);
                    if (c.gameObject.tag == "weapon") {
                        c.gameObject.GetComponent<WeaponFire>().BeginFire();
                    }
                }
                isGrabing = true;

            }
        } else if (isReachable.Length > 0 && isGrabing) {
            if (Input.GetButtonDown("Grab")) {
                foreach (Collider c in isReachable) {
                    grabed.Remove(c);
                    c.transform.SetParent(null);
                    if (c.gameObject.tag == "weapon") {
                        c.gameObject.GetComponent<WeaponFire>().StopFire();
                    }
                }
                isGrabing = false;
            }
        }
    }

    public void UseKey() {//Even if this refers to any c in isReacheble, (not only a key), this method will be called only by a keyLock and we meta-know that we are realeasing a key.
        foreach (Collider c in isReachable) {
            grabed.Remove(c);
            c.transform.SetParent(null);
        }
        isGrabing = false;
    }
    public void ReleaseWeapon() {
        foreach (Collider c in isReachable) {
            if (c.gameObject.tag == "weapon") {
                grabed.Remove(c);
                isGrabing = false;
            }
        }
    }

}
