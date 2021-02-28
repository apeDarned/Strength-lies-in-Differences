using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class levelEnd : MonoBehaviour
{
    bool boolCHk;
    [SerializeField] private Camera theCamera;
    [SerializeField] private ArmsMovement armsMovement;
    [SerializeField] private BasicMovement legsMovement;
    [SerializeField] private Image whiteCanvas;
    [SerializeField] private Image thanksScreen;
    [SerializeField] private AudioSource levelEndMusic;
    [SerializeField] private AudioSource levelSong;
    [SerializeField] private List<ObjectBehaviour> objectsBehavioursToStop = new List<ObjectBehaviour>();

    private void Update() {
        if (boolCHk) {
            if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Escape)) {
                StartCoroutine(ToMainMenu());
            }
        }
    }

    private void OnTriggerEnter(Collider other) {
        Debug.Log("LEVEL END");
        if (other.tag == "Arms" || other.tag == "Legs") {
            StartCoroutine(LevelEnd());
        }
    }
    IEnumerator LevelEnd() {
        armsMovement.enabled = false;
        legsMovement.enabled = false;
        theCamera.gameObject.GetComponent<FollowPlayer>().enabled = false;
        foreach (ObjectBehaviour b in objectsBehavioursToStop) {
            b.enabled = false;
        }

        theCamera.DOFieldOfView(33, 1);
        levelSong.DOFade(0, 0.2f);
        yield return new WaitForSeconds(0.2f);
        levelEndMusic.Play();
        yield return new WaitForSeconds(4);
        thanksScreen.DOFade(1, 1);
        boolCHk = true;
    }
    IEnumerator ToMainMenu() {
        whiteCanvas.DOFade(1, 1);
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(0);
        
    }

}
