using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPiece : MonoBehaviour
{
    public bool moveNow;
    public bool isReady;
    public int numberOfStepsToMove;
    public int numberOfStepsAlreadyMove;
    public PathObjectParent pathParent;

    Coroutine MovePlayerPiece;

    public PathPoint previousPathPoint;
    public PathPoint currentPathPoint;
    private void Awake()
    {
        pathParent = FindObjectOfType<PathObjectParent>();
    }

    public void MoveSteps(PathPoint[] pathPointToMoveOn_)
    {
      MovePlayerPiece = StartCoroutine(MoveSteps_Enum(pathPointToMoveOn_));
    }

    public void MakeplayerReadyToMove(PathPoint[] pathPointToMoveOn_)
    {
        isReady = true;
        transform.position = pathPointToMoveOn_[0].transform.position;
        numberOfStepsAlreadyMove = 1;

        previousPathPoint = pathPointToMoveOn_[0];
        currentPathPoint = pathPointToMoveOn_[0];
        currentPathPoint.AddPlayerPice(this);
        GameManager.gm.AddPathPoint(currentPathPoint);

        GameManager.gm.canDiceRoll = true;
        GameManager.gm.selfDice = true;
        GameManager.gm.transferDice = false;
        if (GameManager.gm.sound) { GameManager.gm.ads.Play(); }// will be delete later ready sound code

    }


    // for dice 1 code will be delete
    //public void MakeplayerReadyToMovefor1(PathPoint[] pathPointToMoveOn_)
    //{
    //    isReady = true;
    //    transform.position = pathPointToMoveOn_[0].transform.position;
    //    numberOfStepsAlreadyMove = 1;

    //    previousPathPoint = pathPointToMoveOn_[0];
    //    currentPathPoint = pathPointToMoveOn_[0];
    //    currentPathPoint.AddPlayerPice(this);
    //    GameManager.gm.AddPathPoint(currentPathPoint);

    //    GameManager.gm.canDiceRoll = false;
    //    GameManager.gm.selfDice = false;
    //    GameManager.gm.transferDice = true;

    //}

    IEnumerator MoveSteps_Enum(PathPoint[] pathPointToMoveOn_)
    {
        GameManager.gm.transferDice = false;

        yield return new WaitForSeconds(0.25f);
        numberOfStepsToMove = GameManager.gm.numberOfStepsToMove;
            
        currentPathPoint.RescaleAndRepositonAllPlayerPiece();

        for (int i = numberOfStepsAlreadyMove; i < (numberOfStepsAlreadyMove + numberOfStepsToMove); i++)
        {

            if (isPathPointsAvailableToMove(numberOfStepsToMove, numberOfStepsAlreadyMove, pathPointToMoveOn_))
            {

                transform.position = pathPointToMoveOn_[i].transform.position;
                if (GameManager.gm.sound) { GameManager.gm.ads.Play(); }
                yield return new WaitForSeconds(0.25f);
            }
         }

        if (isPathPointsAvailableToMove(numberOfStepsToMove, numberOfStepsAlreadyMove, pathPointToMoveOn_))
        {
            numberOfStepsAlreadyMove += numberOfStepsToMove;

            GameManager.gm.RemovePathPoint(previousPathPoint); 

            previousPathPoint.RemovePlayerPice(this);

            currentPathPoint = pathPointToMoveOn_[numberOfStepsAlreadyMove - 1];

           if ( currentPathPoint.AddPlayerPice(this))
            {
                if (numberOfStepsAlreadyMove == 57)
                {
                    GameManager.gm.selfDice = true;
                }
                else
                {
                    if (GameManager.gm.numberOfStepsToMove != 6)
                    {
                        GameManager.gm.transferDice = true;
                    }
                    else
                    {
                        GameManager.gm.selfDice = true;

                    }
                }
            }
            else
            {
                GameManager.gm.selfDice = true;
            }
                
            GameManager.gm.AddPathPoint(currentPathPoint);
            previousPathPoint = currentPathPoint;

            

            GameManager.gm.numberOfStepsToMove = 0;


        }

        GameManager.gm.CanPlayerMove = true;
        GameManager.gm.RollingDiceManager();

        if (MovePlayerPiece != null)
        {
            StopCoroutine("MoveSteps_Enum");
        }
    }

    bool isPathPointsAvailableToMove(int numOfStepsToMove_ , int numOfStepsAlreadyMove , PathPoint [] pathPointsToMove_)
    {
        if (numOfStepsToMove_ == 0)
        {
            return false;
        }
        int leftNumOfPath = pathPointsToMove_.Length - numOfStepsAlreadyMove;

        if (leftNumOfPath >= numOfStepsToMove_)
        {
            return true;
        }
        else
        {
            return false;
         }
            
        
    }
}
