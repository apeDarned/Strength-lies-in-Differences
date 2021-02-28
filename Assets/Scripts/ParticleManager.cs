using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleManager : MonoBehaviour
{
    public static ParticleManager particleManager;

    private void Awake() {
        if (!particleManager) {
            particleManager = this;
        } else {
            Destroy(this.gameObject);
        }
    }

    public List<ParticleSystem> enemyDestroyParticles = new List<ParticleSystem>();

}
