using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenPlayerPiece : PlayerPiece
{
    RollingDice greenHomeRollingDice;
    void Start()
    {

        greenHomeRollingDice = GetComponentInParent<GreenHome>().rollingDice;

    }
    public void OnMouseDown()
    {
        if (GameManager.gm.rollingDice != null)
        {


            if (!isReady)
            {
                if (GameManager.gm.rollingDice == greenHomeRollingDice && (GameManager.gm.numberOfStepsToMove == 6 /*|| GameManager.gm.numberOfStepsToMove == 1*/ ))
                {
                    GameManager.gm.greenOutPlayers += 1;
                    MakeplayerReadyToMove(pathParent.GreenPathPoint);
                    GameManager.gm.numberOfStepsToMove = 0;
                    return;

                }// else if code will delete later
                //else if (GameManager.gm.rollingDice == greenHomeRollingDice && GameManager.gm.numberOfStepsToMove == 1)
                //{
                //    GameManager.gm.greenOutPlayers += 1;
                //    MakeplayerReadyToMovefor1(pathParent.GreenPathPoint);
                //    GameManager.gm.numberOfStepsToMove = 0;
                //    return;

                //}
            }

        }

        if (GameManager.gm.rollingDice == greenHomeRollingDice && isReady && GameManager.gm.CanPlayerMove)
        {
            GameManager.gm.CanPlayerMove = false;
            MoveSteps(pathParent.GreenPathPoint);

        }
    }
}