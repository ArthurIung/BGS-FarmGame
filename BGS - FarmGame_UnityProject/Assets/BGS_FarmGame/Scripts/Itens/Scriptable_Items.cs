using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu()]
public class Scriptable_Items : ScriptableObject
{
    [Tooltip("ID of identification. Certify that is single for all itens")]
    /// <summary>
    /// Id of Identification
    /// </summary>
    public string _id;

    public Sprite _itemIcon;

    [Space]
    [Tooltip("The price that the Shopkeeper will sell this item. Player will sell at half of the value")]
    public float shopPrice;

}
