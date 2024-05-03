using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class Scriptable_Equipment : Scriptable_Items
{

    public enum EquipmentType 
    { 
        None = 0,
        Hat = 1,
        FullCloth = 2
    }

    [Tooltip("Equipment type value")]
    public EquipmentType _equipmentType_enum;
    [Tooltip("The body part that this equipment will be placed")]
    public Character_Equipment.EquipmentBodyPart.BodyPart _bodyPartSlot;
    /// <summary>
    /// The Animator that will animate the sprite
    /// </summary>
    [Tooltip("Animator of the sprite")]
    public RuntimeAnimatorController _equipmentAnimator;


    
}
