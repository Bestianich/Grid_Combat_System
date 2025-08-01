using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    [SerializeField] private static Entity _entitySelected;

    private void Update()
    {
        if (GameManager.Instance.GetGamePhase() == GamePhase.SpawnPhase) {
            SpawnEntity();
        }
    }

    private void SpawnEntity()
    {
        if (Input.GetMouseButtonUp(0)) {
            if (_entitySelected == null) {
                Debug.Log("No entity selected");
                return;
            }

            Vector3 mousePos = Input.mousePosition;
            Ray ray = Camera.main.ScreenPointToRay(mousePos);
            RaycastHit hit;
            Physics.Raycast(ray, out hit);
            var cell = hit.collider.GetComponent<Cell>();
            if (cell.GetCellType() == CellType.Spawner) {
                var entityPrefab = _entitySelected;
                Debug.Log(entityPrefab.transform.localScale);
                Vector3 playerOffset = new Vector3(0, 0, -entityPrefab.transform.localScale.z / 2);
                var entitySpawned = Instantiate(_entitySelected, cell.transform.position, Quaternion.identity,
                    cell.transform);
                entitySpawned.transform.localPosition += playerOffset;
                cell.SetEntityOnCell(entitySpawned);
            }
            _entitySelected = null;
        }

    }

    public static void SelectEntity(Entity entitySelected)
    {
        _entitySelected = entitySelected;
    }
}

