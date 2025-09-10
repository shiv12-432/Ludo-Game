using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager gm;
    public RollingDice rollingDice;
    public int numberOfStepsToMove;
    public bool CanPlayerMove = true;
    
    List<PathPoint> playerOnPathPointList = new List<PathPoint>();

    public bool canDiceRoll = true;
    public bool transferDice = false;
    public bool selfDice = false;

    public int blueOutPlayers;
    public int redOutPlayers;
    public int greenOutPlayers;
    public int yellowOutPlayers;

    public int blueCompletePlayers;
    public int redCompletePlayers;
    public int greenCompletePlayers;
    public int yellowCompletPlayers;

    public RollingDice[] manageRollingDice;

    public PlayerPiece[] bluePlayerPice;
    public PlayerPiece[] redPlayerPice;
    public PlayerPiece[] greenPlayerPice;
    public PlayerPiece[] yellowPlayerPice;

    public int totalPlayerCanPlay;

    public int totalSix = 0;

    public AudioSource ads;
    public AudioSource DiceKillSound;
    public AudioSource playerPieceWinSound;
    public AudioSource completeWinSound;
    public bool sound = true;



    public void Awake()
    {
        gm = this;
        ads = GetComponent<AudioSource>();
    }

    public void AddPathPoint(PathPoint pathPoint)
    {
        playerOnPathPointList.Add(pathPoint);
    }

    public void RemovePathPoint(PathPoint pathPoint)
    {
        if (playerOnPathPointList.Contains(pathPoint))
        {
            playerOnPathPointList.Remove(pathPoint);
        }
        else
        {
            Debug.Log("Do not found path point to be removed");
        }
    }

    public void RollingDiceManager()
    {
         
        if (GameManager.gm.transferDice)
        {
            if (GameManager.gm.numberOfStepsToMove != 6)
            {
            
                siftDice();

            }            
            GameManager.gm.canDiceRoll = true;
        }
        else
        {
            if (GameManager.gm.selfDice)
            {
                GameManager.gm.selfDice = false;
                GameManager.gm.canDiceRoll = true;
                GameManager.gm.SelfRoal();
            }
        }
    }
    

    public void SelfRoal()
    {
        if(GameManager.gm.totalPlayerCanPlay == 1 && GameManager.gm.rollingDice == GameManager.gm.manageRollingDice[2])
        {
            Invoke("roled",0.6f);
        }
    }

    void roled()
    {
        GameManager.gm.manageRollingDice[2].mouseRoll();
    }

    void siftDice()
    {
        int nextdice;

        if (GameManager.gm.totalPlayerCanPlay == 1)
        {
            if (GameManager.gm.rollingDice == GameManager.gm.manageRollingDice[0])
            {
                GameManager.gm.manageRollingDice[0].gameObject.SetActive(false);
                GameManager.gm.manageRollingDice[2].gameObject.SetActive(true);

                passout(0);

                GameManager.gm.manageRollingDice[2].mouseRoll();
            }
            else
            {
                GameManager.gm.manageRollingDice[0].gameObject.SetActive(true);
                GameManager.gm.manageRollingDice[2].gameObject.SetActive(false);
                passout(2);
            }
        }
        else if (GameManager.gm.totalPlayerCanPlay == 2)
        {
            if (GameManager.gm.rollingDice == GameManager.gm.manageRollingDice[0])
            {
                GameManager.gm.manageRollingDice[0].gameObject.SetActive(false);
                GameManager.gm.manageRollingDice[2].gameObject.SetActive(true);
                passout(0);
            }
            else
            {
                GameManager.gm.manageRollingDice[0].gameObject.SetActive(true);
                GameManager.gm.manageRollingDice[2].gameObject.SetActive(false);
                passout(2);
            }
        }
        else if (GameManager.gm.totalPlayerCanPlay == 3)
        {
            for (int i = 0; i < 3; i++)
            {
                if (i == 2)
                {
                    nextdice = 0;
                }
                else
                {
                    nextdice = i + 1;
                }

                i = passout(i);

                if (GameManager.gm.rollingDice == GameManager.gm.manageRollingDice[i])
                {
                    GameManager.gm.manageRollingDice[i].gameObject.SetActive(false);
                    GameManager.gm.manageRollingDice[nextdice].gameObject.SetActive(true);

                }
            }
        }
        else
        {
           for (int i = 0; i < 4; i++)
            {
               if (i == 3)
               {
                    nextdice = 0;
                }
                else
               {
                    nextdice = i + 1;
                }

               i = passout(i);

                if (GameManager.gm.rollingDice == GameManager.gm.manageRollingDice[i] )
               {
                    GameManager.gm.manageRollingDice[i].gameObject.SetActive(false);
                    GameManager.gm.manageRollingDice[nextdice].gameObject.SetActive(true);

                }
            }
        }

    }


    int passout(int i)
    {
        if (i == 0) { if (GameManager.gm.blueCompletePlayers == 4) { return (i + 1); } }
        else if (i == 1) { if (GameManager.gm.redCompletePlayers == 4) { return (i + 1); } }
        else if (i == 2) { if (GameManager.gm.greenCompletePlayers == 4) { return (i + 1); } }
        else if (i == 3) { if (GameManager.gm.yellowCompletPlayers == 4) { return (i + 1); } }
        return i;
    }
}
