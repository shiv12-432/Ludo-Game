using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BluePlayerPiece : PlayerPiece
{
    RollingDice blueHomeRollingDice;
     void Start()
    {

        blueHomeRollingDice = GetComponentInParent<BlueHome>().rollingDice;
       
    }

    public void OnMouseDown()
    {
        if (GameManager.gm.rollingDice != null)
        {

       
            if (!isReady)
            {
                if (GameManager.gm.rollingDice == blueHomeRollingDice && (GameManager.gm.numberOfStepsToMove == 6 /*|| GameManager.gm.numberOfStepsToMove == 1 */ ))
                {
                    GameManager.gm.blueOutPlayers += 1;
                    MakeplayerReadyToMove(pathParent.BluePathPoint);
                    GameManager.gm.numberOfStepsToMove = 0;
                   return;

                } // else if  code will be delete after test
               //else if (GameManager.gm.rollingDice == blueHomeRollingDice && GameManager.gm.numberOfStepsToMove == 1 )
               // {
               //     GameManager.gm.blueOutPlayers += 1;
               //     MakeplayerReadyToMovefor1(pathParent.BluePathPoint);
               //     GameManager.gm.numberOfStepsToMove = 0;
               //     return;

               // }
            }
                        
        }

        if (GameManager.gm.rollingDice == blueHomeRollingDice && isReady && GameManager.gm.CanPlayerMove)
        {
            GameManager.gm.CanPlayerMove = false;
            MoveSteps(pathParent.BluePathPoint);

        }



    }



}
