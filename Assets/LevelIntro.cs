using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class LevelIntro : MonoBehaviour {

    public static LevelIntro lIntro;

    private void Awake() {
        if (!lIntro) {
            lIntro = this;
        } else {
            Destroy(gameObject);
        }
    }

    private void Start() {
        Cursor.lockState = CursorLockMode.Locked;
        TurnOff();
    }

    [SerializeField] private Image blank;
    public void TurnOff() {
        StartCoroutine(TurnOffCo());
    }
    public void TurnOn() {
        StartCoroutine(TurnOnCo());
    }
    IEnumerator TurnOffCo() {
        blank.DOFade(0, 1);
        yield return null;
    }
    IEnumerator TurnOnCo() {
        blank.DOFade(1, 1);
        yield return new WaitForSeconds(1);
    }
}
