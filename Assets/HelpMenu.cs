using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class HelpMenu : MonoBehaviour
{
    [SerializeField] private Image keyboardInput;
    [SerializeField] private TextMeshProUGUI instruction;
    public bool chk;

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            StartCoroutine(OpenHelp());
        }
    }
    IEnumerator OpenHelp() {
        keyboardInput.DOFade(1, 0.3f);
        yield return new WaitForSeconds(0.3f);
    }

}
