using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class BaseCharacter : BaseInitializer
{
    #region Private Variables

    [SerializeField] Character_Animation _characterAnimation;
    public Character_Animation Animation;
    [SerializeField] Character_Inventory _characterInventory;
    public Character_Inventory Inventory { get { return _characterInventory; } }
    [SerializeField] Character_Equipment _characterEquipment;
    public Character_Equipment Equipment { get { return _characterEquipment; } }


    #region Movement

    bool isMoving;
    [SerializeField] Vector2 _movementDirection;

    public Vector3 Direction
    {
        get
        {
            return _movementDirection;
        }
    }

    [SerializeField] float _speed;

    #endregion


    #endregion

    #region CallBacks

    public Action<Vector3, bool> OnStartMoving;
    public Action<Vector3, bool> OnStopMoving;


    #endregion



    #region Unity Functions



    private void Update()
    {
        if (!IsInitialized) return;

        if(isMoving)
            transform.position += Direction * _speed * Time.deltaTime;

        _characterAnimation.SetWalk(isMoving);
        _characterAnimation.AnimateMovement(Direction.x, Direction.y);


    }

    #endregion 

    #region Initialization

    public override IEnumerator Cor_Initialize()
    {
        CreateInputAction();
        yield return StartCoroutine(_characterAnimation.Cor_Initialize());
        yield return StartCoroutine(_characterInventory.Cor_Initialize());
        yield return StartCoroutine(_characterEquipment.Cor_Initialize());

        yield return StartCoroutine(base.Cor_Initialize());
    }

    /// <summary>
    /// Create callback to perfomed inputs
    /// </summary>
    public void CreateInputAction()
    {
        LogicController.Instance._playerControl.Movement.Direction.performed += MovePosition;

        //LogicController.Instance._playerControl.Movement.XMovement.canceled += (x) => _xMovementDirection = 0;
        LogicController.Instance._playerControl.Movement.Direction.canceled += (x) => isMoving = false;
        LogicController.Instance._playerControl.Movement.Direction.canceled += (x) => OnStopMoving?.Invoke(Direction, isMoving);
        //LogicController.Instance._playerControl.Movement.YMovement.canceled += (x) => _yMovementDirection = 0;
        //LogicController.Instance._playerControl.Movement.YMovement.canceled += (x) => OnStopMoving?.Invoke(Direction);

    }

    #endregion


    private void MovePosition(InputAction.CallbackContext context)
    {
        _movementDirection = context.ReadValue<Vector2>();
        isMoving = true;
        OnStartMoving?.Invoke(Direction, isMoving);
    }

    private void MoveYPosition(InputAction.CallbackContext context)
    {
        //_yMovementDirection = -context.ReadValue<float>();
        OnStartMoving?.Invoke(Direction, isMoving);
    }
}
