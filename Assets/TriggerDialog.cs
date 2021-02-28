using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class TriggerDialog : MonoBehaviour
{
    [SerializeField] private Transform dialogParent;
    int chk;
    [SerializeField] private ParticleSystem grabingMe;
    private void Update() {
        if (chk == 1) {
            if (Input.GetKeyDown(KeyCode.L)) {
                DialogClose();
                grabingMe.Play();
                chk = 2;
            }

        } else if(chk == 2) {
            if (Input.GetKeyDown(KeyCode.L)) {
                DialogClose();
                grabingMe.Stop();
                chk = 0;
            }

        }
        
    }

    private void OnTriggerEnter(Collider other) {
        if (other.tag == "Arms") {
            Debug.Log("Dialog Grab open");
            DialogOpen();
        }
    }

    private void OnTriggerExit(Collider other) {
        if(other.tag == "Arms"){
            DialogClose();
        }
    }

    [ContextMenu("DialogOpen")]
    [ContextMenu("DialogClose")]
    public void DialogOpen() {
        dialogParent.DOScaleY(1, 0.2f).SetEase(Ease.OutElastic);
        chk = 1;
    }
    public void DialogClose() {
        dialogParent.DOScaleY(0, 0.2f).SetEase(Ease.InElastic);
        chk = 0;
    }

}
