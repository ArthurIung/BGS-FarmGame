using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryCell : BaseCell
{

    [SerializeField] Image _iconSprite;

    public void ConfigureCell(Character_Inventory.Cell _c)
    {
        _iconSprite.sprite = _c._itemReference._itemIcon;
        _currentItem = Instantiate(_c._itemReference);

        if (LogicController.Instance.Player.Equipment.IsThisEquiped(_currentItem))
        {
            _bgCell.sprite = _selectedSprite;
        }
        else
        {
            _bgCell.sprite = _unselectedSprite;
        }

    }

    public override void Button_SelectThisItem()
    {
        //This Verifies if the interaction is with the shopkeeper. That will prevent the necessity of the creation of another interface for the player's inventory
        if (!LogicController.Instance._uiController._shopIsRevealed)
        {
            LogicController.Instance.Player.Equipment.ChangeEquipment(this);
        }
        else
        {
            LogicController.Instance._uiController.ClickOnInventoryCell(this);
        }

        base.Button_SelectThisItem();
    }



}
