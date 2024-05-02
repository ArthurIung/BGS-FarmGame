using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using static Character_Equipment.EquipmentBodyPart;

public class Character_Equipment : BaseInitializer
{
    BaseCharacter _base;

    [System.Serializable]
    public class EquipmentBodyPart 
    {
        public enum BodyPart
        {
            Head,
            Torso
        }

        public BodyPart _bodyPart;
        public Scriptable_Items _equippedItem;

        public Animator animatorBodyPart;

    }
    public List<EquipmentBodyPart> allBodyParts = new List<EquipmentBodyPart>();


    public override IEnumerator Cor_Initialize()
    {
        _base = GetComponent<BaseCharacter>();
        _base.OnStartMoving += AnimateEquipment;
        _base.OnStopMoving += AnimateEquipment;
        yield return StartCoroutine(base.Cor_Initialize());

    }

    #region Equip Functions

    public void ChangeEquipment(Scriptable_Items ItemToEquip)
    {

        Scriptable_Equipment equipment = (ItemToEquip as Scriptable_Equipment);

        EquipmentBodyPart bodyPart = allBodyParts.Find(x => x._bodyPart == equipment._bodyPartSlot);

        if (bodyPart != null)
        {
            if(bodyPart._equippedItem._id == ItemToEquip._id)
            {
                bodyPart._equippedItem = null;
                bodyPart.animatorBodyPart.runtimeAnimatorController = null;
                bodyPart.animatorBodyPart.GetComponent<SpriteRenderer>().sprite = null;
            }
            else
            {
                bodyPart._equippedItem = ItemToEquip;
                bodyPart.animatorBodyPart.runtimeAnimatorController = equipment._equipmentAnimator;
            }
        }
    }

    public void Unnequip(EquipmentBodyPart.BodyPart bodyPartToRemoveEquipment)
    {
        EquipmentBodyPart bodyPart = allBodyParts.Find(x => x._bodyPart == bodyPartToRemoveEquipment);

        if (bodyPart != null)
        {
            bodyPart._equippedItem = null;
            bodyPart.animatorBodyPart.GetComponent<SpriteRenderer>().sprite = null;
        }


    }

    #endregion


    void AnimateEquipment(Vector3 directionMovement, bool isWalking)
    {
        foreach (EquipmentBodyPart bodyPart in allBodyParts)
        {
            if (bodyPart._equippedItem == null) continue;
            bodyPart.animatorBodyPart.SetFloat("Direction-X", directionMovement.x);
            bodyPart.animatorBodyPart.SetFloat("Direction-Y", directionMovement.y);
            bodyPart.animatorBodyPart.SetBool("IsWalking", isWalking);


        }
    }

}
