using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomM : MonoBehaviour
{
    public SearchPieces searchPieces;

    public Board board;
    public void Awake()
    {
        searchPieces = GetComponent<SearchPieces>();
        board = GameObject.FindGameObjectWithTag("Board").GetComponent<Board>();
    }

    public Vector2 ToGetPointO(int XMin,int XMax,int YMin,int YMax,bool state)
    {
        for (int i=0;i<50 ;i++ ) {
            int x = (int)Random.Range(XMin, XMax);
            int y = (int)Random.Range(YMin, YMax);
            if (board.board[9-y, x+9] == null && searchPieces.canMove(new Vector2(x, y),state))
            {
                return new Vector2(x, y);
            }
        }
        return new Vector2(-1, 0);
    }
    //state为true 白子的放置
    //state为false 黑子的放置
    public Vector2 ToGetPointO(Vector2 point1,Vector2 point2,bool state) 
    {
        int XMin= (int)(point1.x<=point2.x ? point1.x : point2.x);
        int XMax=(int)(point1.x>=point2.x ? point1.x :point2.x);
        int YMin=(int)(point1.y<=point2.y ? point1.y : point2.y);
        int YMax =(int)(point1.y>=point2.y ? point1.y :point2.y);
        for (int i = 0; i < 50; i++)
        {
            int x = (int)Random.Range(XMin, XMax);
            int y = (int)Random.Range(YMin, YMax);
            
            if (board.board[9-y, x+9] == null && searchPieces.canMove(new Vector2(x, y), state))
            {
                return new Vector2(x, y);
            }
        }
        return new Vector2(-1, 0);
    }

    public Vector2 ToGetRandom(bool state)
    {
        for (int i = 0; i < 50; i++)
        {
            int x = (int)Random.Range(-9, 9);
            int y = (int)Random.Range(-9, 9);
            if (board.board[9-y, 9+x] == null && searchPieces.canMove(new Vector2(x, y), state))
            {
                return new Vector2(x, y);
            }
        }
        return new Vector2(-1,0);
    }
    
    public Vector2 ToGet(int x1,int x2,bool state)//获得一块x1*x2的可以落state状态棋子的空地的左上角坐标
    {
        
        Vector2 v1 = new Vector2(-1, 0);
        for (int i = 0; i < 50; i++)
        {
            int bo = 1;
            Vector2 v2 = ToGetRandom(state);
            if (v2.x + x1 < 9 && v2.y - x2 > -9)
            {
                Debug.Log(v2.x + " " + v2.y);

                for (int j = (int)v2.x; j < v2.x + x1; j++)
                {
                    for (int k = (int)v2.y; k < v2.y - x2; k++)
                    {
                        if (board.board[9 - k, j + 9] != null || !searchPieces.canMove(new Vector2(j, k), state))
                        {
                            bo = 0;
                        }
                    }
                }
                if (bo == 1)
                    return v2;
            }
        }
        return v1;
    }
}
