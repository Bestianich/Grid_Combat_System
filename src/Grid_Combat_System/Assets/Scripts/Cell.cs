using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class Cell : MonoBehaviour
{
    [SerializeField] private CellType _cellType;
    [SerializeField] private Color _colorHover;
    [SerializeField] private Color _colorSpawn;
    [SerializeField] private Entity _entityOnCell;

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
        if (GameManager.Instance.GetGamePhase() == GamePhase.SpawnPhase && _cellType == CellType.Spawner)
            _meshRenderer.material.color = _colorSpawn;
        HoverEffect();
    }

    public CellType GetCellType()
    {
        return _cellType;
    }

    public void SetEntityOnCell(Entity entityOnCell)
    {
        _entityOnCell = entityOnCell;
    }

    public Entity GetEntityOnCell()
    {
        return _entityOnCell;
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
            if (_cellType != CellType.Spawner) {
                _meshRenderer.material.color = Color.Lerp(_meshRenderer.material.color, _startColor,
                    Mathf.InverseLerp(0f, 1f, Time.time));
            }
            else {
                _meshRenderer.material.color = Color.Lerp(_meshRenderer.material.color, _colorSpawn , Mathf.InverseLerp(0f, 1f, Time.time));
            }
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
