using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Character_Inventory : BaseInitializer
{

    [System.Serializable]
    public class Cell 
    {
        public int _id;
        public string _Name;
        public int _amount;
        public Scriptable_Items _itemReference;
    }



    BaseCharacter _base;

    [SerializeField] List<Cell> _inventory  = new List<Cell>();
    public List<Cell> Inventory { get { return _inventory; } } 

    public override IEnumerator Cor_Initialize()
    {
        _base = GetComponent<BaseCharacter>();
        yield return StartCoroutine(base.Cor_Initialize());
    }


    #region Temporário

    private void Update()
    {
        if (LogicController.Instance == null) return;

        //if(Keyboard.current.eKey.isPressed)
        //{
        //    _base.Equipment.Equip(_inventory[0]);
        //}

        //if(Keyboard.current.rKey.isPressed)
        //{
        //    _base.Equipment.Unnequip(Character_Equipment.EquipmentBodyPart.BodyPart.Head);
        //}



    }

    #endregion


}
