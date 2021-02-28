using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BoatController : MonoBehaviour
{
    public static BoatController bController;

    private void Awake() {
        if (!bController) {
            bController = this;
        } else {
            Destroy(this.gameObject);
        }
    }

    [Space][Header("Generals")]
    public float travellSpeed;

    [SerializeField] private ParticleSystem[] stella;

    [SerializeField] private ParticleSystem[] windParticles;

    public bool stellaChk;
    [SerializeField] private Camera camera_m;
    private bool camfov;
    [Space][Header("Sound Effects")]
    [SerializeField] private AudioSource paperFoldWind;
    [SerializeField] private AudioSource sailingStrong;
    [SerializeField] private AudioSource notSailing;

    [Space][Header("Navigation Vector")]
    public Vector3 navVector;

    [Space][Header("BowThruster")]
    [SerializeField] private Transform bowThruster;
    [SerializeField] private float thrusterForce;
    [SerializeField] private Rigidbody boatBody;
    public bool thrustBool;

    private void Update() {
        if(Input.GetButtonDown("sail")) {
            if (!stellaChk) {
                stellaChk = true;
                navVector = Vector3.right;
                StartCoroutine(Sail("north"));
                Debug.Log("stella");
            }

        }
        if(Input.GetAxis("Horizontal") != 0) {
            thrustBool = true;
        } else {
            thrustBool = false;
        }
        if (Input.GetButtonUp("sail")) {
            navVector = Vector3.zero;
            StartCoroutine(StopSail());
        }
        navVector = -transform.forward;
    }

    private void FixedUpdate() {
        if (thrustBool) {
            //boatBody.AddForceAtPosition(thrusterForce * -Vector3.forward * Input.GetAxis("Horizontal") * Time.deltaTime ,bowThruster.position);
            boatBody.AddTorque(Vector3.up * thrusterForce* Input.GetAxis("Horizontal") * Time.deltaTime);
        }
    }

    IEnumerator Sail(string direction) {
        windParticles[0].Play();
        camera_m.DOFieldOfView(76, 3).SetEase(Ease.InOutCubic);
        foreach(ParticleSystem p in stella) {
            p.Play();
        }
        WaveManager.wManager.speed = 2;
        paperFoldWind.Play();
        paperFoldWind.DOFade(1, 1);
        sailingStrong.Play();
        sailingStrong.DOFade(0.07f, 1.0f);
        notSailing.DOFade(0.25f, 1f);
        yield return null;
    }

    IEnumerator StopSail() {
        stellaChk = false;
        paperFoldWind.DOFade(0, 1);
        sailingStrong.DOFade(0, 1);
        notSailing.DOFade(0.5f, 0);
        foreach(ParticleSystem p in stella) {
            p.Stop();
        }
        foreach(ParticleSystem q in windParticles) {
            q.Stop();
        }
        WaveManager.wManager.speed = 1;
        camera_m.DOFieldOfView(68, 1);
        yield return null;
    }

}
