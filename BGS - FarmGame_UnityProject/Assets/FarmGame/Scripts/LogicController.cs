using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogicController : BaseInitializer
{
    static LogicController _instance;
    public static LogicController Instance 
    { 
        get 
        { 
            if (_instance == null)
                _instance = GameObject.FindObjectOfType<LogicController>();

            return _instance;
        }
    }



    #region Public Variables

    public BaseCharacter _playerCharacter;
    public PlayerControl _playerControl;

    #endregion

    #region Unity Functions

    private void Awake()
    {
        _playerControl = new PlayerControl();
        _playerControl.Enable();

        StartCoroutine(Cor_Initialize());
    }

    #endregion


    public override IEnumerator Cor_Initialize()
    {

        yield return StartCoroutine(_playerCharacter.Cor_Initialize());


        yield return StartCoroutine(base.Cor_Initialize());
    }

}
