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

    [SerializeField] private Cell _cellPrefab;

    [SerializeField] private Cell[,] _cells;


    [SerializeField] private Color _walkableCellGizmoColor = Color.green;
    [SerializeField] private Color _obstacleCellGizmoColor = Color.grey;
    [SerializeField] private Color _spawnCellGizmoColor = Color.blue;
    [SerializeField] private Color _enemySpawnCellGizmoColor = Color.red;

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

        _cells = new Cell[_sizeX, _sizeY];
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

    }

    public void BuildGrid()
    {
        _cells = new Cell[_sizeX, _sizeY];
        _points = new List<Vector3>();
        _points = new List<Vector3>(GetComponent<MeshFilter>().sharedMesh.vertices);
        foreach (Vector3 point in _points) {
            var cell = Instantiate(_cellPrefab, point + new Vector3(0f, 0.01f, 0f), _cellPrefab.transform.rotation,
                this.transform);
            cell.name = "Cell: " + (int)point.x + "x" + (int)point.z;
            cell.SetCellCords((int)point.x, (int)point.z);
            _cells[(int)point.x, (int)point.z] = cell;
        }
    }

    public void DestroyGrid()
    {
        _points = null;
        foreach (Cell cell in _cells){
            DestroyImmediate(cell.gameObject);
        }
    }

    public Cell[,] GetCells()
    {
        return _cells;
    }
    private void OnDrawGizmos()
    {
        // if (_points == null)
        //     return;
        // foreach (Vector3 point in _points)
        // {
        //     Gizmos.DrawSphere(point, 0.2f);
        // }

        foreach (Cell cellObj in _cells) {
            if (cellObj != null) {
                var cell = cellObj.GetComponent<Cell>();
                if (cell.GetCellType() == CellType.Spawner) {
                    Gizmos.color = _spawnCellGizmoColor;
                    Gizmos.DrawSphere(cell.transform.position, 0.2f);
                }
                else if (cell.GetCellType() == CellType.EnemySpawner) {
                    Gizmos.color = _enemySpawnCellGizmoColor;
                    Gizmos.DrawSphere(cell.transform.position, 0.2f);
                }
                else if (cell.GetCellType() == CellType.Obstacle) {
                    Gizmos.color = _obstacleCellGizmoColor;
                    Gizmos.DrawSphere(cell.transform.position, 0.2f);
                }
                else if (cell.GetCellType() == CellType.Walkable) {
                    Gizmos.color = _walkableCellGizmoColor;
                    Gizmos.DrawSphere(cell.transform.position, 0.2f);
                }
            }
        }
    }
}
