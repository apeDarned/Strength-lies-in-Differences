using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]

public class WaterManager : MonoBehaviour
{
    private MeshFilter meshFilter;
    public Vector3[] vertices;

    private void Awake() {
        meshFilter = GetComponent<MeshFilter>();
    }

    private void Update() {
        vertices = meshFilter.mesh.vertices;
        for(int i = 0; i < vertices.Length; i++) {
            vertices[i].y = WaveManager.wManager.GetWaveHeight(transform.position.x + vertices[i].x);

        }

        meshFilter.mesh.vertices = vertices;
        meshFilter.mesh.RecalculateNormals();

    }

}
