using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class WeaponFire : MonoBehaviour
{
    [SerializeField] private float cadencyOfFire;
    [SerializeField] private float ammo;
    [SerializeField] private float ammoReduction;
    [SerializeField] private ParticleSystem fire;
    [SerializeField] private ParticleSystem explode;
    [SerializeField] private AudioSource firing;
    [SerializeField] private AudioSource exploding;
    [SerializeField] private GameObject bulletType;
    [SerializeField] private Transform instantiateBullet;
    [SerializeField] private MeshRenderer[] allWeaponParts;
    bool active;

    public void BeginFire() {
        active = true;
        StartCoroutine(FireCo());
    }

    public void StopFire() {
        active = false;
    }

    IEnumerator FireCo() {
        if (ammo > 0) {
            var r = Random.Range(0.85f, 1.21f);
            //firing.pitch = r;
            //firing.Play();
            //fire.Play();
            ammo -= ammoReduction;
            var tempBullet = Instantiate(bulletType, instantiateBullet.position, Quaternion.LookRotation(instantiateBullet.forward, instantiateBullet.up));
            yield return new WaitForSeconds(cadencyOfFire);
            if (active) {
                StartCoroutine(FireCo());
            }
        } else {
            foreach (MeshRenderer m in allWeaponParts) {
                m.enabled = false;
            }
            //explode.Play();
            //exploding.Play();
            yield return new WaitForSeconds(2);
            active = false; //this line is not necessary, I have to analyze and remove...
            ArmsGrab.aGrabClass.ReleaseWeapon();
            yield return new WaitForEndOfFrame();//From here this will execute ONLY if above line is done in ArmsGrab class, but anyway I want to make sure that the method is done before the line below executes...
            gameObject.SetActive(false);

        }
        

    }

}
