using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseNPC : BaseCharacter
{
    [Space]
    [SerializeField] NPC_Interaction _nPCInteraction;
    public NPC_Interaction NPCInteraction
    {
        get { return _nPCInteraction; }
    }

    public override IEnumerator Cor_Initialize()
    {
        yield return StartCoroutine(_nPCInteraction.Cor_Initialize());

        yield return StartCoroutine(base.Cor_Initialize());
    }

}
