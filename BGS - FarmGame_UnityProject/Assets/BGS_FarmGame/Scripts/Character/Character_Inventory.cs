using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering.Universal;

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

    public void RemoveItem(Scriptable_Items _i)
    {
        Debug.LogAssertion(_i._id);
        _inventory.RemoveAll((x) => _i._id == x._itemReference._id);
    }


}
