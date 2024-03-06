using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PiecesShow : MonoBehaviour
{
    bool isPresent;
    public SpriteRenderer spriteRenderer;

    private void OnEnable()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.color = new Color32(0, 230, 250, 255);
    }
    private void Update()
    {
        if (isPresent)
        {
            transform.localScale = new Vector3(1.5f, 1.5f, 1);
            spriteRenderer.color = new Color32(255, 255, 255, 200);
        }
            
        else
        {
            transform.localScale = new Vector3(0.8f, 0.8f, 1);
            spriteRenderer.color = new Color32(0, 230, 250,255);
        }
            

    }
    private void OnMouseEnter()
    {
        isPresent = true;
    }
    private void OnMouseExit()
    {
        isPresent = false;
    }
}
