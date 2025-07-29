using System;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

[ExecuteInEditMode]
public class GridGenerator : MonoBehaviour
{

    [SerializeField] private int _sizeX = 100;
    [SerializeField] private int _sizeY = 50;
    [SerializeField] private float _tileSize = 2f;

    [SerializeField] private GameObject _cellPrefab;

    [SerializeField] private List<GameObject> _cells = new List<GameObject>();


    private List<Vector3> _points = null;

    private void Start()
    {
        _points = new List<Vector3>();
        // BuildMesh();
        // BuildGrid();
        // DestroyGrid();
    }

    public void BuildMesh()
    {
        int numTiles = _sizeX * _sizeY;
        int numTriangles = numTiles * 2;

        int vertX = _sizeX + 1;
        int vertY = _sizeY + 1;

        int numVertices = vertX * vertY;

        Vector3[] vertices = new Vector3[numVertices];
        int[] triangles = new int[numTriangles * 3];
        Vector3[] normals = new Vector3[numVertices];
        Vector2[] uv = new Vector2[numVertices];

        for (int z = 0; z < vertY; z++) {
            for (int x = 0; x < vertX; x++) {
                vertices[ z * vertX + x] = new Vector3(z * _tileSize, 0, x * _tileSize);
                normals[z * vertX + x]  = Vector3.up;
                uv[ z * vertX + x] = new Vector2((float)z / (float)_sizeX, (float)x / (float)_sizeY);
            }
        }

        for (int z = 0; z < _sizeY; z++) {
            for (int x = 0; x < _sizeX; x++) {
                int squareIndex = z * _sizeX + x;
                int triangleOffset = squareIndex * 6;
                triangles[triangleOffset + 0] = z * vertX + x + 0;
                triangles[triangleOffset + 1] = z * vertX + x + vertX +  1;
                triangles[triangleOffset + 2] = z * vertX + x + vertX +  0;
                triangles[triangleOffset + 3] = z * vertX + x +  0;
                triangles[triangleOffset + 4] = z * vertX + x +  1;
                triangles[triangleOffset + 5] = z * vertX + x + vertX + 1;
            }
        }

        Mesh mesh = new Mesh();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.normals = normals;
        mesh.uv = uv;

        MeshFilter mf = GetComponent<MeshFilter>();
        MeshRenderer mr = GetComponent<MeshRenderer>();
        MeshCollider mc = GetComponent<MeshCollider>();
        mf.mesh = mesh;

        Debug.Log(mf.sharedMesh.vertices.Length);

    }

    public void BuildGrid()
    {
        _points = new List<Vector3>();
        _points = new List<Vector3>(GetComponent<MeshFilter>().sharedMesh.vertices);
        foreach (Vector3 point in _points) {
            var cell = Instantiate(_cellPrefab, point + new Vector3(0f, 0.01f, 0f), _cellPrefab.transform.rotation,
                this.transform);
            cell.name = "Cell: " + (int)point.x + "x" + (int)point.z;
            _cells.Add(cell);
        }
    }

    public void DestroyGrid()
    {
        _points = null;
        int i = 0;
        foreach (GameObject cell in _cells){
            Debug.Log(i++);
            DestroyImmediate(cell);
        }
        _cells.Clear();
    }
    private void OnDrawGizmos()
    {
        if (_points == null)
            return;
        foreach (Vector3 point in _points)
        {
            Gizmos.DrawSphere(point, 0.2f);
        }
    }
}
