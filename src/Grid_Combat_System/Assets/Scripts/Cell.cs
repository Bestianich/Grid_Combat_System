using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class Cell : MonoBehaviour
{
    [SerializeField] private CellType _cellType;
    [SerializeField] private Color _colorHover;

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
       HoverEffect();
    }


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

}

[Serializable]
public enum CellType
{
    Obstacle,
    Walkable,
    Spawner
}
