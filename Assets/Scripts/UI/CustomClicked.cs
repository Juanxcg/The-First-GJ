using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CustomClicked : Image
{
    public override bool IsRaycastLocationValid(Vector2 screenPoint, Camera eventCamera) {
        return GetComponent<PolygonCollider2D>().OverlapPoint(screenPoint);
    }
}
