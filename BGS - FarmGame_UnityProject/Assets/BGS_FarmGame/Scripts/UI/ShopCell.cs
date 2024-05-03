using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopCell : BaseCell
{

    [SerializeField] Image _iconSprite;

    public void ConfigureCell(NPC_Interaction.Shop _c)
    {
        _iconSprite.sprite = _c._item._itemIcon;
        _currentItem = Instantiate(_c._item);
        
    }

    public override void Button_SelectThisItem()
    {
        LogicController.Instance._uiController.ClickOnShopCell(this);

        base.Button_SelectThisItem();

    }

}
