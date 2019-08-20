using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knight : MonoBehaviour
{
    public int N;
    public int[] knightpos;
    public int[] targetpos;

    
    float lastmove;
    // Start is called before the first frame update
    void Start()
    {
        N = GameController.Instance.N;
        transform.position = GameController.Instance.GetPosition(7, 7);
        knightpos = new int[2] { 7, 7 };
    }
    

    void Move(int[] knightpos, int[] targetpos, int N)
    {
        int[] dx = { -2, -1, 1, 2, -2, -1, 1, 2 };
        int[] dy = { -1, -2, -2, -1, 1, 2, 2, 1 };
        List<Cell> q = new List<Cell>();
        q.Add(new Cell(knightpos[0], knightpos[1], 0, null));
        Cell a;
        int x;
        int y;
        bool[,] visit = new bool[N,N];
        for(int i = 0; i < N-1; i++)
        {
            for(int j = 0; j < N-1; j++)
            {
                visit[i, j] = false;
            }
        }
        visit[knightpos[0], knightpos[1]] = true;

        while(q.Count > 0)
        {
            a = q[0];
            q.RemoveAt(0);
            if (a.x == targetpos[0] && a.y == targetpos[1])
            {
                transform.position = GameController.Instance.GetPosition(a.ori.x, a.ori.y);
                knightpos[0] = a.ori.x;
                knightpos[1] = a.ori.y;
                return;
            }
            for(int i = 0; i < 7; i++)
            {
                x = a.x + dx[i];
                y = a.y + dy[i];
                if(GameController.Instance.IsInside(x, y, N) && !visit[x, y])
                {
                    visit[x, y] = true;

                    Cell c = new Cell(x, y, a.dis + 1, null);
                    if(a.ori == null)
                    {
                        c.ori = c;
                    }
                    else
                    {
                        c.ori = a.ori;
                    }
                    q.Add(c);
                }
            }    
        }
    }

    

    // Update is called once per frame
    void Update()
    {
        if (GameController.Instance.gamestart && Time.time - lastmove >= 2)
        {
            if (knightpos[0] != targetpos[0] || knightpos[1] != targetpos[1])
            {
                Move(knightpos, targetpos, 8);
                lastmove = Time.time;
            }
            else
            {
                GameController.Instance.gamestart = false;
                GameController.Instance.panel.SetActive(true);
                GameController.Instance.eat();
            }
                
        }       
    }

    public void ReturnPos()
    {
        transform.position = GameController.Instance.GetPosition(7, 7);
        lastmove = Time.time;
        knightpos = new int[2] { 7, 7 };
    }
}

public class Cell
{
    public int x;
    public int y;
    public int dis;
    public Cell ori;
    public Cell (int x, int y, int dis, Cell ori)
    {
        this.x = x;
        this.y = y;
        this.dis = dis;
        this.ori = ori;
    }

    
}