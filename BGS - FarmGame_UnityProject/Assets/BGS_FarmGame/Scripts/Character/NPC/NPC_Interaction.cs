using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_Interaction : BaseInitializer
{

    BaseNPC _base;

    [System.Serializable]
    public class Shop
    {
        public Scriptable_Items _item;
    }

    [SerializeField] List<Shop> _shopItems = new List<Shop>();
    public List<Shop> ShopItems { get { return _shopItems; } }
    [SerializeField]float _distanceToInteraction = 0.2f;

    [Space]
    public bool _canInteractWithThis;

    [SerializeField] Transform _iconCanInteractWith;

    #region Initialization

    public override IEnumerator Cor_Initialize()
    {
        _base = GetComponent<BaseNPC>();
        CreateInputAction();
        yield return StartCoroutine(base.Cor_Initialize());

        _base.NPCSchedule.OnReachDestination += ()=> _canInteractWithThis = true;
        _base.NPCSchedule.OnLeaveDestination += ()=> _canInteractWithThis = false;
        _base.NPCSchedule.OnMove += () => VerifyDistance();

        _base.NPCSchedule.OnLeaveDestination += () => LogicController.Instance._uiController.HideShop();
    }

    public void CreateInputAction()
    {
        LogicController.Instance._playerControl.Interface.Shop.performed += (x) => InteractWith();
        LogicController.Instance._playerControl.Movement.Direction.performed += (x) => LogicController.Instance._uiController.HideShop();
        LogicController.Instance._playerControl.Movement.Direction.performed += (x) => LogicController.Instance._uiController.HideInventory();
        
        
    }

    #endregion

    #region Movement

    private void Update()
    {
        //This is to verify the proximity of the player, to show the interaction icon
        if (LogicController.Instance._playerControl.Movement.Direction.IsPressed())
            VerifyDistance();
    }

    void VerifyDistance()
    {

        float distanceFromPlayer = Vector2.Distance(LogicController.Instance.Player.transform.position, transform.position);


        if (distanceFromPlayer < _distanceToInteraction && _canInteractWithThis)
        {
            LeanTween.scale(_iconCanInteractWith.gameObject, Vector3.one, 0.2f).setEase(LeanTweenType.easeOutBack);
        }
        else
            LeanTween.scale(_iconCanInteractWith.gameObject, Vector3.zero, 0.2f).setEase(LeanTweenType.easeOutBack);
    }

    public void InteractWith()
    {
        if (!_canInteractWithThis) return;
        float distanceFromPlayer = Vector2.Distance(LogicController.Instance.Player.transform.position, transform.position);


        if (distanceFromPlayer < _distanceToInteraction)
        {
            LogicController.Instance._uiController.RevealShop(_base);
        }
    }



    #endregion

    #region Inventory

    public void RemoveItem(Scriptable_Items selectedItem)
    {
        _shopItems.RemoveAll(x=>x._item._id == selectedItem._id);
    }

    public void InsertItem(Scriptable_Items selectedItem)
    {
        Shop newItem = new Shop();
        newItem._item = selectedItem;
        _shopItems.Add(newItem);
    }

    #endregion
}
