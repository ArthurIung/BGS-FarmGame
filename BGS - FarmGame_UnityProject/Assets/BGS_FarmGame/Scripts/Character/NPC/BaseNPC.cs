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

    [SerializeField] NPC_Schedule _nPCSchedule;
    public NPC_Schedule NPCSchedule
    {
        get
        {
            return _nPCSchedule;
        }
    }


    public override IEnumerator Cor_Initialize()
    {
        yield return StartCoroutine(_nPCInteraction.Cor_Initialize());
        yield return StartCoroutine(_nPCSchedule.Cor_Initialize());

        yield return StartCoroutine(base.Cor_Initialize());
    }





}
