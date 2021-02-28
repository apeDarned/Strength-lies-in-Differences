using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManageMenuSong : MonoBehaviour
{
    [SerializeField] private AudioSource menuSong;
    private void Awake() {
        GameObject.Find("MainCamera").GetComponent<MenuSelection>().MenuSong(menuSong);
        GameObject.Find("MainCamera").GetComponent<MenuSelection>().songMenu = menuSong;
    }
}
