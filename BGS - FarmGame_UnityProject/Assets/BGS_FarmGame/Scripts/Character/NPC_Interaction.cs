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

    [SerializeField] List<Shop> _items = new List<Shop>();
    public List<Shop> Items { get { return _items; } }
    [SerializeField]float _distanceToInteraction = 0.2f;


    public override IEnumerator Cor_Initialize()
    {
        _base = GetComponent<BaseNPC>();
        CreateInputAction();
        yield return StartCoroutine(base.Cor_Initialize());
    }

    public void CreateInputAction()
    {
        LogicController.Instance._playerControl.Interface.Shop.performed += (x) => InteractWith();
        LogicController.Instance._playerControl.Movement.Direction.performed += (x) => LogicController.Instance._uiController.HideShop();
        LogicController.Instance._playerControl.Movement.Direction.performed += (x) => LogicController.Instance._uiController.HideInventory();
    }


    void InteractWith()
    {
        float distanceFromPlayer = Vector2.Distance(LogicController.Instance.Player.transform.position, transform.position);


        if (distanceFromPlayer < _distanceToInteraction)
        {
            LogicController.Instance._uiController.RevealShop(_base);
        }
    }

    internal void RemoveItem(Scriptable_Items selectedItem)
    {
        _items.RemoveAll(x=>x._item._id == selectedItem._id);
    }

}
