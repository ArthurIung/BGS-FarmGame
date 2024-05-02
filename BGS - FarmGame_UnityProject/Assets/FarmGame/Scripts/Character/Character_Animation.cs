using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character_Animation : BaseInitializer
{
    #region Private Variables

    [SerializeField] Animator _animator;

    #endregion


    public void AnimateMovement(float xDirection, float yDirection)
    {
        AnimateFloatValue("Direction-X", xDirection);
        AnimateFloatValue("Direction-Y", yDirection);
    }

    public void SetWalk(bool isWalking)
    {
        AnimateBool("IsWalking", isWalking);
    }




    #region Animation Functions

    void AnimateFloatValue(string name, float value)
    {
        _animator.SetFloat(name, value);
    }

    void AnimateBool(string name, bool value)
    {
        _animator.SetBool(name, value);
    }

    #endregion


}
