using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SearchPieces : MonoBehaviour
{
    
    public Board board;

    private Pieces current;
    private Pieces temp;
    private int x, y, x1, y1;
    private Queue queue;
    private Queue diePieces;
    private int count;
    private int num;
    private int dir;
    private bool flag;

    public static event EventHandler<OnAnyScoreIncreaseArgs> OnAnyScoreIncrease;
    public class OnAnyScoreIncreaseArgs
    {
        public int increasedScore;
    }

    private void Awake()
    {
        board = GameObject.FindGameObjectWithTag("Board").GetComponent<Board>();
    }
    private void Start()
    {
        queue = new Queue();
        diePieces = new Queue();
    }
    public int Search(Pieces head)
    {
        for (int i = 0; i < 4; i++)
        {
            x1 = (int)head.pos.x;
            y1 = (int)head.pos.y;
            switch (i)
            {
                case 0:
                    y1--;
                    break;
                case 1:
                    x1++;
                    break;
                case 2:
                    y1++;
                    break;
                case 3:
                    x1--;
                    break;
            }
            //如果为黑子
            if (y1 <= 18 && y1 >= 0 && x1 <= 18 && x1 >= 0&&board.board[y1, x1] != null && board.board[y1, x1].pieces.state == 2)
            {
                queue.clean();
                diePieces.clean();
                count = 0;
                board.isNotChecked();
                queue.EnQueue(board.board[y1, x1].pieces);
                //四周找子
                flag = true;
                while (!queue.IsEmpty())
                {
                    //放入死子栈
                    current = queue.GetFront();
                    queue.DeQueue();
                    diePieces.EnQueue(current);
                    count++;
                    for (dir = 0; dir < 4; dir++)//将黑子放入队列中，标记，如果有空位，则退出
                    {
                        x = (int)current.pos.x;
                        y = (int)current.pos.y;
                        switch (dir)
                        {
                            case 0:
                                y--;
                                break;
                            case 1:
                                x++;
                                break;
                            case 2:
                                y++;
                                break;
                            case 3:
                                x--;
                                break;
                        }
                        if (y <= 18 && y >= 0 && x <= 18 && x >= 0 && board.board[y, x] != null)
                            temp = board.board[y, x].pieces;
                        else if(y <= 18 && y >= 0 && x <= 18 && x >= 0 && board.board[y, x] == null )
                        {
                            flag = false;
                            break;
                        }
                        if ( y <= 18 && y >= 0 && x <= 18 && temp.state == 2 && x >= 0 && !board.board[y, x].isChecked)
                        {
                            queue.EnQueue(temp);
                            board.board[y, x].isChecked = true;
                        }
                    }
                    if (flag == false)
                        break;
                }
                if (flag == true)//删除子，计数
                {
                    num += count;
                    while (!diePieces.IsEmpty())
                    {
                        current = diePieces.GetFront();
                        Debug.Log(current ==null);
                        diePieces.DeQueue();
                        Destroy(current.gameObject);
                    }
                }
            }
        }

        if (num != 0) {
            OnAnyScoreIncrease?.Invoke(this, new OnAnyScoreIncreaseArgs {
                increasedScore = num
            });
        }
        
        return num;
    }

    //flag为true 白子的放置
    //flag为false 黑子的放置
    public bool canMove(Vector2 position, bool flag,Pieces head = null)
    {
        int state1;
        int state2;
        if (flag == true)
        {
            state1 = 1;
            state2 = 0;
        }
        else
            state1 = state2 = 2;
        int x = (int)position.x + 9;
        int y = -(int)position.y + 9;
        bool f = false;
        board.isNotChecked();
        queue.clean();
        while (!queue.IsEmpty() || !f)
        {
            if (f == false)
                f = true;
            else
            {
                current = queue.GetFront();
                queue.DeQueue();
                x = (int)current.pos.x;
                y = (int)current.pos.y;
            }
            for (dir = 0; dir < 4; dir++)
            {
                x1 = x;
                y1 = y;
                switch (dir)
                {
                    case 0:
                        y1--;
                        break;
                    case 1:
                        x1++;
                        break;
                    case 2:
                        y1++;
                        break;
                    case 3:
                        x1--;
                        break;
                }
                if (x1 == (int)position.x + 9 && y1 == -(int)position.y + 9)
                    continue;
                if (y1 <= 18 && y1 >= 0 && x1 <= 18 && x1 >= 0 && board.board[y1, x1] != null)
                    temp = board.board[y1, x1].pieces;
                else if (y1 <= 18 && y1 >= 0 && x1 <= 18 && x1 >= 0 && board.board[y1, x1] == null)
                    return true;
                //在边界内，未找过，点非空，是白子
                if (y1 <= 18 && y1 >= 0 && x1 <= 18 && x1 >= 0 && !board.board[y1, x1].isChecked && (temp.state == state1 || temp.state == state2))
                {
                    queue.EnQueue(temp);
                    board.board[y1, x1].isChecked = true;
                }
                
            }
        }
        //到了这步说明同色子被异色子包围，判断外圈异色子是否无气
        if (flag == false)
        {
            state1 = 1;
            state2 = 0;
        }
        else
            state1 = state2 = 2;
        //Debug.Log("state2");
        Vector2 tailPosition = new Vector2(-1, -1);
        if (head != null&&head.gameObject.GetComponent<Dragon>())
        {
            Pieces tail = head.gameObject.GetComponent<Dragon>().queue.array[head.gameObject.GetComponent<Dragon>().queue.rear - 1];
            if (tail != head)
                tailPosition = tail.pos;
        }
        Debug.Log(tailPosition);
        for (int i = 0; i < 4; i++)
        {
            x1 = (int)position.x + 9; ;
            y1 = -(int)position.y + 9;
            switch (i)
            {
                case 0:
                    y1--;
                    break;
                case 1:
                    x1++;
                    break;
                case 2:
                    y1++;
                    break;
                case 3:
                    x1--;
                    break;
            }
            //如果为异色子
            if (y1 <= 18 && y1 >= 0 && x1 <= 18 && x1 >= 0 && board.board[y1, x1] != null && (board.board[y1, x1].pieces.state == state1 || board.board[y1, x1].pieces.state == state2))
            {
                //Debug.Log(new Vector2(x1, y1));
                queue.clean();
                board.isNotChecked();
                queue.EnQueue(board.board[y1, x1].pieces);
                flag = true;
                //四周找子
                while (!queue.IsEmpty())
                {
                    current = queue.GetFront();
                    queue.DeQueue();
                    for (dir = 0; dir < 4; dir++)//将黑子放入队列中，标记，如果有空位，则退出
                    {
                        x = (int)current.pos.x;
                        y = (int)current.pos.y;
                        switch (dir)
                        {
                            case 0:
                                y--;
                                break;
                            case 1:
                                x++;
                                break;
                            case 2:
                                y++;
                                break;
                            case 3:
                                x--;
                                break;
                        }
                        if (y == tailPosition.y && x == tailPosition.x)
                        {
                            flag = false;
                            break;
                        }
                        if (x == (int)position.x + 9 && y == -(int)position.y + 9)
                            continue;
                        if (y <= 18 && y >= 0 && x <= 18 && x >= 0 && board.board[y, x] != null)
                            temp = board.board[y, x].pieces;
                        else if (y <= 18 && y >= 0 && x <= 18 && x >= 0 && board.board[y, x] == null)
                        {
                            flag = false;
                            break;
                        }  
                        if (y <= 18 && y >= 0 && x <= 18 && x >= 0 && (temp.state == state1 || temp.state == state2) && !board.board[y, x].isChecked)
                        {
                            queue.EnQueue(temp);
                            board.board[y, x].isChecked = true;
                        }
                    }
                    if (flag == false)
                        break;
                }
                if (flag == true)
                    return true;
            }
        }
        return false;
    }
}