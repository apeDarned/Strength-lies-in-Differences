using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class DetectAndUseKey : MonoBehaviour
{
    public enum KeyColor {redKey, yellowKey, blackKey, whiteKey}
    [SerializeField] private KeyColor keyColor;
    [SerializeField] private Transform unlockPosition;
    private Transform key;

    [Space][Header("Activates LevelWall")]
    [SerializeField] private Collider[] triggers;

    [Space][Header("Another Events")]
    [SerializeField] private float delayEvent;
    [SerializeField] private GameObject eventObject;

    private void OnTriggerEnter(Collider other) {
        if (other.transform.tag == keyColor.ToString()) {
            GameObject.Find("playerArms").GetComponent<ArmsGrab>().UseKey();
            key = other.transform;
            StartCoroutine(Unlock());
        }
    }
    IEnumerator Unlock() {
        yield return new WaitForSeconds(0.1f);
        key.DOMove(unlockPosition.position, 0.4f);
        //unlockSound.Play();
        yield return new WaitForSeconds(0.5f);
        key.gameObject.SetActive(false);
        OpenAttachedDoor();
    }

    [Space]
    [Header("Door and stuff")]
    [SerializeField] private Transform door;
    [SerializeField] private Transform closedPosition;
    [SerializeField] private Transform openPosition;
    [SerializeField] private bool overrideHideDoor;
    [SerializeField] private float delayBeforeOpen;
    
    [ContextMenu("OpenAttachedDoor")]
    public void OpenAttachedDoor() {
        StartCoroutine(OpenDoor());
    }
    IEnumerator OpenDoor() {
        if (callCameraTravel) {
            goPoint.GoToPoint(whereToTravel, whereToLook);
        }
        if (eventObject != null) {
            yield return new WaitForSeconds(delayEvent);
            eventObject.SetActive(true);
        }
        yield return new WaitForSeconds(delayBeforeOpen);
        door.DOMove(openPosition.position, 1);
        yield return new WaitForSeconds(1);
        if (!overrideHideDoor) {
            door.gameObject.SetActive(false);
        }
        
        if (triggers != null) {
            foreach (Collider c in triggers) {
                c.gameObject.SetActive(true);
            }
        }

    }

    [Space][Header("Camera Focus Point")]
    [SerializeField] private bool callCameraTravel;
    [SerializeField] private GoToPointAndBack goPoint;
    [SerializeField] private Transform whereToTravel;
    [SerializeField] private Transform whereToLook;


}
