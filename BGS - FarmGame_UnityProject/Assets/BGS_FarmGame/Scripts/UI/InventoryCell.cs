using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryCell : MonoBehaviour
{

    [SerializeField] Scriptable_Items _currentItem;
    [SerializeField] Image _iconSprite;

    public void ConfigureCell(Character_Inventory.Cell _c)
    {
        _iconSprite.sprite = _c._itemReference._itemIcon;
        _currentItem = Instantiate(_c._itemReference);
    }

    public void Button_SelectThisItem()
    {
        LogicController.Instance.Player.Equipment.ChangeEquipment(_currentItem);
    }

}
