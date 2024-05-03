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
    public BasePlayer Player
    {
        get { return _playerCharacter as BasePlayer; }
    }

    [SerializeField] List<BaseCharacter> _allnpcs = new List<BaseCharacter>();

    public UIController _uiController;
    public TimeController _timeController;

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

        for (int i = 0; i < _allnpcs.Count; i++)
        {
            yield return StartCoroutine(_allnpcs[i].Cor_Initialize());
        }

        yield return StartCoroutine(_timeController.Cor_Initialize());
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
