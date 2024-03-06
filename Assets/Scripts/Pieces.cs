using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

public class Pieces : MonoBehaviour
{
    public int state;
    public Vector2 pos;
    public UnityEvent<Pieces,bool> PiecesCreatEvent;
    public UnityEvent<Pieces,bool> PiecesDestoryEvent;
    public UnityEvent<Pieces, Vector2> PiecesMoveEvent;
    public bool isMoving;
    private void OnEnable()
    {
        PiecesCreatEvent?.Invoke(GetComponent<Pieces>(),true);
        GoToPosition();
        //Debug.Log(GetComponent<Pieces>().state);
    }
    

    private void OnDestroy()
    {
        PiecesDestoryEvent?.Invoke(GetComponent<Pieces>(),false);
    }
    public void MoveTo(Vector2 target)
    {
        //TODO:事件呼叫移动事件
        PiecesMoveEvent?.Invoke(GetComponent<Pieces>(), target);
        pos = new Vector2((int)target.x,(int)target.y);
        if(state == 0)
        {
            isMoving = true;
            StartCoroutine(TriggerMove());
        }
        else
            GoToPosition();
        //Debug.Log(transform.position);
    }
    private void GoToPosition()
    {
        transform.position = new Vector3((int)pos.x - 9, -(int)pos.y + 9, 0);
    }
    private IEnumerator TriggerMove()//二维坐标
    {
        
        Vector2 position = new Vector2(pos.x - 9, 9 - pos.y);
        do
        {
            yield return null;
            float dirX;
            float dirY;
            if (Mathf.Abs(position.x - transform.position.x) >= 0.001f)
                dirX = ((position.x - transform.position.x) > 0) ? 1 : -1;
            else
                dirX = 0f;
            if (Mathf.Abs(position.y - transform.position.y) >= 0.001f)
                dirY = ((position.y - transform.position.y) > 0) ? 1 : -1;
            else
                dirY = 0f;
            if (dirX == 0 && dirY == 0)
                break;
            transform.position = new Vector3(transform.position.x + dirX*Time.deltaTime*2, transform.position.y + dirY*Time.deltaTime*2, 0);
        } while (true);
        GoToPosition();
        isMoving = false;
    }
}
