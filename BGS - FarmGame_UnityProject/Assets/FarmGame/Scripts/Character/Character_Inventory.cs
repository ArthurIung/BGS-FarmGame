using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Character_Inventory : BaseInitializer
{

    BaseCharacter _base;

    [SerializeField] List<Scriptable_Items> _inventory  = new List<Scriptable_Items>();

    public override IEnumerator Cor_Initialize()
    {
        _base = GetComponent<BaseCharacter>();
        yield return StartCoroutine(base.Cor_Initialize());
    }


    #region Temporário

    private void Update()
    {
        if (LogicController.Instance == null) return;

        if(Keyboard.current.eKey.isPressed)
        {
            _base.Equipment.Equip(_inventory[0]);
        }

        if(Keyboard.current.rKey.isPressed)
        {
            _base.Equipment.Unnequip(Character_Equipment.EquipmentBodyPart.BodyPart.Head);
        }



    }

    #endregion


}
