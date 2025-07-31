using UnityEngine;
using UnityEngine.EventSystems;

public class SpawnSlotUI : MonoBehaviour, IPointerDownHandler
{
   [SerializeField] private Entity _entitySelected;

   public void OnPointerDown(PointerEventData eventData)
   {
       PlayerInput.SelectEntity(_entitySelected);
   }

}
