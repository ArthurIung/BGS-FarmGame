using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class Scriptable_Equipment : Scriptable_Items
{

    public enum EquipmentType 
    { 
        None = 0,
        Hat = 1
    }

    public EquipmentType _equipmentType_enum;
    public Character_Equipment.EquipmentBodyPart.BodyPart _bodyPartSlot;

    public RuntimeAnimatorController _equipmentAnimator;


    
}
