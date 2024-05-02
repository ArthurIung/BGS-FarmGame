using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopCell : MonoBehaviour
{
    [SerializeField] Scriptable_Items _currentItem;
    [SerializeField] Image _iconSprite;

    public void ConfigureCell(NPC_Interaction.Shop _c)
    {
        _iconSprite.sprite = _c._item._itemIcon;
        _currentItem = Instantiate(_c._item);
    }

    public void Button_SelectThisItem()
    {
        LogicController.Instance._uiController.ShowPriceItem(_currentItem);
    }
}
