using System.Diagnostics;
using UnityEngine;

public class ItemsSpawner : MonoBehaviour
{
    public GameObject ItemPrefab;

    private void Start()
    {
        foreach (Item race in ItemsList.AllRace)
        {
            ItemInfo newItem = Instantiate(ItemPrefab, Vector2.zero, Quaternion.identity, CreateUnit.Instance.RacesGrid).GetComponent<ItemInfo>();
            newItem.ThisItem = race.Copy();
            newItem.ThisItem.Value = race.Value;
            newItem.Value.text = "x" + newItem.ThisItem.Value.ItemValue.ToString();
            newItem.Image.sprite = newItem.ThisItem.Base.Sprite;
            newItem.GetComponent<RectTransform>().localPosition = Vector3.zero;
        }

        foreach (Item item in ItemsList.AllEquipmentItems)
        {
            ItemInfo newItem = Instantiate(ItemPrefab, Vector2.zero, Quaternion.identity, CreateUnit.Instance.ItemsGrid).GetComponent<ItemInfo>();
            newItem.ThisItem = item.Copy();
            newItem.ThisItem.Value = item.Value;
            newItem.Value.text = "x" + newItem.ThisItem.Value.ItemValue.ToString();
            newItem.Image.sprite = newItem.ThisItem.Base.Sprite;
            newItem.GetComponent<RectTransform>().localPosition = Vector3.zero;
        }
    }
}
