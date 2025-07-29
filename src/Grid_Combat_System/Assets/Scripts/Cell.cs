using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class Cell : MonoBehaviour
{
    [SerializeField] private CellType _cellType;
    [SerializeField] private Color _colorHover;
    [SerializeField] private Color _colorSpawn;

    private Color _startColor;
    private MeshRenderer _meshRenderer;

    private bool _mouseOver = false;

    private void Start()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
        _startColor = _meshRenderer.material.color;

    }

    private void Update()
    {
        if (GridManager.Instance.GetGamePhase() == GamePhase.SpawnPhase)
        {
            if (_cellType == CellType.Spawner) {
                _meshRenderer.material.color = _colorSpawn;
                SpawnPlayer();
            }
        }
        else {
            HoverEffect();
        }
    }

    public CellType GetCellType()
    {
        return _cellType;
    }

    private void SpawnPlayer()
    {
        if (Input.GetMouseButtonUp(0)) {
            Vector3 mousePos = Input.mousePosition;
            Ray ray = Camera.main.ScreenPointToRay(mousePos);
            RaycastHit hit;
            Physics.Raycast(ray, out hit);
            if (hit.transform.gameObject == this.gameObject) {
                Instantiate(GridManager.Instance.GetPlayerPrefab(), transform.position, Quaternion.identity);
            }
        }
    }

    #region MouseHoverEffect


    private void HoverEffect()
    {
        if (_mouseOver)
        {
            _meshRenderer.material.color = Color.Lerp(_meshRenderer.material.color, _colorHover, Mathf.InverseLerp(0f, 1f, Time.time));
        }
        else
        {
            _meshRenderer.material.color = Color.Lerp(_meshRenderer.material.color, _startColor, Mathf.InverseLerp(0f, 1f, Time.time));
        }
    }
    private void OnMouseOver()
    {

        _mouseOver = true;
        // _meshRenderer.material.color = Color.Lerp(_startColor , _colorHover, 0.2f);

    }

    private void OnMouseExit()
    {
        _mouseOver = false;
    }
    #endregion

}

[Serializable]
public enum CellType
{
    Walkable,
    Spawner,
    EnemySpawner,
    Obstacle
}
