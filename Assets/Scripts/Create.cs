using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Create : MonoBehaviour
{
    public RandomM randomM;
    public void OnEnable()
    {
        randomM = GetComponent<RandomM>();
    }
    public GameObject ToCreateWhite(Vector2 pos)
    {
        GameObject newRandom = GameObject.Instantiate(Resources.Load("baizi")) as GameObject;
        newRandom.GetComponent<Pieces>().MoveTo(new Vector2(pos.x +9, 9-pos.y));
        newRandom.name = "bai zi";
        newRandom.tag = "whiteChequer";
        return newRandom;
    }

    public GameObject ToCreateBlack(Vector2 pos)
    {
        GameObject newRandom = GameObject.Instantiate(Resources.Load("heizi")) as GameObject;
        newRandom.GetComponent<Pieces>().MoveTo(new Vector2(pos.x + 9, 9 - pos.y));
        newRandom.name = "hei zi";
        newRandom.tag = "blackChequer";
        return newRandom;
    }

    public GameObject ToCreateHead(Vector2 pos)
    {
        GameObject newRandom = GameObject.Instantiate(Resources.Load("baizi")) as GameObject;

        newRandom.GetComponent<Pieces>().MoveTo(new Vector2(pos.x + 9, 9 - pos.y));
        newRandom.AddComponent<Move>();
        newRandom.AddComponent<SearchPieces>();
        newRandom.AddComponent<Create>();
        newRandom.GetComponent<SpriteRenderer>().color = new Color32(220, 220, 220, 255);
        newRandom.AddComponent<Dragon>();
        newRandom.name = "chequerHead";
        newRandom.tag = "chequerHead";
        return newRandom;
    }

    public void GetRandomA(bool state)//随机生成单个棋子
    {
        if(!state)
        ToCreateBlack(randomM.ToGetRandom(state));
        else
            ToCreateWhite(randomM.ToGetRandom(state));
    }
    public void GetRandomB(int n)//随机在2*2的格子中生成n个黑子
    {
        if (n > 4 && n < 1)
            return;
        Vector2 ve = randomM.ToGet(2, 2, false);
        ToCreateBlack(ve);
        for (int i = 1; i < n; i++)
        {
            Vector2 vi = randomM.ToGetPointO(ve, new Vector2(ve.x + 2, ve.y - 2), false);
            ToCreateBlack(vi);
        }
    }
    public void GetRandomB()//随机在2*2的格子中生成随机个黑子
    {
        int n = (int)Random.Range(1, 4);
        Vector2 ve = randomM.ToGet(2, 2, false);
        ToCreateBlack(ve);
        for (int i = 1; i < n; i++)
        {
            Vector2 vi = randomM.ToGetPointO(ve, new Vector2(ve.x + 2, ve.y - 2), false);
            ToCreateBlack(vi);
        }
    }
    public void GetRandomC(int n)//随机在3*3的格子中生成n个黑子
    {
        if (n > 9 && n < 1)
            return;
        Vector2 ve = randomM.ToGet(3, 3, false);
        ToCreateBlack(ve);
        for (int i = 1; i < n; i++)
        {
            Vector2 vi = randomM.ToGetPointO(ve, new Vector2(ve.x + 3, ve.y - 3), false);
            ToCreateBlack(vi);
        }
    }
    public void GetRandomC()//随机在3*3的格子中生成随机个黑子
    {
        int n = (int)Random.Range(1, 10);
        Vector2 ve = randomM.ToGet(3, 3, false);
        ToCreateBlack(ve);
        for (int i = 1; i < n; i++)
        {
            Vector2 vi = randomM.ToGetPointO(ve, new Vector2(ve.x + 3, ve.y - 3), false);
            ToCreateBlack(vi);
        }
    }

    public void GetRandomC1()//随机在3*3的格子中生成特殊体（十字缺中心子）
    {
        Vector2 ve = randomM.ToGet(3, 3, false);
        Debug.Log(ve);
        ToCreateBlack(new Vector2(ve.x + 1, ve.y));
        ToCreateBlack(new Vector2(ve.x, ve.y - 1));
        ToCreateBlack(new Vector2(ve.x + 2, ve.y - 1));
        ToCreateBlack(new Vector2(ve.x + 1, ve.y - 2));
    }
}
