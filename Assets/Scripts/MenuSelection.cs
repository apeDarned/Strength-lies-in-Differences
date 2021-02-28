using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;//I have to change this to a non-destructable singleton if I have time...

public class MenuSelection : MonoBehaviour {
    public int menuIndex;
    [SerializeField] private List<Transform> cameraPositions = new List<Transform>();
    [SerializeField] private Transform theCamera;
    public bool canMove = true;
    [SerializeField] GameObject[] lights;

    [Space][Header("Credits and Rulset")]
    [SerializeField] private Image creditsImage;
    [SerializeField] private Image ruleset;
    [SerializeField] private TextMeshProUGUI escInstruction;
    public bool creditsBool;
    public bool rulesetBool;

    [Space][Header("Start Game")]
    [SerializeField] private Image whiteCanvas;
    

    private void Start() {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update() {
        if (canMove) {
            if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.W)
                || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.RightArrow)) {
                MenuAudioManager.menuAudioManager.Whoosh();
                canMove = false;
                if (menuIndex == cameraPositions.Count - 1) {
                    menuIndex = 0;
                } else {
                    menuIndex++;
                }
                StartCoroutine(MoveCamera());
            }
            if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.S)
                || Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.DownArrow)) {
                MenuAudioManager.menuAudioManager.Whoosh();
                canMove = false;
                if (menuIndex == 0) {
                    menuIndex = cameraPositions.Count - 1;
                } else {
                    menuIndex--;
                }
                StartCoroutine(MoveCamera());
            }
            if (Input.GetKeyDown(KeyCode.Return)) {
                Debug.Log("Enter pressed!");
                canMove = false;
                SelectOption();
                
            }
        } else {
            if (rulesetBool) {
                if (Input.GetKeyDown(KeyCode.Escape)) {
                    StartCoroutine(HideRuleSet());
                }
            }
            if (creditsBool) {
                if (Input.GetKeyDown(KeyCode.Escape)) {
                    StartCoroutine(HideCredits());
                }
            }
        }

    }

    IEnumerator MoveCamera() {
        theCamera.DOMove(cameraPositions[menuIndex].position, 0.3f).SetEase(Ease.InOutQuad);
        MenuAudioManager.menuAudioManager.LightSwitch();
        yield return new WaitForSeconds(0.45f);
        switch (menuIndex) {
            case 0:
                lights[0].SetActive(true);
                lights[1].SetActive(false);
                lights[2].SetActive(false);
                break;
            case 1:
                lights[0].SetActive(false);
                lights[1].SetActive(true);
                lights[2].SetActive(false);
                break;
            case 2:
                lights[0].SetActive(false);
                lights[1].SetActive(false);
                lights[2].SetActive(true);
                break;
        }
        canMove = true;
    }
    public AudioSource songMenu;
    public void MenuSong(AudioSource song) {
        songMenu = song;
        Debug.Log("Playing Menu Song");
        songMenu.Play();
        
    }
    public void StopMenuSong() {
        StartCoroutine(StopMenuSongCo());

    }
    IEnumerator StopMenuSongCo() {
        songMenu.DOFade(0, 1);
        yield return new WaitForSeconds(1);
        songMenu.Stop();
    }
    void SelectOption() {
        switch (menuIndex) {
            case 0:
                canMove = false;
                Debug.Log("Game Started");
                StartCoroutine(StartGame());
                break;
            case 1:
                canMove = false;
                Debug.Log("Showing RuleSet");
                StartCoroutine(ShowRuleset());
                break;
            case 2:
                canMove = false;
                Debug.Log("Showing Credits");
                StartCoroutine(ShowCredits());
                break;
        }
        
    }
    IEnumerator ShowRuleset() {
        MenuAudioManager.menuAudioManager.MenuSelectionInput();
        ruleset.DOFade(1, 1);
        escInstruction.DOFade(1, 1.5f);
        yield return new WaitForSeconds(1);
        rulesetBool = true;
    }
    IEnumerator HideRuleSet() {
        MenuAudioManager.menuAudioManager.WhipeOut();
        ruleset.DOFade(0, 1);
        escInstruction.DOFade(0, 0.2f);
        yield return new WaitForSeconds(1);
        rulesetBool = false;
        canMove = true;
    }
    IEnumerator ShowCredits() {
        MenuAudioManager.menuAudioManager.MenuSelectionInput();
        creditsImage.DOFade(1, 1);
        escInstruction.DOFade(1, 1.5f);
        yield return new WaitForSeconds(1);
        creditsBool = true;
    }
    IEnumerator HideCredits() {
        MenuAudioManager.menuAudioManager.WhipeOut();
        creditsImage.DOFade(0, 1);
        escInstruction.DOFade(0, 0.2f);
        yield return new WaitForSeconds(1);
        creditsBool = false;
        canMove = true;
    }
    IEnumerator StartGame() {
        MenuAudioManager.menuAudioManager.StartGame();
        songMenu.DOFade(0, 2);
        whiteCanvas.DOFade(1, 2);
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(1);
    }
}
