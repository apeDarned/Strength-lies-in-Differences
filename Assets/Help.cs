using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using TMPro;
public class Help : MonoBehaviour
{
    public static Help help;

    private void Awake() {
        if (!help) {
            help = this;
        } else {
            Destroy(this);
        }
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)){
            if (!helpActive) {
                ShowHelp();
                helpActive = true;
            } else {
                HideHelp();
                helpActive = false;
            }
            
        }
    }
    [SerializeField] private Image keyBoardLayout;
    bool helpActive;
    [ContextMenu("ShowHelp")]
    [ContextMenu("HideHelp")]
    public void ShowHelp() {
        
        Time.timeScale = 0;
        StartCoroutine(HelpShow());
    }
    IEnumerator HelpShow() {
        //Debug.Log("Showing Help");
        keyBoardLayout.DOFade(1, 1).SetUpdate(true);
        helpActive = true;
        //Time.timeScale = 0;
        yield return new WaitForSecondsRealtime(0.1f);

    }
    public void HideHelp() {
        Debug.Log("Hiding Help");
        StartCoroutine(HelpHide());
    }
    IEnumerator HelpHide() {
        keyBoardLayout.DOFade(0, 1).SetUpdate(true);
        helpActive = true;
        Time.timeScale = 1;
        yield return new WaitForSecondsRealtime(0.1f);
    }

}
