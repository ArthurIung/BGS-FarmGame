using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIController : BaseInitializer
{

    #region Shop Variables

    [Header("Shop")]
    [SerializeField] Transform _shopObject;
    [Space]
    [SerializeField] Transform _parent_ShopCell;
    [SerializeField] GameObject _prefab_ShopCell;

    [SerializeField] TextMeshProUGUI text_itemPrice;
    [SerializeField] GameObject btn_buy;
    [SerializeField] GameObject btn_sell;

    BaseCell selectedItem;

    public bool _shopIsRevealed;

    Action OnShowShop;
    Action OnHideShop;

    [SerializeField] BaseNPC _currentShopkeeper;

    #endregion

    #region Inventory Variables

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

    [Space, SerializeField] Transform _tutorialObject;
    [SerializeField] CanvasGroup _canvasGroup;

    public override IEnumerator Cor_Initialize()
    {

        LogicController.Instance._playerControl.Interface.Inventory.performed += (x) => RevealInventory();


        _inventoryIsRevealed = false;
        _shopIsRevealed = false;

        LeanTween.scale(_shopObject.gameObject, Vector3.zero, 0f).setEase(LeanTweenType.easeInBack).setOnComplete(() => _shopObject.gameObject.SetActive(false)); ;
        LeanTween.scale(_inventoryObject.gameObject, Vector3.zero, 0f).setEase(LeanTweenType.easeInBack).setOnComplete(() => _inventoryObject.gameObject.SetActive(false)); ;


        yield return StartCoroutine(base.Cor_Initialize());

        LeanTween.alphaCanvas(_canvasGroup, 0, 0.3f).setDelay(0.5f);

    }



    #region Show Inventory

    public void RevealInventory()
    {
        if (_inventoryIsRevealed)
        {
            _inventoryIsRevealed = false;
            LeanTween.scale(_inventoryObject.gameObject, Vector3.zero, 0.2f).setEase(LeanTweenType.easeInBack).setOnComplete(() => _inventoryObject.gameObject.SetActive(false));
            OnHideInventory?.Invoke();
        }
        else
        {
            _inventoryIsRevealed = true;

            LeanTween.scale(_inventoryObject.gameObject, Vector3.one, 0.2f).setEase(LeanTweenType.easeOutBack).setOnStart(() =>
            { 
                _inventoryObject.gameObject.SetActive(true);
                LeanTween.scale(btn_sell, Vector3.zero, 0);
            });
            OnShowInventory?.Invoke();
            RevealInventoryIcons();
        }
    }

    public void HideInventory()
    {
        _inventoryIsRevealed = false;
        LeanTween.scale(_inventoryObject.gameObject, Vector3.zero, 0.2f).setEase(LeanTweenType.easeInBack).setOnComplete(() => _inventoryObject.gameObject.SetActive(false));
        OnHideInventory?.Invoke();
    }

    public void RevealInventoryIcons()
    {
        Character_Inventory character_Inventory = LogicController.Instance.Player.Inventory;

        LogicController.Instance.DestroyAllChildren(_parent_inventoryCell);

        foreach (Character_Inventory.Cell _c in character_Inventory.Inventory)
        {
            if (_c == null) continue;
            GameObject cellReference = Instantiate(_prefab_inventoryCell, _parent_inventoryCell);
            cellReference.GetComponent<InventoryCell>().ConfigureCell(_c);

            cellReference.transform.localScale = Vector3.zero;
            LeanTween.scale(cellReference, Vector3.one, 0.2f).setEase(LeanTweenType.easeInOutBack).setDelay(0.1f).setOnComplete(() =>
            {
                cellReference.GetComponent<ButtonAnimation>().Anim_Idle();
            }); ;

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
            LeanTween.scale(_shopObject.gameObject, Vector3.zero, 0.2f).setEase(LeanTweenType.easeInBack).setOnComplete(() => _shopObject.gameObject.SetActive(false));
            LeanTween.scale(_inventoryObject.gameObject, Vector3.zero, 0.2f).setEase(LeanTweenType.easeInBack).setOnComplete(() =>
            {
                _inventoryObject.gameObject.SetActive(false);
                
            }); 
            OnHideShop?.Invoke();
        }
        else
        {
            _shopIsRevealed = true;

            _currentShopkeeper = shopKeeper;

            LeanTween.scale(_shopObject.gameObject, Vector3.one, 0.2f).setEase(LeanTweenType.easeOutBack).setOnStart(() => _shopObject.gameObject.SetActive(true));
            LeanTween.scale(_inventoryObject.gameObject, Vector3.one, 0.2f).setEase(LeanTweenType.easeOutBack).setOnStart(() =>
            {
                _inventoryObject.gameObject.SetActive(true);
            });



            OnShowShop?.Invoke();
            RevealShopIcons(shopKeeper.NPCInteraction.ShopItems);
            RevealInventoryIcons();
        }
    }

    void RevealShopIcons(List<NPC_Interaction.Shop> shopList)
    {

        LeanTween.scale(btn_sell, Vector3.zero, 0.3f).setEase(LeanTweenType.easeInBack);
        LeanTween.scale(btn_buy, Vector3.zero, 0.3f).setEase(LeanTweenType.easeInBack);


        LogicController.Instance.DestroyAllChildren(_parent_ShopCell);

        foreach (NPC_Interaction.Shop _c in shopList)
        {
            if (_c == null) continue;
            GameObject cellReference = Instantiate(_prefab_ShopCell, _parent_ShopCell);
            cellReference.GetComponent<ShopCell>().ConfigureCell(_c);

            cellReference.transform.localScale = Vector3.zero;
            LeanTween.scale(cellReference, Vector3.one, 0.2f).setEase(LeanTweenType.easeInOutBack).setDelay(0.1f).setOnComplete(() =>
            {
                cellReference.GetComponent<ButtonAnimation>().Anim_Idle();
            });

        }
    }

    public void HideShop()
    {
        if (_shopIsRevealed)
        {
            _currentShopkeeper = null;
            _shopIsRevealed = false;
            LeanTween.scale(_shopObject.gameObject, Vector3.zero, 0.2f).setEase(LeanTweenType.easeInBack).setOnComplete(() => _shopObject.gameObject.SetActive(false));
            LeanTween.scale(_inventoryObject.gameObject, Vector3.zero, 0.2f).setEase(LeanTweenType.easeInBack).setOnComplete(() => _inventoryObject.gameObject.SetActive(false));
            OnHideShop?.Invoke();
        }
    }

    public void ClickOnShopCell(ShopCell _currentItem)
    {

        if (selectedItem != null)
        {
            selectedItem.UnselectThis();
        }

        selectedItem = _currentItem;
        text_itemPrice.text = "Price: $" + _currentItem.CurrentItem.shopPrice;
        if (LogicController.Instance.Player.CurrentMoney - selectedItem.CurrentItem.shopPrice >= 0)
            LeanTween.scale(btn_buy, Vector3.one, 0.3f).setEase(LeanTweenType.easeOutBack);
        LeanTween.scale(btn_sell, Vector3.zero, 0.3f).setEase(LeanTweenType.easeOutBack);

    }

    public void ClickOnInventoryCell(InventoryCell _currentItem)
    {
        if(selectedItem != null) 
        {
            selectedItem.UnselectThis();
        }


        selectedItem = _currentItem;

        text_itemPrice.text = "Sell Price: $" + _currentItem.CurrentItem.shopPrice/2;
        LeanTween.scale(btn_sell, Vector3.one, 0.3f).setEase(LeanTweenType.easeOutBack);
        LeanTween.scale(btn_buy, Vector3.zero, 0.3f).setEase(LeanTweenType.easeOutBack);
    }



    #endregion


    #region Button Interactions
    public void Button_Buy()
    {
        if (selectedItem == null) return;

        
            LogicController.Instance.Player.InsertMoney(-selectedItem.CurrentItem.shopPrice);
            LogicController.Instance.Player.Inventory.InsertItem(selectedItem.CurrentItem);
            _currentShopkeeper.NPCInteraction.RemoveItem(selectedItem.CurrentItem);

            RevealShopIcons(_currentShopkeeper.NPCInteraction.ShopItems);
            RevealInventoryIcons();

            selectedItem = null;

            text_itemPrice.text = "";
    }

    public void Button_Sell()
    {
        if (selectedItem == null) return;

        LogicController.Instance.Player.InsertMoney(selectedItem.CurrentItem.shopPrice / 2);
        LogicController.Instance.Player.Inventory.RemoveItem(selectedItem.CurrentItem);
        _currentShopkeeper.NPCInteraction.InsertItem(selectedItem.CurrentItem);

        RevealShopIcons(_currentShopkeeper.NPCInteraction.ShopItems);
        RevealInventoryIcons();

        text_itemPrice.text = "";

        selectedItem = null;
    }


    #endregion


    public void HideTutorial()
    {
        LeanTween.scale(_tutorialObject.gameObject, Vector3.zero, 0.3f).setEase(LeanTweenType.easeOutBack);
        LogicController.Instance._timeController.timeIsRunning = true;
    }

}
