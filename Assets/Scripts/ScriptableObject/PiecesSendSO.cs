using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Event/PiecesSendSO")]
public class PiecesSendSO : ScriptableObject
{
    public UnityAction<Pieces,bool > OnEventRaised;

    public void RaiseEvent(Pieces pieces,bool isCreat)
    {
        OnEventRaised?.Invoke(pieces,isCreat);
    }
}
