using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piece : MonoBehaviour
{
    public bool isWhite;
    public bool isKing;
       
     
    public bool ValidMove(Piece[,] board, int x1, int y1, int x2, int y2)
    {

        //if on top of another piece
        if (board[x2,y2] != null)
        {
            return false; //denied
        }
        int deltaMove = Mathf.Abs(x1 - x2);
        int deltaMoveY = y2 - y1;

        //for white team
        if(isWhite || isKing)
        {
            if(deltaMove == 1)//normal move
            { 
                if(deltaMoveY == 1)
                {
                    return true; //allowed move 
                }
            }
            else if(deltaMove == 2)//kill jump
            {
                if(deltaMoveY == 2)
                {
                    Piece p = board[(x1 + x2) / 2, (y1 + y2) / 2];

                    if(p != null && p.isWhite != isWhite)
                    {
                        return true;//get rid of middle piece
                    }
                }
            }
        }

        //for black team
        if (!isWhite || isKing)
        {
            if (deltaMove == 1)//normal move
            {
                if (deltaMoveY == -1)
                {
                    return true; //allowed move 
                }
            }
            else if (deltaMove == 2)//kill jump
            {
                if (deltaMoveY == -2)
                {
                    Piece p = board[(x1 + x2) / 2, (y1 + y2) / 2];

                    if (p != null && p.isWhite != isWhite)
                    {
                        return true;//get rid of middle piece
                    }
                }
            }
        }
        if (FindObjectOfType<GameSystemHandler>().isAcornEvent)
        {
            return true;
        }
        else
        {
            return false;
        }


    }
    /* public bool IsForcedToMove(Piece[,] board, int x, int y) //feature to force the player to kill an enemy if it can do so, remove if not used
    {
        if (isWhite)
        {
            //top left
            if(x>=2  && y<=5)
            {
                Piece p = board[x - 1, y + 1];

                if (p != null && p.isWhite != isWhite)//if there is a piece and is not the same color
                {
                    if (board[x-2, y +2] == null)//if posible to land after jump
                    {
                        return true;
                    }
                }
            }

            //top right
            if (x <= 5 && y <= 5)
            {
                Piece p = board[x + 1, y + 1];

                if (p != null && p.isWhite != isWhite)//if there is a piece and is not the same color
                {
                    if (board[x + 2, y + 2] == null)//if posible to land after jump
                    {
                        return true;
                    }
                }
            }
        }
        if(!isWhite || isKing) //black team
        {
            //bottom left
            if (x >= 2 && y >= 2)
            {
                Piece p = board[x - 1, y - 1];

                if (p != null && p.isWhite != isWhite)//if there is a piece and is not the same color
                {
                    if (board[x - 2, y - 2] == null)//if posible to land after jump
                    {
                        return true;
                    }
                }
            }*/

    /*  //bottom right
      if (x <= 5 && y >= 2)
      {
          Piece p = board[x + 1, y - 1];

          if (p != null && p.isWhite != isWhite)//if there is a piece and is not the same color
          {
              if (board[x + 2, y - 2] == null)//if posible to land after jump
              {
                  return true;
              }
          }
      }
  }

}*/

}
