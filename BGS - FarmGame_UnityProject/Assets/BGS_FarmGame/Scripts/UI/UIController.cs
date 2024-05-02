using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using TMPro.EditorUtilities;
using UnityEditor.EditorTools;
using UnityEngine;

public class UIController : BaseInitializer
{

    #region Shop

    [Header("Shop")]
    [SerializeField] Transform _shopObject;
    [Space]
    [SerializeField] Transform _parent_ShopCell;
    [SerializeField] GameObject _prefab_ShopCell;

    [SerializeField] TextMeshProUGUI text_itemPrice;

    Scriptable_Items selectedItem;

    bool _shopIsRevealed;

    Action OnShowShop;
    Action OnHideShop;

    [SerializeField]BaseNPC _currentShopkeeper;

    #endregion

    #region Inventory

    [Header("Inventory")]
    [SerializeField] Transform _inventoryObject;
    [Space]
    [SerializeField] Transform _parent_inventoryCell;
    [SerializeField] GameObject _prefab_inventoryCell;

    [Space]
    [SerializeField] TextMeshProUGUI _textPlayerMoney;

    bool _inventoryIsRevealed;

    Action OnShowInventory;
    Action OnHideInventory;

    #endregion


    public override IEnumerator Cor_Initialize()
    {

        LogicController.Instance._playerControl.Interface.Inventory.performed += (x) => RevealInventory();
        

        _inventoryIsRevealed = false;
        _inventoryObject.gameObject.SetActive(false);

        _shopIsRevealed = false;
        _shopObject.gameObject.SetActive(false);

        yield return StartCoroutine(base.Cor_Initialize());
    }



    #region Show Inventory

    public void RevealInventory()
    {
        if(_inventoryIsRevealed)
        {
            _inventoryIsRevealed = false;
            _inventoryObject.gameObject.SetActive(false);
            OnHideInventory?.Invoke();
        }
        else
        {
            _inventoryIsRevealed = true;

            _inventoryObject.gameObject.SetActive(true);
            OnShowInventory?.Invoke();
            RevealInventoryIcons();
        }
    }

    public void HideInventory()
    {
        _inventoryIsRevealed = false;
        _inventoryObject.gameObject.SetActive(false);
        OnHideInventory?.Invoke();
    }

    void RevealInventoryIcons()
    {
        Character_Inventory character_Inventory = LogicController.Instance.Player.Inventory;

        LogicController.Instance.DestroyAllChildren(_parent_inventoryCell);

        foreach (Character_Inventory.Cell _c in character_Inventory.Inventory)
        {
            if(_c == null) continue;
            GameObject cellReference = Instantiate(_prefab_inventoryCell, _parent_inventoryCell);
            cellReference.GetComponent<InventoryCell>().ConfigureCell(_c);
        }

        _textPlayerMoney.text = "Money: $" + LogicController.Instance.Player.CurrentMoney;


    }

    #endregion


    #region Show Shop

    public void RevealShop(BaseNPC shopKeeper)
    {
        if (_shopIsRevealed)
        {

            _currentShopkeeper = null;

            _shopIsRevealed = false;
            _shopObject.gameObject.SetActive(false);
            _inventoryObject.gameObject.SetActive(false);
            OnHideShop?.Invoke();
        }
        else
        {
            _shopIsRevealed = true;

            _currentShopkeeper = shopKeeper;

            _shopObject.gameObject.SetActive(true);
            _inventoryObject.gameObject.SetActive(true);
            OnShowShop?.Invoke();
            RevealShopIcons(shopKeeper.NPCInteraction.Items);
            RevealInventoryIcons();
        }
    }

    void RevealShopIcons(List<NPC_Interaction.Shop> shopList)
    {
        LogicController.Instance.DestroyAllChildren(_parent_ShopCell);

        foreach (NPC_Interaction.Shop _c in shopList)
        {
            if (_c == null) continue;
            GameObject cellReference = Instantiate(_prefab_ShopCell, _parent_ShopCell);
            cellReference.GetComponent<ShopCell>().ConfigureCell(_c);
        }
    }

    public void HideShop()
    {
        if (_shopIsRevealed)
        {
            _currentShopkeeper = null;
            _shopIsRevealed = false;
            _shopObject.gameObject.SetActive(false);
            _inventoryObject.gameObject.SetActive(false);
            OnHideShop?.Invoke();
        }
    }

    public void ShowPriceItem(Scriptable_Items _currentItem)
    {
        selectedItem = _currentItem;
        text_itemPrice.text = "Price: $" + _currentItem.shopPrice;
    }

    public void Button_Buy()
    {
        if (selectedItem == null) return;

        if(LogicController.Instance.Player.CurrentMoney - selectedItem.shopPrice >= 0)
        {
            LogicController.Instance.Player.InsertMoney(-selectedItem.shopPrice);
            LogicController.Instance.Player.Inventory.InsertItem(selectedItem);
            _currentShopkeeper.NPCInteraction.RemoveItem(selectedItem);

            RevealShopIcons(_currentShopkeeper.NPCInteraction.Items);
            RevealInventoryIcons();
        }
    }

    #endregion

}
