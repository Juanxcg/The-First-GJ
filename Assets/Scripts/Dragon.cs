using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Dragon : MonoBehaviour
{
    public SearchPieces searchPieces;
    public Board board;
    public Pieces head;
    private Move dragonMove;
    private Create create;
    public Queue queue;
    public int currentLenth;
    private bool isMoving;
    public bool canMove;

     private void Start()
    {
        board = GameObject.FindGameObjectWithTag("Board").GetComponent<Board>();
        create = GetComponent<Create>();
        searchPieces = GetComponent<SearchPieces>();
        dragonMove = GetComponent<Move>();
        head = GetComponent<Pieces>();
        currentLenth = 1;
        queue = new Queue();
        //Debug.Log(head.pos.x);
        queue.EnQueue(head);

    }
    private void Awake()
    {
        
    }
    private void Update()
    {
        isMoving = head.isMoving;
    }
    /// <summary>
    /// 用作移动一起
    /// </summary>
    public bool Move(Action cb = null)
    {
        dragonMove = GetComponent<Move>();
        if (isMoving == false && head != null)
        {
            if (dragonMove.move(head))
            {
                Vector2 pos = dragonMove.previousPosition;
                pos = new Vector2((int)pos.x + 9, -(int)pos.y + 9);
                int front = queue.front + 1;
                int rear = queue.rear;
                Pieces current;
                for (; front < rear; front++)
                {
                    Vector2 currentPos = pos;
                    current = queue.array[front];
                    pos = current.pos;
                    current.MoveTo(currentPos);
                }
                if (dragonMove.canEat)
                {
                    AddPiece(pos);
                    dragonMove.canEat = false;
                }

                searchPieces.Search(head);
                cb?.Invoke();
            }
            if (!dragonMove.canMove)
            {
                return true;
            }
        }
        
        return false;
    }

    public void AddPiece(Vector2 pos)
    {
        Pieces newPiece = create.ToCreateWhite(new Vector2(pos.x-9,9-pos.y)).GetComponent<Pieces>();
        newPiece.state = 0;
        queue.EnQueue(newPiece);
        currentLenth++;
    }
}
