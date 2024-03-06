using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Event/PiecesMoveSO")]
public class PiecesMoveSO : ScriptableObject
{
    public UnityAction<Pieces, Vector2> OnEventRaised;

    public void RaiseEvent(Pieces pieces, Vector2 target)
    {
        OnEventRaised?.Invoke(pieces, target);
    }
}
