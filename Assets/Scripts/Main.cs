using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
    基本流程：开始游戏之后每回合选择落子和移动，如果没有吃过黑子就只能移动，如果九个方向都不能移动或者回合数到达一定数值结束游戏？ 
    （这个还没确定，ui那边还没搞定）
    
    逻辑有问题，现在的设计是每回合通过点击按钮来切换落子和移动，移动的时候左键预计落子？然后点确定才是落子？
    
    如果非要鼠标落子的话，可以设置一个快捷键，来终止棋子移动
    
    优化：
        方案1：鼠标移动控制棋子移动与鼠标左键落子，每次落子的时候刷新棋盘，用不到按钮了
        方案2：键盘wasd移动，空格落子，也是用不到按钮
    
    基本的流程就是这个了，你得注意脚本引用了啥，你这个耦合度太高了，基本都挂在一个对象上，所以引发的bug可能会难以查找
    现在就是缺少游戏结束条件，棋盘刷新逻辑，都可以在RefreshChess()函数里写

 */

public enum State{
    Move,
    Down,
    GameOver
}

public class Main : MonoBehaviour
{
    [HideInInspector] public PutDown putDown;
    [HideInInspector] public Create create;
    State isResetChess = State.Move;

    private int roundnumber = 1;
    public GameObject head;

    private bool isSkillTrigged = false;

    private bool isGameover = false;

    private int putDownCd = 5;
    private int putDownRound = 5;
    
    // Start is called before the first frame update
    void Start()
    {
        create = GetComponent<Create>();
        putDown = GetComponent<PutDown>();
        head = create.ToCreateHead(new Vector2(0,0));
        create.GetRandomC1();
        
        SkillUI.Instance.OnSkillButtonTrigger += Main_OnSkillButtonTrigger;
    }

    private void Main_OnSkillButtonTrigger(object sender, SkillUI.OnSkillButtonTriggerArgs e) {
        isSkillTrigged = e.isTrigger;
    }

    void Update()
    {
        //按A键切换到移动
        if (Input.GetKeyDown(KeyCode.A))
        {
            OnClickMove();
        }
        //按D键实现落子
        if (Input.GetKeyDown(KeyCode.D) || isSkillTrigged) {
            isSkillTrigged = false;
            OnClickPut();
        }

        if (isResetChess == State.GameOver)
        {
            Application.Quit();//这里我直接写终止程序了，可以退回开始游戏主界面
        }
        //用作移动棋子
        if(isResetChess == State.Move)
        {
            if(head == null)
            head = GameObject.FindGameObjectWithTag("chequerHead");
            //预落子之后，直接落子
            head.GetComponent<Pieces>().state = 0;
            Debug.Log("head in Main: " + (head == null));
            isGameover = head.GetComponent<Dragon>().Move(() => { RefreshChess(); });
        }
        //点击按钮后落子,下一轮开始
        if(isResetChess == State.Down)
        {
            if (putDownRound >= putDownCd)
            {
                GameObject[] gameObjects = GameObject.FindGameObjectsWithTag("newChequer");
                if (gameObjects.Length > 0)
                {
                    foreach (GameObject gameObject in gameObjects)
                    {
                        Destroy(gameObject);
                    }
                }
                putDown.putDownItem(() => { RefreshChess(); });
            }
            else
            {
                OnClickMove();
            }
        }
        
    }

    /// <summary>
    /// 用于落子后刷新
    /// </summary>
    public void RefreshChess()
    {
        if(isResetChess == State.Move)
        {
            putDownRound++;
        }
        if(isResetChess == State.Down)
        {
            putDownRound = 0;
        }

        //重置状态为move
        isResetChess = State.Move;
        roundnumber++;
        if(roundnumber < 5)
        {
            create.GetRandomA(true);
            create.GetRandomA(false);
        }

        if(roundnumber<100&& roundnumber % 15 == 0)
        {
            create.GetRandomB();
        }

        if(roundnumber>=100&&roundnumber<200&&roundnumber%15 == 0)
        {
            create.GetRandomC();
        }

        if(roundnumber>200&&roundnumber%10 == 0)
        {
            create.GetRandomC();
        }
        if(roundnumber% 50 == 0)
        {
            create.GetRandomC1();
        }
        
        if(isGameover||roundnumber >500) { isResetChess = State.GameOver; }
    }

    public void OnClickMove()
    {
        isResetChess = State.Move;
        //其他逻辑
    }
    public void OnClickPut()
    {
        isResetChess = State.Down;
        //其他逻辑
    }

    
}
