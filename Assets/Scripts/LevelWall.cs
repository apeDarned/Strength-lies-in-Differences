using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class LevelWall : MonoBehaviour {
    [Header("Transforms")]
    [SerializeField] private Transform actor;
    [SerializeField] private Transform openPosition;
    [SerializeField] private Transform closedPosition;

    [Space][Header("Timings")]
    [SerializeField] private float transitionTime;
        
    [Space][Header("Booleans")]
    public bool open;

    public void OpenWall() {
        actor.transform.DOMove(closedPosition.position, transitionTime);
    }

    public void CloseWall() {
        actor.transform.DOMove(openPosition.position, transitionTime);
    }

}
