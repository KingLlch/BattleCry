using System;
using System.Reflection;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CreateUnit : MonoBehaviour
{

    private static CreateUnit _instance;

    public static CreateUnit Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<CreateUnit>();
            }

            return _instance;
        }
    }

    public GameObject createUnitGameObject;

    public ItemInfo Race;
    public ItemInfo Weapon;
    public ItemInfo Armor;
    public ItemInfo Shield;
    public ItemInfo Special;

    private ItemInfo RaceLink;
    private ItemInfo WeaponLink;
    private ItemInfo ArmorLink;
    private ItemInfo ShieldLink;
    private ItemInfo SpecialLink;

    public Unit createUnit;
    public Unit loadUnit;

    public TextMeshProUGUI Value;

    public TextMeshProUGUI Name;
    public TextMeshProUGUI Points;

    public TextMeshProUGUI Health;

    public TextMeshProUGUI AttackRange;
    public TextMeshProUGUI AttackInterval;

    public GameObject[] DamagesGameObjects;
    public TextMeshProUGUI[] Damages;

    public GameObject[] ResistsGameObjects;
    public TextMeshProUGUI[] Resists;

    public Transform RacesGrid;
    public Transform ItemsGrid;

    public GameObject UnitPrefab;

    public bool IsEditUnit;

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }

        ClearUI();
    }

    public void ChangeName(string name)
    {
        Name.text = name;
        createUnit.unitCharacteristics.Name = name;
    }

    public void AddItem(ItemInfo itemInfo)
    {
        createUnit.unitCharacteristics.Value = 1;
        ChangeItemValueUI();

        if (itemInfo.ThisItem.Base.Type == ItemType.Race && itemInfo.ThisItem.Value.ItemValue > 0 && (Race.ThisItem == null || Race.ThisItem.Base.Name != itemInfo.ThisItem.Base.Name))
        {
            if (Race.ThisItem != null)
            {
                ChangeStats(Race.ThisItem, false);
            }

            Race.ThisItem = itemInfo.ThisItem.Copy();
            RaceLink = itemInfo;
            Race.Image.sprite = itemInfo.ThisItem.Base.Sprite;
            ChangeStats(itemInfo.ThisItem, true);
            ChangeUI(createUnit);
        }

        else if (itemInfo.ThisItem.Base.Type == ItemType.Weapon && itemInfo.ThisItem.Value.ItemValue > 0 && (Weapon.ThisItem == null || Weapon.ThisItem.Base.Name != itemInfo.ThisItem.Base.Name))
        {
            if (Weapon.ThisItem != null)
            {
                ChangeStats(Weapon.ThisItem, false);
            }

            Weapon.ThisItem = itemInfo.ThisItem.Copy();
            WeaponLink = itemInfo;
            Weapon.Image.sprite = itemInfo.ThisItem.Base.Sprite;
            ChangeStats(itemInfo.ThisItem, true);
            ChangeUI(createUnit);
        }

        else if (itemInfo.ThisItem.Base.Type == ItemType.Armor && itemInfo.ThisItem.Value.ItemValue > 0 && (Armor.ThisItem == null || Armor.ThisItem.Base.Name != itemInfo.ThisItem.Base.Name))
        {
            if (Armor.ThisItem != null)
            {
                ChangeStats(Armor.ThisItem, false);
            }

            Armor.ThisItem = itemInfo.ThisItem.Copy();
            ArmorLink = itemInfo;
            Armor.Image.sprite = itemInfo.ThisItem.Base.Sprite;
            ChangeStats(itemInfo.ThisItem, true);
            ChangeUI(createUnit);
        }

        else if (itemInfo.ThisItem.Base.Type == ItemType.Shield && itemInfo.ThisItem.Value.ItemValue > 0 && (Shield.ThisItem == null || Shield.ThisItem.Base.Name != itemInfo.ThisItem.Base.Name))
        {
            if (Shield.ThisItem != null)
            {
                ChangeStats(Shield.ThisItem, false);
            }

            Shield.ThisItem = itemInfo.ThisItem.Copy();
            ShieldLink = itemInfo;
            Shield.Image.sprite = itemInfo.ThisItem.Base.Sprite;
            ChangeStats(itemInfo.ThisItem, true);
            ChangeUI(createUnit);
        }

        else if (itemInfo.ThisItem.Base.Type == ItemType.Special && itemInfo.ThisItem.Value.ItemValue > 0 && (Special.ThisItem == null || Special.ThisItem.Base.Name != itemInfo.ThisItem.Base.Name))
        {
            if (Special.ThisItem != null)
            {
                ChangeStats(Special.ThisItem, false);
            }

            Special.ThisItem = itemInfo.ThisItem.Copy();
            SpecialLink = itemInfo;
            Special.Image.sprite = itemInfo.ThisItem.Base.Sprite;
            ChangeStats(itemInfo.ThisItem, true);
            ChangeUI(createUnit);
        }
    }

    private void ChangeItemValueUI()
    {
        Race.Value.text = createUnit.unitCharacteristics.Value.ToString();
        Weapon.Value.text = createUnit.unitCharacteristics.Value.ToString();
        Armor.Value.text = createUnit.unitCharacteristics.Value.ToString();
        Shield.Value.text = createUnit.unitCharacteristics.Value.ToString();
        Special.Value.text = createUnit.unitCharacteristics.Value.ToString();
    }

    public void RemoveItem(ItemInfo itemInfo)
    {
        if (Race.ThisItem != null && itemInfo.ThisItem.Base.Type == ItemType.Race)
        {
            ChangeStats(Race.ThisItem, false);
            Race.ThisItem = null;
            Race.Image.sprite = null;
        }

        if (Weapon.ThisItem != null && itemInfo.ThisItem.Base.Type == ItemType.Weapon)
        {
            ChangeStats(Weapon.ThisItem, false);
            Weapon.ThisItem = null;
            Weapon.Image.sprite = null;
        }

        if (Armor.ThisItem != null && itemInfo.ThisItem.Base.Type == ItemType.Armor)
        {
            ChangeStats(Armor.ThisItem, false);
            Armor.ThisItem = null;
            Armor.Image.sprite = null;
        }

        if (Shield.ThisItem != null && itemInfo.ThisItem.Base.Type == ItemType.Shield)
        {
            ChangeStats(Shield.ThisItem, false);
            Shield.ThisItem = null;
            Shield.Image.sprite = null;
        }
        if (Special.ThisItem != null && itemInfo.ThisItem.Base.Type == ItemType.Special)
        {
            ChangeStats(Special.ThisItem, false);
            Special.ThisItem = null;
            Special.Image.sprite = null;
        }

        ChangeUI(createUnit);
    }

    public void ChangeStats(Item item, bool isAdd)
    {
        if (isAdd)
        {
            createUnit.unitCharacteristics.Points += item.Base.Points;
            createUnit.unitCharacteristics.Health += item.Base.Health;
            createUnit.unitCharacteristics.MaxHealth += item.Base.Health;

            Damages damages = createUnit.unitCharacteristics.Damages;
            Type damageType = damages.GetType();
            FieldInfo[] damageFields = damageType.GetFields();

            Damages damagesItem = item.Damages;
            Type damageTypeItem = damagesItem.GetType();
            FieldInfo[] damageFieldsItem = damageTypeItem.GetFields();

            for (int i = 0; i < Damages.Length; i++)
            {
                int currentDamageValue = (int)damageFields[i].GetValue(damages);
                int itemDamageValue = (int)damageFieldsItem[i].GetValue(damagesItem);

                int resultValue = currentDamageValue + itemDamageValue;

                damageFields[i].SetValueDirect(__makeref(damages), resultValue);
            }

            Resists resists = createUnit.unitCharacteristics.Resists;
            Type resistType = resists.GetType();
            FieldInfo[] resistFields = resistType.GetFields();

            Resists resistsItem = item.Resists;
            Type resistTypeItem = resistsItem.GetType();
            FieldInfo[] resistFieldsItem = resistTypeItem.GetFields();

            for (int i = 0; i < resistFields.Length; i++)
            {
                int currentResistValue = (int)resistFields[i].GetValue(resists);
                int itemResistValue = (int)resistFieldsItem[i].GetValue(resistsItem);

                int resultValue = currentResistValue + itemResistValue;

                resistFields[i].SetValueDirect(__makeref(resists), resultValue);
            }

            createUnit.unitCharacteristics.AttackTime = item.Weapon.AttackTime;
            createUnit.unitCharacteristics.AttackRange = item.Weapon.AttackRange;
        }

        else
        {
            createUnit.unitCharacteristics.Points -= item.Base.Points;
            createUnit.unitCharacteristics.Health -= item.Base.Health;
            createUnit.unitCharacteristics.MaxHealth -= item.Base.Health;

            Damages damages = createUnit.unitCharacteristics.Damages;
            Type damageType = damages.GetType();
            FieldInfo[] damageFields = damageType.GetFields();

            Damages damagesItem = item.Damages;
            Type damageTypeItem = damagesItem.GetType();
            FieldInfo[] damageFieldsItem = damageTypeItem.GetFields();

            for (int i = 0; i < Damages.Length; i++)
            {
                int currentDamageValue = (int)damageFields[i].GetValue(damages);
                int itemDamageValue = (int)damageFieldsItem[i].GetValue(damagesItem);

                int resultValue = currentDamageValue - itemDamageValue;

                damageFields[i].SetValueDirect(__makeref(damages), resultValue);
            }

            Resists resists = createUnit.unitCharacteristics.Resists;
            Type resistType = resists.GetType();
            FieldInfo[] resistFields = resistType.GetFields();

            Resists resistsItem = item.Resists;
            Type resistTypeItem = resistsItem.GetType();
            FieldInfo[] resistFieldsItem = resistTypeItem.GetFields();

            for (int i = 0; i < resistFields.Length; i++)
            {
                int currentResistValue = (int)resistFields[i].GetValue(resists);
                int itemResistValue = (int)resistFieldsItem[i].GetValue(resistsItem);

                int resultValue = currentResistValue - itemResistValue;

                resistFields[i].SetValueDirect(__makeref(resists), resultValue);
            }

            createUnit.unitCharacteristics.AttackTime = item.Weapon.AttackTime;
            createUnit.unitCharacteristics.AttackRange = item.Weapon.AttackRange;
        }
    }

    public void ChangeUI(Unit unit)
    {
        Value.text = unit.unitCharacteristics.Value.ToString();
        Points.text = unit.unitCharacteristics.Points.ToString();
        Health.text = unit.unitCharacteristics.Health + " / " + unit.unitCharacteristics.MaxHealth;

        Damages damages = unit.unitCharacteristics.Damages;
        Type damageType = damages.GetType();
        FieldInfo[] damagefields = damageType.GetFields();

        for (int i = 0; i < Damages.Length; i++)
        {
            if ((int)damagefields[i].GetValue(damages) != 0)
            {
                DamagesGameObjects[i].SetActive(true);
                Damages[i].gameObject.SetActive(true);
                Damages[i].text = ((int)damagefields[i].GetValue(damages)).ToString();
            }
            else
            {
                Damages[i].gameObject.SetActive(false);
                DamagesGameObjects[i].SetActive(false);
            }
        }

        Resists resists = unit.unitCharacteristics.Resists;
        Type resistType = resists.GetType();
        FieldInfo[] resistfields = resistType.GetFields();

        for (int i = 0; i < Damages.Length; i++)
        {
            if ((int)resistfields[i].GetValue(resists) != 0)
            {
                ResistsGameObjects[i].SetActive(true);
                Resists[i].gameObject.SetActive(true);
                Resists[i].text = ((int)resistfields[i].GetValue(resists)).ToString();
            }
            else
            {
                Resists[i].gameObject.SetActive(false);
                ResistsGameObjects[i].SetActive(false);
            }
        }

        AttackInterval.text = unit.unitCharacteristics.AttackTime.ToString();
        AttackRange.text = unit.unitCharacteristics.AttackRange.ToString();
    }

    public void AllItems()
    {
        foreach (Transform item in ItemsGrid.transform)
        {
            item.gameObject.SetActive(true);
        }
    }

    public void WeaponItems()
    {
        foreach (Transform item in ItemsGrid.transform)
        {
            if (item.GetComponent<ItemInfo>().ThisItem.Base.Type == ItemType.Weapon)
                item.gameObject.SetActive(true);
            else
                item.gameObject.SetActive(false);
        }
    }

    public void ArmorItems()
    {
        foreach (Transform item in ItemsGrid.transform)
        {
            if (item.GetComponent<ItemInfo>().ThisItem.Base.Type == ItemType.Armor)
                item.gameObject.SetActive(true);
            else
                item.gameObject.SetActive(false);
        }
    }

    public void ShieldItems()
    {
        foreach (Transform item in ItemsGrid.transform)
        {
            if (item.GetComponent<ItemInfo>().ThisItem.Base.Type == ItemType.Shield)
                item.gameObject.SetActive(true);
            else
                item.gameObject.SetActive(false);
        }
    }

    public void SpecialItems()
    {
        foreach (Transform item in ItemsGrid.transform)
        {
            if (item.GetComponent<ItemInfo>().ThisItem.Base.Type == ItemType.Special)
                item.gameObject.SetActive(true);
            else
                item.gameObject.SetActive(false);
        }
    }

    public void AddValue()
    {
        if ((Race.ThisItem == null || Race.ThisItem.Value.ItemValue > createUnit.unitCharacteristics.Value) &&
            (Weapon.ThisItem == null || Weapon.ThisItem.Value.ItemValue > createUnit.unitCharacteristics.Value) &&
            (Armor.ThisItem == null || Armor.ThisItem.Value.ItemValue > createUnit.unitCharacteristics.Value) &&
            (Shield.ThisItem == null || Shield.ThisItem.Value.ItemValue > createUnit.unitCharacteristics.Value) &&
            (Special.ThisItem == null || Special.ThisItem.Value.ItemValue > createUnit.unitCharacteristics.Value))
        {
            createUnit.unitCharacteristics.Value++;
            Value.text = createUnit.unitCharacteristics.Value.ToString();
            ChangeItemValueUI();
        }
    }

    public void RemoveValue()
    {
        if (createUnit.unitCharacteristics.Value > 1)
        {
            createUnit.unitCharacteristics.Value--;
            Value.text = createUnit.unitCharacteristics.Value.ToString();
            ChangeItemValueUI();
        }
    }

    public void CreateNewUnit()
    {
        if (Race.ThisItem != null)
        {
            createUnit.unitCharacteristics.Race = Race.ThisItem.Copy();
            createUnit.unitCharacteristics.RaceName = Race.ThisItem.Base.Name;
            RaceLink.ThisItem.Value.ItemValue -= createUnit.unitCharacteristics.Value;
            RaceLink.Value.text = RaceLink.ThisItem.Value.ItemValue.ToString();
        }
        else
            return;

        if (Weapon.ThisItem != null)
        {
            createUnit.unitCharacteristics.Weapon = Weapon.ThisItem.Copy();
            createUnit.unitCharacteristics.WeaponName = Weapon.ThisItem.Base.Name;
            WeaponLink.ThisItem.Value.ItemValue -= createUnit.unitCharacteristics.Value;
            WeaponLink.Value.text = WeaponLink.ThisItem.Value.ItemValue.ToString();
        }
        if (Armor.ThisItem != null)
        {
            createUnit.unitCharacteristics.Armor = Armor.ThisItem.Copy();
            createUnit.unitCharacteristics.ArmorName = Armor.ThisItem.Base.Name;
            ArmorLink.ThisItem.Value.ItemValue -= createUnit.unitCharacteristics.Value;
            ArmorLink.Value.text = ArmorLink.ThisItem.Value.ItemValue.ToString();
        }
        if (Shield.ThisItem != null)
        {
            createUnit.unitCharacteristics.Shield = Shield.ThisItem.Copy();
            createUnit.unitCharacteristics.ShieldName = Shield.ThisItem.Base.Name;
            ShieldLink.ThisItem.Value.ItemValue -= createUnit.unitCharacteristics.Value;
            ShieldLink.Value.text = ShieldLink.ThisItem.Value.ItemValue.ToString();
        }
        if (Special.ThisItem != null)
        {
            createUnit.unitCharacteristics.Special = Special.ThisItem.Copy();
            createUnit.unitCharacteristics.SpecialName = Special.ThisItem.Base.Name;
            SpecialLink.ThisItem.Value.ItemValue -= createUnit.unitCharacteristics.Value;
            SpecialLink.Value.text = SpecialLink.ThisItem.Value.ItemValue.ToString();
        }

        if (!IsEditUnit)
        {
            Unit newUnit = Instantiate(UnitPrefab, Vector2.zero, Quaternion.identity, PrepareUIManager.Instance.UnitParent).GetComponent<Unit>();
            newUnit.unitCharacteristics = createUnit.unitCharacteristics.Copy();
            newUnit.UnitImage.sprite = createUnit.UnitImage.sprite;
            newUnit.Value.text = "x" + createUnit.unitCharacteristics.Value;

            if (Race.ThisItem != null)
                newUnit.unitCharacteristics.RaceLink = RaceLink;
            if (Weapon.ThisItem != null)
                newUnit.unitCharacteristics.WeaponLink = WeaponLink;
            if (Armor.ThisItem != null)
                newUnit.unitCharacteristics.ArmorLink = ArmorLink;
            if (Shield.ThisItem != null)
                newUnit.unitCharacteristics.ShieldLink = ShieldLink;
            if (Special.ThisItem != null)
                newUnit.unitCharacteristics.SpecialLink = SpecialLink;

            newUnit.GetComponent<RectTransform>().localPosition = Vector3.zero;
        }

        else
        {
            PrepareManager.Instance.ChosenUnit.unitCharacteristics = createUnit.unitCharacteristics.Copy();
            PrepareManager.Instance.ChosenUnit.UnitImage.sprite = createUnit.UnitImage.sprite;
            PrepareManager.Instance.ChosenUnit.Value.text = "x" + createUnit.unitCharacteristics.Value;

            if (Race.ThisItem != null)
                PrepareManager.Instance.ChosenUnit.unitCharacteristics.RaceLink = RaceLink;
            if (Weapon.ThisItem != null)
                PrepareManager.Instance.ChosenUnit.unitCharacteristics.WeaponLink = WeaponLink;
            if (Armor.ThisItem != null)
                PrepareManager.Instance.ChosenUnit.unitCharacteristics.ArmorLink = ArmorLink;
            if (Shield.ThisItem != null)
                PrepareManager.Instance.ChosenUnit.unitCharacteristics.ShieldLink = ShieldLink;
            if (Special.ThisItem != null)
                PrepareManager.Instance.ChosenUnit.unitCharacteristics.SpecialLink = SpecialLink;
        }

        CloseCreateUnitPanel(false);
    }

    public void CloseCreateUnitPanel(bool isButton = true)
    {
        if (IsEditUnit && isButton)
        {
            if (RaceLink != null)
            {
                RaceLink.ThisItem.Value.ItemValue -= loadUnit.unitCharacteristics.Value;
                RaceLink.Value.text = RaceLink.ThisItem.Value.ItemValue.ToString();
            }
            if (WeaponLink != null)
            {
                WeaponLink.ThisItem.Value.ItemValue -= loadUnit.unitCharacteristics.Value;
                WeaponLink.Value.text = WeaponLink.ThisItem.Value.ItemValue.ToString();
            }
            if (ArmorLink != null)
            {
                ArmorLink.ThisItem.Value.ItemValue -= loadUnit.unitCharacteristics.Value;
                ArmorLink.Value.text = ArmorLink.ThisItem.Value.ItemValue.ToString();
            }
            if (ShieldLink != null)
            {
                ShieldLink.ThisItem.Value.ItemValue -= loadUnit.unitCharacteristics.Value;
                ShieldLink.Value.text = ShieldLink.ThisItem.Value.ItemValue.ToString();
            }
            if (SpecialLink != null)
            {
                SpecialLink.ThisItem.Value.ItemValue -= loadUnit.unitCharacteristics.Value;
                SpecialLink.Value.text = SpecialLink.ThisItem.Value.ItemValue.ToString();
            }
        }

        ClearCreateUnit();
        createUnitGameObject.SetActive(false);
    }

    public void ClearCreateUnit()
    {
        createUnit.unitCharacteristics = new UnitCharacteristics();
        Race.ThisItem = Weapon.ThisItem = Armor.ThisItem = Shield.ThisItem = Special.ThisItem = null;
        Race.Image.sprite = Weapon.Image.sprite = Armor.Image.sprite = Shield.Image.sprite = Special.Image.sprite = null;
        RaceLink = WeaponLink = ArmorLink = ShieldLink = SpecialLink = null;
        ClearUI();
    }

    private void ClearUI()
    {
        Value.text = "1";
        Health.text = "0/0";
        Points.text = "0";
        AttackInterval.text = "0.0s";
        AttackRange.text = null;

        for (int i = 0; i < Damages.Length; i++)
        {
            Damages[i].gameObject.SetActive(false);
            DamagesGameObjects[i].SetActive(false);
            Resists[i].gameObject.SetActive(false);
            ResistsGameObjects[i].SetActive(false);
        }
    }

    public void EditUnit(Unit unit)
    {
        loadUnit = unit;
        createUnit.unitCharacteristics = loadUnit.unitCharacteristics.Copy();
        ChangeUI(loadUnit);

        Race.ThisItem = loadUnit.unitCharacteristics.Race;
        Weapon.ThisItem = loadUnit.unitCharacteristics.Weapon;
        Armor.ThisItem = loadUnit.unitCharacteristics.Armor;
        Shield.ThisItem = loadUnit.unitCharacteristics.Shield;
        Special.ThisItem = loadUnit.unitCharacteristics.Special;

        if (loadUnit.unitCharacteristics.Race != null)
            Race.Image.sprite = loadUnit.unitCharacteristics.Race.Base.Sprite;
        if (loadUnit.unitCharacteristics.Weapon != null)
            Weapon.Image.sprite = loadUnit.unitCharacteristics.Weapon.Base.Sprite;
        if (loadUnit.unitCharacteristics.Armor != null)
            Armor.Image.sprite = loadUnit.unitCharacteristics.Armor.Base.Sprite;
        if (loadUnit.unitCharacteristics.Shield != null)
            Shield.Image.sprite = loadUnit.unitCharacteristics.Shield.Base.Sprite;
        if (loadUnit.unitCharacteristics.Special != null)
            Special.Image.sprite = loadUnit.unitCharacteristics.Special.Base.Sprite;

        RaceLink = loadUnit.unitCharacteristics.RaceLink;
        WeaponLink = loadUnit.unitCharacteristics.WeaponLink;
        ArmorLink = loadUnit.unitCharacteristics.ArmorLink;
        ShieldLink = loadUnit.unitCharacteristics.ShieldLink;
        SpecialLink = loadUnit.unitCharacteristics.SpecialLink;

        Race.Value.text = loadUnit.unitCharacteristics.Value.ToString();
        Weapon.Value.text = loadUnit.unitCharacteristics.Value.ToString();
        Armor.Value.text = loadUnit.unitCharacteristics.Value.ToString();
        Shield.Value.text = loadUnit.unitCharacteristics.Value.ToString();
        Special.Value.text = loadUnit.unitCharacteristics.Value.ToString();


        if (RaceLink != null)
        {
            RaceLink.ThisItem.Value.ItemValue += loadUnit.unitCharacteristics.Value;
            RaceLink.Value.text = RaceLink.ThisItem.Value.ItemValue.ToString();
        }

        if (WeaponLink != null)
        {
            WeaponLink.ThisItem.Value.ItemValue += loadUnit.unitCharacteristics.Value;
            WeaponLink.Value.text = WeaponLink.ThisItem.Value.ItemValue.ToString();
        }
        if (ArmorLink != null)
        {
            ArmorLink.ThisItem.Value.ItemValue += loadUnit.unitCharacteristics.Value;
            ArmorLink.Value.text = ArmorLink.ThisItem.Value.ItemValue.ToString();
        }
        if (ShieldLink != null)
        {
            ShieldLink.ThisItem.Value.ItemValue += loadUnit.unitCharacteristics.Value;
            ShieldLink.Value.text = ShieldLink.ThisItem.Value.ItemValue.ToString();
        }
        if (SpecialLink != null)
        {
            SpecialLink.ThisItem.Value.ItemValue += loadUnit.unitCharacteristics.Value;
            SpecialLink.Value.text = SpecialLink.ThisItem.Value.ItemValue.ToString();
        }
    }

    public void DeleteUnit(Unit deleteUnit)
    {
        RaceLink = deleteUnit.unitCharacteristics.RaceLink;
        WeaponLink = deleteUnit.unitCharacteristics.WeaponLink;
        ArmorLink = deleteUnit.unitCharacteristics.ArmorLink;
        ShieldLink = deleteUnit.unitCharacteristics.ShieldLink;
        SpecialLink = deleteUnit.unitCharacteristics.SpecialLink;

        if (RaceLink != null)
        {
            RaceLink.ThisItem.Value.ItemValue += deleteUnit.unitCharacteristics.Value;
            RaceLink.Value.text = RaceLink.ThisItem.Value.ItemValue.ToString();
        }
        if (WeaponLink != null)
        {
            WeaponLink.ThisItem.Value.ItemValue += deleteUnit.unitCharacteristics.Value;
            WeaponLink.Value.text = WeaponLink.ThisItem.Value.ItemValue.ToString();
        }
        if (ArmorLink != null)
        {
            ArmorLink.ThisItem.Value.ItemValue += deleteUnit.unitCharacteristics.Value;
            ArmorLink.Value.text = ArmorLink.ThisItem.Value.ItemValue.ToString();
        }
        if (ShieldLink != null)
        {
            ShieldLink.ThisItem.Value.ItemValue += deleteUnit.unitCharacteristics.Value;
            ShieldLink.Value.text = ShieldLink.ThisItem.Value.ItemValue.ToString();
        }
        if (SpecialLink != null)
        {
            SpecialLink.ThisItem.Value.ItemValue += deleteUnit.unitCharacteristics.Value;
            SpecialLink.Value.text = SpecialLink.ThisItem.Value.ItemValue.ToString();
        }


        Destroy(PrepareManager.Instance.ChosenUnit.gameObject);
    }

    public void ChangeValueItems()
    {
        foreach (Transform child in RacesGrid)
        {
            child.GetComponent<ItemInfo>().Value.text = child.GetComponent<ItemInfo>().ThisItem.Value.ItemValue.ToString();
        }

        foreach (Transform child in ItemsGrid)
        {
            child.GetComponent<ItemInfo>().Value.text = child.GetComponent<ItemInfo>().ThisItem.Value.ItemValue.ToString();
        }
    }
}
