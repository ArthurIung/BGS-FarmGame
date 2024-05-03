using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BaseCell : MonoBehaviour
{
    [SerializeField]protected Image _bgCell;
    [SerializeField]protected Sprite _selectedSprite;
    [SerializeField]protected Sprite _unselectedSprite;

    [SerializeField]protected Scriptable_Items _currentItem;
    public Scriptable_Items CurrentItem
    {
        get { return _currentItem; }
    }


    public virtual void Button_SelectThisItem()
    {
        _bgCell.sprite = _selectedSprite;
    }

    public virtual void UnselectThis()
    {
        _bgCell.sprite = _unselectedSprite;
    }

}
