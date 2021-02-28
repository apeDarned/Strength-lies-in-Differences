using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FollowPlayer : MonoBehaviour
{

    [Header("Dependencies")]
    [SerializeField] private Transform playerOne;
    [SerializeField] private Transform playerTwo;
    [SerializeField] private Transform middlePoint;
    public Vector3 distanceVector;
    public float distanceMagnitude;
    
    [Space][Header("Limits")]
    [SerializeField] private float inferiorLimit;
    [SerializeField] private float superiorLimit;

    [Space][Header("Camera Settings")]
    [SerializeField] private Camera theCamera;
    [SerializeField] private Vector3 offset;

    [Space][Header("Timing and speed")]
    [SerializeField] private float fovChangeTime;
    [SerializeField] private float followTiming;

    [Space] [Header("FOVS")]
    [SerializeField] private float normalFOV;
    [SerializeField] private float minFOV;
    [SerializeField] private float maxFOV;

    public static FollowPlayer fPlayer;
    private void Awake() {
        if (!fPlayer) {
            fPlayer = this;
        } else {
            Destroy(gameObject);
        }
    }

    private void Update() {
        if (!stopAll) {
            distanceVector = playerTwo.position - playerOne.position;
            distanceMagnitude = distanceVector.magnitude;
            var x = (playerTwo.position.x + playerOne.position.x) / 2;
            var y = (playerTwo.position.y + playerOne.position.y) / 2;
            var z = (playerTwo.position.z + playerOne.position.z) / 2;

            middlePoint.position = new Vector3(x, y, z);

            theCamera.transform.DOMove(middlePoint.position + offset, followTiming);

            if (distanceMagnitude >= inferiorLimit && distanceMagnitude < superiorLimit) {
                theCamera.DOFieldOfView(minFOV, fovChangeTime).SetEase(Ease.InOutCirc);
                matchReady = false;
            } else if (distanceMagnitude >= inferiorLimit && distanceMagnitude > superiorLimit) {
                theCamera.DOFieldOfView(maxFOV, fovChangeTime).SetEase(Ease.InOutCirc);
                matchReady = false;
            } else if (distanceMagnitude <= inferiorLimit && distanceMagnitude < superiorLimit) {
                theCamera.DOFieldOfView(normalFOV, fovChangeTime).SetEase(Ease.InOutCirc);
                matchReady = true;
            }
        }

        if (matchReady && !lightOn) {
            lightOn = true;
            TurnOnLights();
        }

        if (!matchReady && lightOn) {
            lightOn = false;
            TurnOffLights();
        }

    }

    private bool stopAll;
    [ContextMenu("FocusPlayerOne")]
    public void FocusPlayerOne() {
        stopAll = true;
        theCamera.transform.DOMove(playerOne.position + offset, 0.5f);
        theCamera.DOFieldOfView(33, 0.5f);
    }
    [ContextMenu("FocusPlayerTwo")]
    public void FocusPlayerTwo() {
        stopAll = true;
        theCamera.transform.DOMove(playerTwo.position + offset, 0.5f);
        theCamera.DOFieldOfView(33, 0.5f);
    }








    ///I shouldn't but we are going to abuse the properties of this script to make something cool. Later if I have time,
    ///I will separate this behaviour into another script. 
    /// PLAYER LIGHTS
    /// 
    bool matchReady;
    [SerializeField] private GameObject playerOneLight;
    [SerializeField] private GameObject playerTwoLight;
    bool lightOn;
    [SerializeField] private Transform dialogHopOver;
    bool hopOnBool;
    public void TurnOnLights() {
        playerOneLight.SetActive(true);
        playerTwoLight.SetActive(true);
        if (!hopOnBool) {
            hopOnBool = true;
            dialogHopOver.DOScaleY(1, 0.132f).SetEase(Ease.OutElastic);
        }
        
        AudioManager.audioManager.PlayerLights();
        AudioManager.audioManager.HopOn();
    }
    public void TurnOffLights() {
        playerOneLight.SetActive(false);
        playerTwoLight.SetActive(false);
        HideHopDialog();
        AudioManager.audioManager.PlayerLights();
        hopOnBool = false;
    }
    public void HideHopDialog() {
        dialogHopOver.DOScaleY(0, 0.132f).SetEase(Ease.InElastic);
    }

}
