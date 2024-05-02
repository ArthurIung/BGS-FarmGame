using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.EditorTools;
using UnityEngine;

public class UIController : BaseInitializer
{

    #region Inventory

    [Header("Inventory")]
    [SerializeField] Transform _inventoryObject;
    [Space]
    [SerializeField] Transform _parent_inventoryCell;
    [SerializeField] GameObject _prefab_inventoryCell;

    bool _inventoryIsRevealed;

    Action OnShowInventory;
    Action OnHideInventory;

    #endregion


    public override IEnumerator Cor_Initialize()
    {

        LogicController.Instance._playerControl.Interface.Inventory.performed += (x) => RevealInventory();

        _inventoryIsRevealed = false;
        _inventoryObject.gameObject.SetActive(false);

        yield return StartCoroutine(base.Cor_Initialize());
    }



    #region Show Interfaces

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


    }

    #endregion

}
