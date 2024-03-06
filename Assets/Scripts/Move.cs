using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Move : MonoBehaviour
{
    public static Move Instance { get; private set; }
    
    private Board board;
    private SearchPieces searchPieces;
    public Vector2 previousPosition;
    public bool canEat;
    public bool canMove;
    private Pieces head;

    public event EventHandler OnCurrentRoundEnd;

    private void Awake() {
        if (Instance == null) {
            Instance = this;
        } else {
            //Debug.LogError("Only exist one instance");
        }
        
    }

    private void Start()
    {
        board = GameObject.FindGameObjectWithTag("Board").GetComponent<Board>();
        searchPieces = GetComponent<SearchPieces>();
        Debug.Log("board in start " + (board == null));
        canMove = true;
    }


    public bool isRoad(Vector2 position)//判断能不能移动到指定位置
    {
        head = GameObject.FindGameObjectWithTag("chequerHead").GetComponent<Pieces>();
        Debug.Log(head == null);
            int x = (int)position.x + 9;
            int y = -(int)position.y + 9;
            if (y >= 0 && y < 19 && x >= 0 && x < 19)
            {
                Debug.Log("board in isRoad " + (board == null));
                if (board.board[y, x] == null && searchPieces.canMove(position, true, head) || board.board[y, x] != null && board.board[y, x].pieces.state == 1)
                    if (board.board[y, x] == null && searchPieces.canMove(position, true) || board.board[y, x] != null && board.board[y, x].pieces.state == 1)

                        if (board.board[y, x] == null && searchPieces.canMove(position, true) || board.board[y, x] != null && board.board[y, x].pieces.state == 1)
                            return true;
            }
            return false;

    }

    public void createNewRoad(Vector2 newRoad)//提醒玩家棋龙的下一步走向
    {
        GameObject newChequer = GameObject.Instantiate(Resources.Load("newChequer Variant")) as GameObject;
        newChequer.transform.position = newRoad;
    }


    public bool move(Pieces head)//移动的实现
    {
        bool flag = false;
        //查找是不是已经创造了提醒位置
        GameObject[] gameObjects = GameObject.FindGameObjectsWithTag("newChequer");
        Vector2 nowPosition = transform.position;
        if (gameObjects.Length == 0)
        {
            for (int i = -1; i < 2; i++)//判断周围的落子点能不能移动
            {
                for (int j = -1; j < 2; j++)
                {
                    if (i == 0 && j == 0)
                        continue;
                    Vector2 newPosition = new Vector2(nowPosition.x + i, nowPosition.y + j);
                    if (isRoad(newPosition))
                    {
                        createNewRoad(newPosition);
                    }
                }
            }
            
        }
        //移动棋子
        if (Input.GetMouseButtonDown(0))
        {
            GameObject[] gameObjects2 = GameObject.FindGameObjectsWithTag("newChequer");
            if (gameObjects.Length == 0)
                return canMove = false;
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);//移动棋子到指定位置
            if (hit.collider != null && hit.collider.tag == "newChequer")
            {
                previousPosition = nowPosition;
                nowPosition = hit.collider.transform.position;
                int x = (int)nowPosition.x + 9;
                int y = -(int)nowPosition.y + 9;
                //吃子
                if (board.board[y, x] != null)
                {
                    Destroy(board.board[y, x].pieces.gameObject);
                    board.board[y, x] = null;
                    canEat = true;
                 }
                //移动棋子
                head.MoveTo(new Vector2((int)nowPosition.x + 9, -(int)nowPosition.y + 9));
                flag = true;
                //将棋子移动位置提醒移除
                if (gameObjects.Length > 0)
                {
                    foreach (GameObject gameObject in gameObjects)
                    {
                        Destroy(gameObject);
                    }
                }
            }
            
            OnCurrentRoundEnd?.Invoke(this, EventArgs.Empty);
         }
        
        return flag;
     }
}
