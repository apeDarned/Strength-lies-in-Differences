using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    public static WaveManager wManager;

    private void Awake() {
        if (!wManager) {
            wManager = this;
        } else {
            Destroy(this.gameObject);
        }
    }

    public float amplitude = 1f;
    public float lenght = 2f;
    public float speed = 1f;
    public float offset = 0;

    private void Update() {
        offset += Time.deltaTime * speed;
    }

    public float GetWaveHeight(float _x) {
        return amplitude * Mathf.Sin(_x / lenght + offset);
    }

}
