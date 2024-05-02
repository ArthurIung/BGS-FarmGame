using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Character_Inventory : BaseInitializer
{

    [System.Serializable]
    public class Cell 
    {
        public int _amount;
        public Scriptable_Items _itemReference;
    }



    BaseCharacter _base;

    [SerializeField] List<Cell> _inventory  = new List<Cell>();
    public List<Cell> Inventory { get { return _inventory; } } 

    public override IEnumerator Cor_Initialize()
    {
        _base = GetComponent<BaseCharacter>();
        yield return StartCoroutine(base.Cor_Initialize());
    }

    public void InsertItem(Scriptable_Items _i)
    {
        Cell newCell = new Cell();
        newCell._itemReference = Instantiate(_i);

        _inventory.Add(newCell);
    }



}
