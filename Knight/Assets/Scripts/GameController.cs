using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public static GameController Instance;
    public Transform Topanchor;
    public Transform BottomAnchor;
    public int N;
    public Knight knight;
    public GameObject Pawn;
    public InputField inputX;
    public InputField inputY;
    public GameObject panel;
    public bool gamestart = false;

    int[] pawposition;
    float CellWidth;
    // Start is called before the first frame update
    void Start()
    {
        pawposition = new int[2] { 0, 0};        
        CellWidth = Mathf.Abs(Topanchor.position.z - BottomAnchor.position.z) / (N-1);
        GameController.Instance = this;
        SetPawn(pawposition[0], pawposition[1]);
    }
    public Vector3 GetPosition(int x, int y)
    {
        Debug.Log(BottomAnchor.position.y);
        return new Vector3(BottomAnchor.position.x + x * CellWidth, 0, BottomAnchor.position.z - y * CellWidth);
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if(IsInside(pawposition[0], pawposition[1] + 1, N))
            {
                SetPawn(pawposition[0], pawposition[1] + 1);
            }
        }
        else if(Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (IsInside(pawposition[0], pawposition[1] - 1, N))
            {
                SetPawn(pawposition[0], pawposition[1] - 1);
            }
        }else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (IsInside(pawposition[0]+1, pawposition[1], N))
            {
                SetPawn(pawposition[0]+1, pawposition[1]);
            }
        }else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (IsInside(pawposition[0] - 1, pawposition[1], N))
            {
                SetPawn(pawposition[0] - 1, pawposition[1]);
            }
        }
    }

    public void SetPawn(int x, int y)
    {
        pawposition[0] = x;
        pawposition[1] = y;
        Pawn.transform.position = GetPosition(x, y);
        knight.targetpos[0] = x;
        knight.targetpos[1] = y;
    }
    public bool IsInside(int x, int y, int N)
    {
        if (x >= 0 && x < N && y >= 0 && y < N)
        {
            return true;
        }
        return false;
    }

    public void Play()
    {
        SetPawn(int.Parse(inputX.text), int.Parse(inputY.text));
        Pawn.SetActive(true);
        knight.ReturnPos();
        gamestart = true;        
    }

    public void eat()
    {
        Pawn.SetActive(false);
    }
}
