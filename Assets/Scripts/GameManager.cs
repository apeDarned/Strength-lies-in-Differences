using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager gManager;

    private void Awake() {
        if (!gManager) {
            gManager = this;
            //DontDestroyOnLoad(gManager.gameObject);
        } else {
            Destroy(this.gameObject);
        }

    }

    [Space][Header("Point system")]
    [SerializeField] private TextMeshProUGUI pointsValue;
    [SerializeField] private TextMeshProUGUI enemyPointsValue;
    public int points;
    public int enemyPoints;

    public void Points(int p) {
        points += p;
        pointsValue.text = points.ToString();
    }
    public void EnemyPoints(int p) {
        enemyPoints += p;
        enemyPointsValue.text = enemyPoints.ToString();
    }

    [System.Serializable]
    public class EnemyAI {
        public Vector3 legsPosition;
        public Vector3 armsPosition;
        public int playerPoints;
        
    }

    public EnemyAI AIInstance;

}
