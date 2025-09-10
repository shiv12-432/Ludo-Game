using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YellowPlayerPiece : PlayerPiece
{

    RollingDice yellowHomeRollingDice;
    void Start()
    {

        yellowHomeRollingDice = GetComponentInParent<YellowHome>().rollingDice;

    }
    public void OnMouseDown()
    {
        if (GameManager.gm.rollingDice != null)
        {


            if (!isReady)
            {
                if (GameManager.gm.rollingDice == yellowHomeRollingDice && (GameManager.gm.numberOfStepsToMove == 6 /*|| GameManager.gm.numberOfStepsToMove == 1 */))
                {
                    GameManager.gm.yellowOutPlayers += 1;
                    MakeplayerReadyToMove(pathParent.YellowPathPoint);
                    GameManager.gm.numberOfStepsToMove = 0;
                    return;

                }
            }

        }

        if (GameManager.gm.rollingDice == yellowHomeRollingDice && isReady && GameManager.gm.CanPlayerMove)
        {
            GameManager.gm.CanPlayerMove = false;
            MoveSteps(pathParent.YellowPathPoint);

        }
    }
}
