using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class IntroManager : MonoBehaviour
{
    [SerializeField] private MenuSelection menuSelection;
    [SerializeField] private Image firstWhiteCanvas;
    [SerializeField] private Image secondWhiteCanvas;
    [SerializeField] private TextMeshProUGUI[] disclaimer;
    public bool chk;
    public bool skip;
    private void Start() {
        StartCoroutine(FadeOne());
    }
    private void Update() {
        if (!skip) {
            if (chk) {
                if (Input.GetKeyDown(KeyCode.Return)) {
                    skip = true;
                    StartCoroutine(FadeTwo());
                    Debug.Log("Call menu song");
                    MenuAudioManager.menuAudioManager.MenuSong();
                    
                }
            }
        }
    }

    IEnumerator FadeOne() {
        firstWhiteCanvas.DOFade(0, 1);
        yield return new WaitForSeconds(1);
        chk = true;

    }
    IEnumerator FadeTwo() {
        foreach (TextMeshProUGUI t in disclaimer) {
            t.DOFade(0, 0.2f);
        }
        yield return new WaitForSeconds(0.2f);
        secondWhiteCanvas.DOFade(0, 1);
        yield return new WaitForSeconds(1);
        menuSelection.enabled = true;
        chk = false;
        this.enabled = false;
    }
}
