using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ObjectBehaviour : MonoBehaviour {

    public enum ActorBehaviour {stay, pattern, followPlayer, bullet, AI}
    [SerializeField] private ActorBehaviour behaviour;
    Transform player;
    bool stay, follow, bullet, ai;
    
    [Space][Header("Transforms")]
    [SerializeField] private List<Transform> patternPoints = new List<Transform>();
    [SerializeField] private Transform _base;

    [Space][Header("Pattern Settings")]
    [SerializeField] private float patternTiming;
    [SerializeField] private float waitBetweenPatternPoints;
    [SerializeField] private int indexPoint;

    [Space][Header("Follow Settings")]
    [SerializeField] private float speed;

    [Space][Header("Overriders")]
    [SerializeField] private bool overridePlayer;
    [SerializeField] private bool smoothMovement;
    [SerializeField] private float offsetDelay;

    private void Start() {
        StartCoroutine(DelayedSwitch());
    }
    IEnumerator DelayedSwitch() {
        yield return new WaitForSeconds(offsetDelay);
        switch (behaviour) {
            case ActorBehaviour.stay:
                stay = true;
                break;
            case ActorBehaviour.pattern:
                stay = true;
                StartCoroutine(PatternCo());
                break;
            case ActorBehaviour.followPlayer:
                follow = true;
                stay = true;
                break;
            case ActorBehaviour.bullet:
                bullet = true;
                break;
            case ActorBehaviour.AI:
                ai = true;
                break;
        }
        StartCoroutine(DelayedStart());
    }
    IEnumerator DelayedStart() {
        if (!overridePlayer) {
            yield return new WaitForSeconds(1);
            var r = Random.Range(0, 2);
            switch (r) {
                case 0:
                    player = GameObject.Find("playerLegs").transform;
                    break;
                case 1:
                    player = GameObject.Find("playerArms").transform;
                    break;
            }
            yield return new WaitForSeconds(4);
            StartCoroutine(DelayedStart());
        }
        

    }
    private void Update() {
        if (stay) {
            if (player != null) {
                transform.LookAt(player);
            }
        }
        if (follow) {
            if(player != null)
                transform.position = Vector3.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
        }
        if (bullet) {
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }
        if (ai) {
            GameManager.gManager.AIInstance.playerPoints = GameManager.gManager.points;
            stay = true;
            if (GameManager.gManager.AIInstance.playerPoints > 10) {
                speed *= 2;
                bullet = false;
                follow = true;
            }
            if (GameManager.gManager.AIInstance.playerPoints < 10) {
                if (player != null) {
                    var distanceToPlayer = Vector3.Magnitude(player.position - transform.position);
                    if (distanceToPlayer > 10) {
                        transform.position = Vector3.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
                    } else {
                        transform.position = Vector3.MoveTowards(transform.position, _base.position, speed * Time.deltaTime * 2);
                    }
                }
            }
            //PseudoCode:
            /*
            if(PlayersAreNotOne()){
                stay = true;
                speed *= 3;
                follow = false;
                bullet = true;
            }
             
            */
        }
    }

    IEnumerator PatternCo() {
        if (!smoothMovement) {
            transform.DOMove(patternPoints[indexPoint].position, patternTiming).SetEase(Ease.InOutCirc);
        } else {
            transform.DOMove(patternPoints[indexPoint].position, patternTiming).SetEase(Ease.InOutQuad);
        }
        
        yield return new WaitForSeconds(patternTiming + waitBetweenPatternPoints);
        if (indexPoint < patternPoints.Count - 1) {
            indexPoint++;
        } else {
            indexPoint = 0;
        }
        StartCoroutine(PatternCo());
    }



}
