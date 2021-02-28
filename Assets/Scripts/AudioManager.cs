using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager audioManager;

    private void Awake() {
        if (!audioManager) {
            audioManager = this;
        } else {
            Destroy(this.gameObject);
        }
    }

    public List<AudioSource> enemyTalk = new List<AudioSource>();
    public List<AudioSource> enemyHurt = new List<AudioSource>();
    public List<AudioSource> enemyDestroy = new List<AudioSource>();
    public List<AudioSource> player = new List<AudioSource>();


    //I have to add a method for all other sounds and load them from here. This implies to correct the pther scripts...
    [Space][Header("Sound Objects")]
    [SerializeField] private GameObject pointSound;
    [SerializeField] private GameObject lightsSound;
    [SerializeField] private GameObject hopOn;
    public void PointSound() {
        Instantiate(pointSound, transform.position, Quaternion.identity);
    }
    public void PlayerLights() {
        Instantiate(lightsSound, transform.position, Quaternion.identity);
    }
    public void HopOn() {
        Instantiate(hopOn, transform.position, Quaternion.identity);
    }

}
