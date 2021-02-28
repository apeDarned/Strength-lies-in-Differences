using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuAudioManager : MonoBehaviour
{
    [SerializeField] private GameObject whoosh;
    [SerializeField] private GameObject switchLight;
    [SerializeField] private GameObject menuSong;
    [SerializeField] private GameObject menuSelectionInput;
    [SerializeField] private GameObject whipeOut;
    [SerializeField] private GameObject startGame;
    public static MenuAudioManager menuAudioManager;

    private void Awake() {
        if (!menuAudioManager) {
            menuAudioManager = this;
        } else {
            Destroy(this.gameObject);
        }
    }
    public void Whoosh() {
        Instantiate(whoosh, transform.position, Quaternion.identity);
    }
    public void LightSwitch() {
        Instantiate(switchLight, transform.position, Quaternion.identity);
    }
    public void MenuSong() {
        Instantiate(menuSong, transform.position, Quaternion.identity);
    }
    public void MenuSelectionInput() {
        Instantiate(menuSelectionInput, transform.position, Quaternion.identity);
    }
    public void WhipeOut() {
        Instantiate(whipeOut, transform.position, Quaternion.identity);
    }
    public void StartGame() {
        Instantiate(startGame, transform.position, Quaternion.identity);
    }

}
