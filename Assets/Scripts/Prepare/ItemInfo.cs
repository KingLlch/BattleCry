using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemInfo : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerMoveHandler, IPointerClickHandler
{
    public Item ThisItem;
    public bool IsInUnit;

    public TextMeshProUGUI Value;
    public Image Image;

    private Camera _mainCamera;

    private void Awake()
    {
        _mainCamera = Camera.main;
    }

    public ItemInfo Copy()
    {
        ItemInfo copy = (ItemInfo)this.MemberwiseClone(); ;
        copy.ThisItem = ThisItem.Copy();
        return copy;
    }

    public void OnPointerEnter(PointerEventData pointer)
    {
        Description.Instance.ShowDescriptionItem(ThisItem, _mainCamera.ScreenToWorldPoint(new Vector3(pointer.position.x, pointer.position.y, 0)));
    }

    public void OnPointerExit(PointerEventData pointer)
    {
        Description.Instance.HideDescription();
    }

    public void OnPointerMove(PointerEventData pointer)
    {
        Description.Instance.ChangePositionDescription(pointer.position);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!IsInUnit)
            CreateUnit.Instance.AddItem(this);
        else
            CreateUnit.Instance.RemoveItem(this);
    }
}
