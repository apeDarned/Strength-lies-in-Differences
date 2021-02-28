using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;
using UnityEngine.SceneManagement;

public class Manager : MonoBehaviour {
    public static Manager manager;

    private void Awake() {
        if (!manager) {
            manager = this;
        } else {
            Destroy(this.gameObject);
        }
    }

    [Space] [Header("Player One")]
    [SerializeField] private GameObject playerOne;
    [SerializeField] private BasicMovement bMovement;
    [SerializeField] private LegsJump lJump;
    [SerializeField] private BelowArms bArms;

    [Space] [Header("Player Two")]
    [SerializeField] private GameObject playerTwo;
    [SerializeField] private ArmsMovement aMovement;
    [SerializeField] private OverLegs oLegs;
    [SerializeField] private ArmsJump aJump;

    [Space] [Header("Game Over")]
    [SerializeField] private Transform looser;
    [SerializeField] private Image gameOverImage;
    [SerializeField] private TextMeshProUGUI gameOverMessageText;
    [SerializeField] private TextMeshProUGUI gameOverInstructionText;
    [SerializeField] private List<string> gameOverLegend = new List<string>();
    [SerializeField] private List<string> gameOverInstruction = new List<string>();
    [SerializeField] private TextMeshProUGUI pressToContinue;

    public void GameOver(string playerWhoLose) {
        //Debug.Log("Game Over!");
        bMovement.enabled = false;
        lJump.enabled = false;
        bArms.enabled = false;
        aMovement.enabled = false;
        oLegs.enabled = false;
        aJump.enabled = false;
        playerOne.GetComponent<CharacterController>().enabled = false;
        playerTwo.GetComponent<CharacterController>().enabled = false;
        playerOne.GetComponent<BoxCollider>().enabled = false;
        playerTwo.GetComponent<BoxCollider>().enabled = false;
        StartCoroutine(GameOverCo(playerWhoLose));
    }
    IEnumerator GameOverCo(string playerWhoLose) {
        //Debug.Log("GameOverCo");
        switch (playerWhoLose) {
            case "playerLegs":
                //Debug.Log("Legs Lose!");
                looser = playerOne.transform;
                StartCoroutine(PlayerAnimationGameOver());
                break;
            case "playerArms":
                Debug.Log("Arms Lose!");
                looser = playerTwo.transform;
                StartCoroutine(PlayerAnimationGameOver());
                break;
        }
        yield return null;
    }
    IEnumerator PlayerAnimationGameOver() {
        looser.DOMoveY(looser.position.y + 4.2f, 0.6f).SetEase(Ease.InOutCirc);
        yield return new WaitForSeconds(0.5f);
        looser.DOMoveY(looser.position.y - 8, 0.6f).SetEase(Ease.InOutCirc);
        yield return new WaitForSeconds(0.9f);
        gameOverImage.DOFade(1, 0.2f);
        gameOverMessageText.text = gameOverLegend[Random.Range(0, gameOverLegend.Count)];
        gameOverInstructionText.text = gameOverInstruction[Random.Range(0, gameOverInstruction.Count)];
        yield return new WaitForSeconds(0.2f);
        gameOverMessageText.DOFade(1, 0.2f);
        yield return new WaitForSeconds(0.2f);
        gameOverInstructionText.DOFade(1, 0.2f);
        yield return new WaitForSeconds(0.2f);
        pressToContinue.DOFade(1, 0.2f);
        restartReady = true;
    }

    bool restartReady;
    private void Update() {
        if (restartReady) {
            if (Input.GetButtonDown("Submit")) {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
            if (Input.GetButtonDown("Cancel")) {
                SceneManager.LoadScene(0);
            }
        }
    }

    
    

}
