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

    /// <summary>
    /// Input Controller
    /// </summary>
    public PlayerControl _playerControl;

    /// <summary>
    /// Player Controller
    /// </summary>
    [SerializeField] BaseCharacter _playerCharacter;
    public BaseCharacter Player
    {
        get { return _playerCharacter; }
    }

    public UIController _uiController;

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
        yield return StartCoroutine(_uiController.Cor_Initialize());


        yield return StartCoroutine(base.Cor_Initialize());
    }

    public void DestroyAllChildren(Transform parentToDestroy)
    {
        for (int i = parentToDestroy.childCount - 1; i >= 0; i--)
        {
            Destroy(parentToDestroy.GetChild(i).gameObject);
        }
    }

}
