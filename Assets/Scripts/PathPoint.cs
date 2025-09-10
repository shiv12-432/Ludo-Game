using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathPoint : MonoBehaviour
{
    PathPoint[] pathPointToMoveOn_;
    public PathObjectParent pathObjectParent;
    public List<PlayerPiece> PlayerPieceList = new List<PlayerPiece>();

   
    void Start()
    {
        pathObjectParent = GetComponentInParent<PathObjectParent>();
    }
    public bool AddPlayerPice(PlayerPiece playerPiece_)
    {
        if (this.name == "CenterPath")
        {
            if (GameManager.gm.sound) { GameManager.gm.playerPieceWinSound.Play(); }
            Compled(playerPiece_);
        }
        if (this.name != "PathPoint" && this.name != "PathPoint (8)" && this.name != "PathPoint (13)" && this.name != "PathPoint (21)" && this.name != "PathPoint (26)" && this.name != "PathPoint (34)" && this.name != "PathPoint (39)" && this.name != "PathPoint (47)" && this.name != "CenterPath")  
        {
            if (PlayerPieceList.Count == 1)
            {
                string prePlayerPiceName = PlayerPieceList[0].name;
                string curntPlayerPiceName = playerPiece_.name;
                curntPlayerPiceName = curntPlayerPiceName.Substring(0, curntPlayerPiceName.Length - 4);

                if (!prePlayerPiceName.Contains(curntPlayerPiceName))
                {
                    PlayerPieceList[0].isReady = false;
                    if (GameManager.gm.sound) { GameManager.gm.DiceKillSound.Play(); }//will be delete code sound
                    StartCoroutine(revertOnStart(PlayerPieceList[0]));

                    PlayerPieceList[0].numberOfStepsAlreadyMove = 0;

                    RemovePlayerPice(PlayerPieceList[0]);

                    PlayerPieceList.Add(playerPiece_);
                    return false;
                }
            }
        }
        addPlayer(playerPiece_);
        return true;

    }

    IEnumerator revertOnStart(PlayerPiece playerPiece_)
    {
        if (playerPiece_.name.Contains("Blue"))
        {
            GameManager.gm.blueOutPlayers -= 1;
            pathPointToMoveOn_ = pathObjectParent.BluePathPoint;
        }
        else if (playerPiece_.name.Contains("Red"))
        {
            GameManager.gm.redOutPlayers -= 1;
            pathPointToMoveOn_ = pathObjectParent.RedPathPoint;
        }
        else if (playerPiece_.name.Contains("Green"))
        {
            GameManager.gm.greenOutPlayers -= 1;
            pathPointToMoveOn_ = pathObjectParent.GreenPathPoint;
        }
        else if (playerPiece_.name.Contains("Yellow"))
        {
            GameManager.gm.yellowOutPlayers -= 1;
            pathPointToMoveOn_ = pathObjectParent.YellowPathPoint;
        }

        for (int i = playerPiece_.numberOfStepsAlreadyMove - 1; i >= 0; i--)
        {
            playerPiece_.transform.position = pathPointToMoveOn_[i].transform.position;
            yield return new WaitForSeconds(0.07f);
        }
        
        playerPiece_.transform.position = pathObjectParent.BasePoints[BasePointPosition(playerPiece_.name)].transform.position;//index out of bond exception
    }

    int BasePointPosition(string name)
    {
        
        for (int i = 0; i < pathObjectParent.BasePoints.Length ; i++)
        {
            if (pathObjectParent.BasePoints[i].name == name)
            {
                
                return i;
            }
        }

        return -1;// -1 return
    }

    void addPlayer(PlayerPiece playerPiece_)
    {
        PlayerPieceList.Add(playerPiece_);
        RescaleAndRepositonAllPlayerPiece();
    }
    public void RemovePlayerPice(PlayerPiece playerPiece_)
    {
        if (PlayerPieceList.Contains(playerPiece_))
        {
            PlayerPieceList.Remove(playerPiece_);
            RescaleAndRepositonAllPlayerPiece();
        }
    }

    // hide players
    void HidePlayers(PlayerPiece[] PlayerPieces_)
    {
        for (int i = 0; i < PlayerPieces_.Length; i++)
        {
            PlayerPieces_[i].gameObject.SetActive(false);
        }
    }

    private void Compled(PlayerPiece playerPiece_)
    {
        if (playerPiece_.name.Contains("Blue"))
        {
            GameManager.gm.blueCompletePlayers += 1;
            GameManager.gm.blueOutPlayers -= 1;
            if (GameManager.gm.blueCompletePlayers == 4)
            {
                showCelebration();
                if (GameManager.gm.sound) { GameManager.gm.completeWinSound.Play(); }
                if (GameManager.gm.totalPlayerCanPlay==2)
                {
                    HidePlayers(GameManager.gm.bluePlayerPice);
                }
               
            }
        }
        else if (playerPiece_.name.Contains("Red"))
        {
            GameManager.gm.redCompletePlayers += 1;
            GameManager.gm.redOutPlayers -= 1;
            if (GameManager.gm.redCompletePlayers == 4)
            {
                showCelebration();
                if (GameManager.gm.sound) { GameManager.gm.completeWinSound.Play(); }
               
            }
        }
        else if (playerPiece_.name.Contains("Green"))
        {
            GameManager.gm.greenCompletePlayers += 1;
            GameManager.gm.greenOutPlayers -= 1;
            if (GameManager.gm.greenCompletePlayers == 4)
            {
                showCelebration();
                if (GameManager.gm.sound) { GameManager.gm.completeWinSound.Play(); }

                if (GameManager.gm.totalPlayerCanPlay == 2)
                {
                    HidePlayers(GameManager.gm.greenPlayerPice);
                }
               
            }
        }
        else if (playerPiece_.name.Contains("Yellow"))
        {
            GameManager.gm.yellowCompletPlayers += 1;
            GameManager.gm.yellowOutPlayers -= 1;
            if (GameManager.gm.yellowCompletPlayers == 4)
            {
                showCelebration();
                if (GameManager.gm.sound) { GameManager.gm.completeWinSound.Play(); }
                
            }
        }
    }

    void showCelebration()
    {
  
    }

    public void RescaleAndRepositonAllPlayerPiece()
    {
        int plsCount = PlayerPieceList.Count;
        bool isOdd = (plsCount % 2) == 0 ? false : true;

        int extent = plsCount / 2;
        int counter = 0;
        int spriteLayer = 0;

        if (isOdd)
        {
            for (int i = -extent; i <= extent; i++)
            {
                PlayerPieceList[counter].transform.localScale = new Vector3(pathObjectParent.scales[plsCount - 1], pathObjectParent.scales[plsCount - 1], 1f);
                PlayerPieceList[counter].transform.position = new Vector3(transform.position.x + (i * pathObjectParent.positionDifference[plsCount - 1]), transform.position.y, 0f);
                counter++;
            }
        }
        else
        {
            for (int i = -extent; i < extent; i++)
            {
                PlayerPieceList[counter].transform.localScale = new Vector3(pathObjectParent.scales[plsCount - 1], pathObjectParent.scales[plsCount - 1], 1f);
                PlayerPieceList[counter].transform.position = new Vector3(transform.position.x + (i * pathObjectParent.positionDifference[plsCount - 1]), transform.position.y, 0f);
                counter++;
            }
        }

        for (int i = 0; i < PlayerPieceList.Count; i++)
        {
            PlayerPieceList[i].GetComponentInChildren<SpriteRenderer>().sortingOrder = spriteLayer;
            spriteLayer++;
        }
    }
}
