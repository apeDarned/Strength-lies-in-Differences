using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    [SerializeField] private float health;
    [SerializeField] private float defense;
    
    private void Start() {
        StartCoroutine(Talk());
    }
    private void OnTriggerEnter(Collider other) {
        //Debug.Log("Collider Collision Entering");
        if (other.tag == "Legs") {
            GameObject.Find("Camera").GetComponent<FollowPlayer>().FocusPlayerOne();
            looserName = other.gameObject.name;
            ToGameOver(looserName);
        }
        if (other.tag == "Arms") {
            GameObject.Find("Camera").GetComponent<FollowPlayer>().FocusPlayerTwo();
            looserName = other.gameObject.name;
            ToGameOver(looserName);
        }
        if (other.tag == "bullet") {
            Debug.Log("BulletHit");
            if (other.GetComponent<Bullet>() != null) {
                TakeDamage(other.GetComponent<Bullet>().damage);
            }
            
        }
    }
    private string looserName;
    private void ToGameOver(string looserName) {
        Manager.manager.GameOver(looserName);
    }

    IEnumerator Talk() {
        var r = Random.Range(0, AudioManager.audioManager.enemyTalk.Count);
        AudioManager.audioManager.enemyTalk[r].pitch = Random.Range(0.87f, 1.21f);
        AudioManager.audioManager.enemyTalk[r].Play();
        yield return new WaitForSeconds(Random.Range(7, 10));
        StartCoroutine(Talk());
    }

    void TakeDamage(float ammount) {
        if (health > 0) {
            health -= ammount - defense;
            var r = Random.Range(0, AudioManager.audioManager.enemyHurt.Count);
            AudioManager.audioManager.enemyHurt[r].pitch = Random.Range(0.87f, 1.21f);
            AudioManager.audioManager.enemyHurt[r].Play();
        } else {
            Debug.Log("1");
            var r = Random.Range(0, AudioManager.audioManager.enemyDestroy.Count);
            AudioManager.audioManager.enemyDestroy[r].pitch = Random.Range(0.87f, 1.21f);
            AudioManager.audioManager.enemyDestroy[r].Play();
            Debug.Log("2");
            var s = Random.Range(0, ParticleManager.particleManager.enemyDestroyParticles.Count);
            var tempParts = Instantiate(ParticleManager.particleManager.enemyDestroyParticles[s], transform.position, Quaternion.identity);
            tempParts.Play();
            Destroy(gameObject);
        }
    }

}
