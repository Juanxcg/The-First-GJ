using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    public static Board Instance { get; private set; }
    
    //[Header("事件监听")]
    public PiecesSendSO GetPieces;
    public PiecesMoveSO MovePieces;
    public int whiteCount;
    public int blackCount;
    //棋盘
    public Point[,] board;

    public event EventHandler OnCapturedChess;

    private void Awake() {
        if (Instance == null) {
            Instance = this;
        } else {
            //Debug.LogError("Only exist one instance");
        }
    }

    private void OnEnable()
    {
       GetPieces.OnEventRaised += SetPiece;
       MovePieces.OnEventRaised += MovePiece;
        board = new Point[19, 19];
    }

    private void OnDisable()
    {
        GetPieces.OnEventRaised -= SetPiece;
    }
    public void SetPiece(Pieces pieces,bool iscreat)
    {
        if (pieces == null)
            return;
        int x = (int)pieces.pos.x;
        int y = (int)pieces.pos.y;
        if (iscreat)
        {
            board[y, x] = new Point(pieces);
            if (pieces.state == 1) {
                whiteCount++;
            }
            else
                blackCount++;
            //Debug.Log("creat");
            //Debug.Log(board[y,x].pieces.pos);
        }
        else
        {
            board[y, x] = null;
            if (pieces.state == 1)
            {
                whiteCount--;
                OnCapturedChess?.Invoke(this, EventArgs.Empty);
            }
            else
            {
                blackCount--;
                OnCapturedChess?.Invoke(this, EventArgs.Empty);
                //Debug.Log("destory");
            }
        }
    }
    private void MovePiece(Pieces pieces, Vector2 target)
    {
        if (pieces == null)
            return;
        int x = (int)pieces.pos.x;
        int y = (int)pieces.pos.y;
        board[y, x] = null;
        if(board[(int)target.y,(int)target.x]==null)
        board[(int)target.y, (int)target.x] = new Point(pieces);
        //Debug.Log(board[(int)target.y, (int)target.x].pieces.pos);
    }
    public void isNotChecked()
    {
        for(int i=0;i<19;i++)
        {
            for(int j=0;j<19;j++)
            {
                if (board[i, j] != null)
                    board[i, j].isChecked = false;
            }
        }
    }
}
