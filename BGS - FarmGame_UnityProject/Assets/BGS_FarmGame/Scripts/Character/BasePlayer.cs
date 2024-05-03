using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BasePlayer : BaseCharacter
{
    [Space]
    [SerializeField] Character_Inventory _characterInventory;
    public Character_Inventory Inventory { get { return _characterInventory; } }


    [SerializeField] float _currentMoney;
    public float CurrentMoney
    {
        get { return _currentMoney; }
    }

    #region Unity Functions

    private void Update()
    {
        if (!IsInitialized) return;

        if (isMoving)
            transform.position += Direction * _speed * Time.deltaTime;

        _characterAnimation.SetWalk(isMoving);
        _characterAnimation.AnimateMovement(Direction.x, Direction.y);


    }

    #endregion 

    #region Initialization

    public override IEnumerator Cor_Initialize()
    {
        CreateInputAction();
        if (_characterAnimation != null)
            yield return StartCoroutine(_characterAnimation.Cor_Initialize());
        if (_characterInventory != null)
            yield return StartCoroutine(_characterInventory.Cor_Initialize());
        if (_characterEquipment != null)
            yield return StartCoroutine(_characterEquipment.Cor_Initialize());

        yield return StartCoroutine(base.Cor_Initialize());
    }

    /// <summary>
    /// Create callback to perfomed inputs
    /// </summary>
    public void CreateInputAction()
    {
        LogicController.Instance._playerControl.Movement.Direction.performed += MovePosition;
        LogicController.Instance._playerControl.Movement.Direction.canceled += (x) => isMoving = false;
        LogicController.Instance._playerControl.Movement.Direction.canceled += (x) => OnStopMoving?.Invoke(Direction, isMoving);

    }

    #endregion


    private void MovePosition(InputAction.CallbackContext context)
    {
        _movementDirection = context.ReadValue<Vector2>();
        isMoving = true;
        OnStartMoving?.Invoke(Direction, isMoving);
    }

    public void InsertMoney(float amount)
    {
        _currentMoney += amount;
        _currentMoney = Mathf.Clamp(_currentMoney, 0, float.MaxValue);
    }

}
