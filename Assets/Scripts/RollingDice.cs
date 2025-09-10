using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollingDice : MonoBehaviour
{

    [SerializeField] Sprite[] numberSprits;
    [SerializeField] SpriteRenderer numberSpriteHolder;
    [SerializeField] SpriteRenderer rollingDiceAnm;
    [SerializeField] int numberGot;

    Coroutine generateRandomNumberOnDice;
    public int outPieces;

    public PathObjectParent pathParent;
    PlayerPiece[] currentPlayerPieces;
    PathPoint[] pathPointToMoveOn_;
    Coroutine MovePlayerPiece;
    PlayerPiece outPlayerPiece;
    public Dice DiceSound;
   // int maxNum = 6; to get lower number of sixes

    private void Awake()
    {
        pathParent = FindObjectOfType<PathObjectParent>();
    }
    public void OnMouseDown()
    {
        generateRandomNumberOnDice = StartCoroutine(RollingDices());
    }


    public void mouseRoll()
    {
        generateRandomNumberOnDice = StartCoroutine(RollingDices());
    }

    IEnumerator RollingDices()
    {
        GameManager.gm.transferDice = false;

        yield return new WaitForEndOfFrame();

        if (GameManager.gm.canDiceRoll)
        {
            if (GameManager.gm.sound) { DiceSound.PlaySound(); }
        GameManager.gm.canDiceRoll = false;
        numberSpriteHolder.gameObject.SetActive(false);
        rollingDiceAnm.gameObject.SetActive(true);
        yield return new WaitForSeconds(1f);

            //if (GameManager.gm.totalSix == 2)
            //{
            //    maxNum = 5;
            //}

        numberGot = Random.Range(0, 6/*maxNum*/);

            //if (numberGot == 5) for get lower number of sixes
            //{
            //    GameManager.gm.totalSix += 1;
            //}
            //else
            //{
            //    GameManager.gm.totalSix = 0;
            //}
        
        numberSpriteHolder.sprite = numberSprits[numberGot];
        numberGot += 1;

            GameManager.gm.numberOfStepsToMove = numberGot;
            GameManager.gm.rollingDice = this;

        numberSpriteHolder.gameObject.SetActive(true);
        rollingDiceAnm.gameObject.SetActive(false);
        yield return new WaitForEndOfFrame();

            int nummberGot = GameManager.gm.numberOfStepsToMove;

            if (PlayerCanNotMove())
            {
                yield return new WaitForSeconds(0.5f);
                if (nummberGot !=6)
                {
                    GameManager.gm.transferDice = true;
                }
                else
                {
                    GameManager.gm.selfDice = true;
                }
            }
               
            else
            {
                
                if (GameManager.gm.rollingDice == GameManager.gm.manageRollingDice[0])
                {
                    outPieces = GameManager.gm.blueOutPlayers;
                }
                else if (GameManager.gm.rollingDice == GameManager.gm.manageRollingDice[1])
                {
                    outPieces = GameManager.gm.redOutPlayers;
                }
                else if (GameManager.gm.rollingDice == GameManager.gm.manageRollingDice[2])
                {
                    outPieces = GameManager.gm.greenOutPlayers;
                }
                else if (GameManager.gm.rollingDice == GameManager.gm.manageRollingDice[3])
                {
                    outPieces = GameManager.gm.yellowOutPlayers;
                }


                if (outPieces == 0 && nummberGot != 6)
                {
                    yield return new WaitForSeconds(0.5f);
                    GameManager.gm.transferDice = true;
                }
                else
                {
                    if (outPieces == 0 && numberGot == 6)
                    {
                        MakeplayerReadyToMove(0);
                    }
                    else if (outPieces == 1 && numberGot !=6 && GameManager.gm.CanPlayerMove)
                    {
                        int playerPicePosition = CheckoutPlayer();
                        if (playerPicePosition >= 0)
                        {
                            GameManager.gm.CanPlayerMove = false;
                            MovePlayerPiece = StartCoroutine(MoveSteps_Enum(playerPicePosition));
                        }
                        else
                        {
                            yield return new WaitForSeconds(0.5f);
                            if (nummberGot != 6)
                            {
                                GameManager.gm.transferDice = true;
                            }
                            else
                            {
                                GameManager.gm.selfDice = true;
                            }
                        }
                    }
                    else if(GameManager.gm.totalPlayerCanPlay == 1 && GameManager.gm.rollingDice == GameManager.gm.manageRollingDice[2])
                    {
                        if (numberGot == 6 && outPieces < 4)
                        {
                            MakeplayerReadyToMove(outPlayerToMove());
                        }
                        else
                        {
                            int playerPicePosition = CheckoutPlayer();
                            if (playerPicePosition >= 0)
                            {
                                GameManager.gm.CanPlayerMove = false;
                                MovePlayerPiece = StartCoroutine(MoveSteps_Enum(playerPicePosition));
                            }
                            else
                            {
                                yield return new WaitForSeconds(0.5f);
                                if (nummberGot != 6)
                                {
                                    GameManager.gm.transferDice = true;
                                }
                                else
                                {
                                    GameManager.gm.selfDice = true;
                                }
                            }
                        }
                    }
                    else
                    {
                        if (CheckoutPlayer() < 0)
                        {
                            yield return new WaitForSeconds(0.5f);
                            if (nummberGot != 6)
                            {
                                GameManager.gm.transferDice = true;
                            }
                            else
                            {
                                GameManager.gm.selfDice = true;
                            }
                        }
                    }
                   
                }
                                                                
            }

            GameManager.gm.RollingDiceManager();


            if (generateRandomNumberOnDice != null)
            {
            StopCoroutine(RollingDices());
             }
        }
    }

    int outPlayerToMove()
    {
        for (int i = 0; i < 4; i++)
        {
            if (!GameManager.gm.greenPlayerPice[i].isReady)
            {
                return i;
            }
        }
        return 0;
    }
    int CheckoutPlayer()
    {
        if (GameManager.gm.rollingDice == GameManager.gm.manageRollingDice[0])
        {
            currentPlayerPieces = GameManager.gm.bluePlayerPice;
            pathPointToMoveOn_ = pathParent.BluePathPoint;

        }
        else if (GameManager.gm.rollingDice == GameManager.gm.manageRollingDice[1])
        {
            currentPlayerPieces = GameManager.gm.redPlayerPice;
            pathPointToMoveOn_ = pathParent.RedPathPoint;
        }

        else if (GameManager.gm.rollingDice == GameManager.gm.manageRollingDice[2])
        {
            currentPlayerPieces = GameManager.gm.greenPlayerPice;
            pathPointToMoveOn_ = pathParent.GreenPathPoint;

        }
        else if (GameManager.gm.rollingDice == GameManager.gm.manageRollingDice[3])
        {
            currentPlayerPieces = GameManager.gm.yellowPlayerPice;
            pathPointToMoveOn_ = pathParent.YellowPathPoint;

        }

        for (int i = 0; i < currentPlayerPieces.Length; i++)
        {
            if ( currentPlayerPieces[i].isReady && isPathPointsAvailableToMove(GameManager.gm.numberOfStepsToMove, currentPlayerPieces[i].numberOfStepsAlreadyMove, pathPointToMoveOn_))
            {
                return i;
            }
        }

        return -1;
    }

    public bool PlayerCanNotMove()// changeble code here  all blueplayerpice and pathpoint
    {
        if (outPieces > 0)
        {
            bool canNotMove = false;
            if (GameManager.gm.rollingDice == GameManager.gm.manageRollingDice[0])
            {
                currentPlayerPieces = GameManager.gm.bluePlayerPice;
                pathPointToMoveOn_ = pathParent.BluePathPoint;
            }
            else if (GameManager.gm.rollingDice == GameManager.gm.manageRollingDice[1])
            {
                currentPlayerPieces = GameManager.gm.redPlayerPice;
                pathPointToMoveOn_ = pathParent.RedPathPoint;
            }
            else if (GameManager.gm.rollingDice == GameManager.gm.manageRollingDice[2])
            {
                currentPlayerPieces = GameManager.gm.greenPlayerPice;
                pathPointToMoveOn_ = pathParent.GreenPathPoint;
            }
            else if (GameManager.gm.rollingDice == GameManager.gm.manageRollingDice[3])
            {
                currentPlayerPieces = GameManager.gm.yellowPlayerPice;
                pathPointToMoveOn_ = pathParent.YellowPathPoint;
            }

            for (int i = 0; i < currentPlayerPieces.Length; i++)
            {
                if (currentPlayerPieces[i].isReady)
                {
                    if (isPathPointsAvailableToMove(GameManager.gm.numberOfStepsToMove, currentPlayerPieces[i].numberOfStepsAlreadyMove, pathPointToMoveOn_))
                    {
                        return false;
                    }
                }
                else
                {
                    if (!canNotMove)
                    {
                        canNotMove = true;
                    }
                }
            }

            if (canNotMove)
            {
                return true;
            }
        }
        return false;
    }

    bool isPathPointsAvailableToMove(int numOfStepsToMove_, int numOfStepsAlreadyMove, PathPoint[] pathPointsToMove_)
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


    public void MakeplayerReadyToMove(int outPlayer)
    {
        if (GameManager.gm.rollingDice == GameManager.gm.manageRollingDice[0])
        {
            outPlayerPiece = GameManager.gm.bluePlayerPice[outPlayer];
            pathPointToMoveOn_ = pathParent.BluePathPoint;
            GameManager.gm.blueOutPlayers += 1;
        }
        else if (GameManager.gm.rollingDice == GameManager.gm.manageRollingDice[1])
        {
            outPlayerPiece = GameManager.gm.redPlayerPice[outPlayer];
            pathPointToMoveOn_ = pathParent.RedPathPoint;
            GameManager.gm.redOutPlayers += 1;
        }
        else if (GameManager.gm.rollingDice == GameManager.gm.manageRollingDice[2])
        {
            outPlayerPiece = GameManager.gm.greenPlayerPice[outPlayer];
            pathPointToMoveOn_ = pathParent.GreenPathPoint;
            GameManager.gm.greenOutPlayers += 1;
        }
        else if (GameManager.gm.rollingDice == GameManager.gm.manageRollingDice[3])
        {
            outPlayerPiece = GameManager.gm.yellowPlayerPice[outPlayer];
            pathPointToMoveOn_ = pathParent.YellowPathPoint;
            GameManager.gm.yellowOutPlayers += 1;
        }

        outPlayerPiece.isReady = true;
        outPlayerPiece.transform.position = pathPointToMoveOn_[0].transform.position;
        outPlayerPiece.numberOfStepsAlreadyMove = 1;

        outPlayerPiece.previousPathPoint = pathPointToMoveOn_[0];
        outPlayerPiece.currentPathPoint = pathPointToMoveOn_[0];
        outPlayerPiece.currentPathPoint.AddPlayerPice(outPlayerPiece);
        GameManager.gm.AddPathPoint(outPlayerPiece.currentPathPoint);

        GameManager.gm.canDiceRoll = true;
        GameManager.gm.selfDice = true;
        GameManager.gm.transferDice = false;
        GameManager.gm.numberOfStepsToMove = 0;

        GameManager.gm.SelfRoal();

    }


    IEnumerator MoveSteps_Enum(int movePlayer)
    {
        if (GameManager.gm.rollingDice == GameManager.gm.manageRollingDice[0])
        {
            outPlayerPiece = GameManager.gm.bluePlayerPice[movePlayer];
            pathPointToMoveOn_ = pathParent.BluePathPoint;

        }
        else if (GameManager.gm.rollingDice == GameManager.gm.manageRollingDice[1])
        {
            outPlayerPiece = GameManager.gm.redPlayerPice[movePlayer];
            pathPointToMoveOn_ = pathParent.RedPathPoint;
        }

        else if (GameManager.gm.rollingDice == GameManager.gm.manageRollingDice[2])
        {
            outPlayerPiece = GameManager.gm.greenPlayerPice[movePlayer];
            pathPointToMoveOn_ = pathParent.GreenPathPoint;

        }
        else if (GameManager.gm.rollingDice == GameManager.gm.manageRollingDice[3])
        {
            outPlayerPiece = GameManager.gm.yellowPlayerPice[movePlayer];
            pathPointToMoveOn_ = pathParent.YellowPathPoint;

        }

        GameManager.gm.transferDice = false;

        yield return new WaitForSeconds(0.25f);
         int  numberOfStepsToMove = GameManager.gm.numberOfStepsToMove;


        outPlayerPiece.currentPathPoint.RescaleAndRepositonAllPlayerPiece();


        for (int i = outPlayerPiece.numberOfStepsAlreadyMove; i < (outPlayerPiece.numberOfStepsAlreadyMove +  numberOfStepsToMove); i++)
        {

            if (isPathPointsAvailableToMove(numberOfStepsToMove, outPlayerPiece.numberOfStepsAlreadyMove, pathPointToMoveOn_))
            {

                outPlayerPiece.transform.position = pathPointToMoveOn_[i].transform.position;
                if (GameManager.gm.sound) { GameManager.gm.ads.Play(); }
                yield return new WaitForSeconds(0.35f);
            }
        }

        if (isPathPointsAvailableToMove(numberOfStepsToMove, outPlayerPiece.numberOfStepsAlreadyMove, pathPointToMoveOn_))
        {
            outPlayerPiece.numberOfStepsAlreadyMove += numberOfStepsToMove;

            GameManager.gm.RemovePathPoint(outPlayerPiece.previousPathPoint);

            outPlayerPiece.previousPathPoint.RemovePlayerPice(outPlayerPiece);

            outPlayerPiece.currentPathPoint = pathPointToMoveOn_[outPlayerPiece.numberOfStepsAlreadyMove - 1];

            if (outPlayerPiece.currentPathPoint.AddPlayerPice(outPlayerPiece))
            {
                if (outPlayerPiece.numberOfStepsAlreadyMove == 57)
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

            GameManager.gm.AddPathPoint(outPlayerPiece.currentPathPoint);
            outPlayerPiece.previousPathPoint = outPlayerPiece.currentPathPoint;



            GameManager.gm.numberOfStepsToMove = 0;


        }

        GameManager.gm.CanPlayerMove = true;
        GameManager.gm.RollingDiceManager();

        if (MovePlayerPiece != null)
        {
            StopCoroutine("MoveSteps_Enum");
        }
    }
}

