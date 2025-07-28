using System;
using System.Collections.Generic;
using UnityEngine;

public class GridGenerator : MonoBehaviour
{
    [SerializeField] private GameObject _plane;
    [SerializeField] private GameObject _cellPrefab;

    private List<Vector3> _points = null;

    private void Start()
    {
        _points = new List<Vector3>();
        _points = new List<Vector3>(_plane.GetComponent<MeshFilter>().mesh.vertices);
        foreach (Vector3 point in _points)
        {
            Instantiate(_cellPrefab, point + new Vector3(0f , 0.01f , 0f) , _cellPrefab.transform.rotation , this.transform);
        }
    }

    private void OnDrawGizmos()
    {
        if (_points == null)
            return;
        foreach (Vector3 point in _points)
        {
            Gizmos.DrawCube(point, Vector3.one);
        }
    }
}
