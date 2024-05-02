using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class BaseCharacter : BaseInitializer
{
    [SerializeField]protected Character_Animation _characterAnimation;
    public Character_Animation Animation
    {
        get { return _characterAnimation; }
    }

    #region Movement
    protected bool isMoving;
    [SerializeField]protected Vector2 _movementDirection;

    public Vector3 Direction
    {
        get
        {
            return _movementDirection;
        }
    }

    [SerializeField]protected float _speed;

    #endregion


    #region CallBacks

    public Action<Vector3, bool> OnStartMoving;
    public Action<Vector3, bool> OnStopMoving;


    #endregion

}
