using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class OverLegs : MonoBehaviour
{
    [SerializeField] private Transform belowPosition;
    [SerializeField] private float size;
    [SerializeField] private LayerMask layerMask;
    public bool aboveLegs;
    public Vector3 newDir;
    
    [Space][Header("Players Texts")]
    [SerializeField] private TextMeshProUGUI playerOne;
    [SerializeField] private TextMeshProUGUI playerTwo;

    [Space][Header("Half Orange")]
    [SerializeField] private Transform legsTransform;
    [SerializeField] private Transform hopInDialog;

    bool hopOnBool;
    private void Update() {
        if (Physics.CheckSphere(belowPosition.position, size, layerMask)) {
            aboveLegs = true;
            //transform.SetParent(legsTransform);
            newDir = legsTransform.GetComponent<BasicMovement>().direction +
                transform.GetComponent<ArmsMovement>().direction;
            //hopInDialog.DOScaleY(0, 0.132f).SetEase(Ease.InElastic);
            playerOne.text = null;
            playerTwo.text = "PLAYER";
            //I should pass the hopDialog here.
            if (!hopOnBool) {
                hopOnBool = true;
                FollowPlayer.fPlayer.HideHopDialog();
            }
            
        }
        else {
            aboveLegs = false;
            newDir = Vector3.zero;
            //transform.SetParent(legsTransform);
            playerOne.text = "Player 1";
            playerTwo.text = "Player 2";
            hopOnBool = false;
        }
    }

}
