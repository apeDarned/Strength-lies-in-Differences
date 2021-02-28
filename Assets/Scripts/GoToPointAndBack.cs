using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GoToPointAndBack : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private FollowPlayer fp;

    [Space] [Header("Transforms")]
    private Camera theCamera;
    private Vector3 here;
    private Transform there;
    private Transform whereToLook;

    [Space][Header("Timings")]
    [SerializeField] private float travelTime;
    [SerializeField] private float waitBeforeComingBack;

    [Space][Header("Booleans")]
    private bool lookThere;

    public static GoToPointAndBack goToPointAndBack;
    private void Awake() {
        if (!goToPointAndBack) {
            goToPointAndBack = this;//Not sure if this in creating a singleton... I think not... Must research...
        } else {
            Destroy(this.gameObject);
        }
        theCamera = transform.GetComponent<Camera>();
    }
    public void GoToPoint(Transform goingPoint, Transform _whereToLook) {
        fp.enabled = false;
        whereToLook = _whereToLook;
        there = goingPoint;
        here = transform.position;
        StartCoroutine(GoThere());
    }
    IEnumerator GoThere() {
        theCamera.transform.DOMove(there.position, travelTime).SetEase(Ease.InOutQuad);
        yield return new WaitForSeconds(travelTime + 0.5f);
        //lookThere = true;
        yield return new WaitForSeconds(waitBeforeComingBack + 1);
        //theCamera.transform.DOMove(here, travelTime).SetEase(Ease.InOutQuad);
        //yield return new WaitForSeconds(travelTime + 0.5f);
        lookThere = false;
        fp.enabled = true;
       
    }

    private void Update() {
        if (lookThere) {
            transform.DOLookAt(whereToLook.position, 1);
        }
    }

}
