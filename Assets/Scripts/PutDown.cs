using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class PutDown : MonoBehaviour
{
    public static PutDown Instance { get; private set; }
    
    public Vector2 mousePos;
    GameObject gameObject2;
    GameObject gameObject1;
    public Board board;
    public SearchPieces searchPieces;

    public event EventHandler OnCurrentRoundEnd;

    private void Awake()
    {
        if (Instance == null) {
            Instance = this;
        } else {
            Debug.LogError("Only exist one instance");
        }
        
        searchPieces = GetComponent<SearchPieces>();
        board = GameObject.FindGameObjectWithTag("Board").GetComponent<Board>();
    }

    public void putDownItem(Action cb = null)
    {
        if (GameObject.FindGameObjectWithTag("Putdown Chequer") == null)
        {
            gameObject2 = GameObject.Instantiate(Resources.Load("newChequer Variant")) as GameObject;
            gameObject2.tag = "Putdown Chequer";
            gameObject1 = GameObject.Instantiate(Resources.Load("baizi")) as GameObject;
        }
        if (gameObject2 != null)
        {
            mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (isRoad(new Vector2(Convert.ToInt32(mousePos.x), Convert.ToInt32(mousePos.y))))
            {
                gameObject1.GetComponent<Transform>().position = new Vector3((float)mousePos.x, (float)mousePos.y, 1);
                gameObject2.GetComponent<Transform>().position = new Vector3(Convert.ToInt32(mousePos.x), Convert.ToInt32(mousePos.y), 1);

                if (Input.GetMouseButtonDown(0))
                {
                    gameObject1.GetComponent<Pieces>().MoveTo(new Vector2(gameObject2.transform.position.x + 9, 9 - gameObject2.transform.position.y));
                    Destroy(gameObject2);
                    cb?.Invoke();
                    OnCurrentRoundEnd?.Invoke(this, EventArgs.Empty);
                    searchPieces.Search(gameObject1.GetComponent<Pieces>());
                }
            }
        }
    }
    public bool isRoad(Vector2 position)//判断能不能移动到指定位置
    {
        if (-(int)position.y + 9 >= 0 && -(int)position.y + 9 < 19 && (int)position.x + 9 >= 0 && (int)position.x + 9 < 19)
        {
            if (board.board[-(int)position.y + 9, (int)position.x + 9] == null && searchPieces.canMove(position, true))
            {
                return true;
            }
        }
        return false;
    }

}
