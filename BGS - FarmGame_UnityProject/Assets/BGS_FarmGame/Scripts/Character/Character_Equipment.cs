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
        public InventoryCell _cellReference;

        public Animator animatorBodyPart;

    }
    public List<EquipmentBodyPart> allBodyParts = new List<EquipmentBodyPart>();


    public override IEnumerator Cor_Initialize()
    {
        _base = GetComponent<BaseCharacter>();
        _base.OnStartMoving += AnimateEquipment;
        _base.OnStopMoving += AnimateEquipment;

        foreach (EquipmentBodyPart part in allBodyParts)
        {
            if (part._equippedItem == null) continue;
            ForceEquipment(part);
        }

        yield return StartCoroutine(base.Cor_Initialize());

    }

    #region Equip Functions

    public void ChangeEquipment(InventoryCell cellItem)
    {

        Scriptable_Equipment equipment = (cellItem.CurrentItem as Scriptable_Equipment);

        EquipmentBodyPart bodyPart = allBodyParts.Find(x => x._bodyPart == equipment._bodyPartSlot);

        if (bodyPart != null)
        {
            if(bodyPart._equippedItem != null && bodyPart._equippedItem._id == cellItem.CurrentItem._id) //Check if is clicking on the equipped item
            {
                bodyPart._equippedItem = null;
                bodyPart._cellReference = null;
                bodyPart.animatorBodyPart.runtimeAnimatorController = null;
                bodyPart.animatorBodyPart.GetComponent<SpriteRenderer>().sprite = null;
                cellItem.UnselectThis();
            }
            else
            {
                if (bodyPart._cellReference != null)
                    bodyPart._cellReference.UnselectThis();

                bodyPart._equippedItem = cellItem.CurrentItem;
                bodyPart.animatorBodyPart.runtimeAnimatorController = equipment._equipmentAnimator;

                AnimateEquipment(_base.Direction, false);

                bodyPart._cellReference = cellItem;
            }
        }
    }

    public void ForceEquipment(EquipmentBodyPart bodyPart)
    {
        bodyPart.animatorBodyPart.runtimeAnimatorController = (bodyPart._equippedItem as Scriptable_Equipment)._equipmentAnimator;

        AnimateEquipment(_base.Direction, false);
    }

    public void Unnequip(EquipmentBodyPart.BodyPart bodyPartToRemoveEquipment)
    {
        EquipmentBodyPart bodyPart = allBodyParts.Find(x => x._bodyPart == bodyPartToRemoveEquipment);

        if (bodyPart != null)
        {
            bodyPart._equippedItem = null;
            bodyPart._cellReference = null;
            bodyPart.animatorBodyPart.runtimeAnimatorController = null;
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

    /// <summary>
    /// Verify if the item is currently equipped in his respective body part
    /// </summary>
    /// <param name="currentItem">Item to verify</param>
    /// <returns>True: Has this item equipped</returns>
    public bool IsThisEquiped(Scriptable_Items currentItem)
    {
        Scriptable_Equipment equipment = (currentItem as Scriptable_Equipment);
        EquipmentBodyPart bodyPart = allBodyParts.Find(x => x._bodyPart == equipment._bodyPartSlot);
        return (bodyPart._equippedItem != null && bodyPart._equippedItem._id == currentItem._id);

    }
}
