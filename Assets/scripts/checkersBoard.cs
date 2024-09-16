using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checkersBoard : MonoBehaviour
{
    //piece rotation
    //cheating on and off
    //controller support
    //spliscreen



    public Piece[,] pieces = new Piece[8, 8];
    public GameObject whitePiecePrefab;
    public GameObject blackPiecePrefab;

    private Vector3 boardOffset = new Vector3(-4.0f, 0, -4.0f);  //offset for pieces to be on board
    private Vector3 pieceOffset = new Vector3(0.5f, 0, 0.5f);   //offset for pieces to match boxes

    public bool isWhite;
    private bool isWhiteTurn;
    private bool hasKilled;

    public Camera mainCamera; //camera ref

    public GameSystemHandler gameSystem;

    private Piece selectedPiece;
    
    private Vector2 mouseOver; // mouse pos
    private Vector2 startDrag; //will drag the pieces instead of point and click, might change later
    private Vector2 endDrag;


    //cheating variables//

    private Piece[,] savedBoardState;
    private bool savedIsWhiteTurn;
    private bool savedHasKilled;

    private void Start()
    {
        isWhite = true;
        isWhiteTurn = true;
        GenerateBoard();
    }

    private void Update()
    {
        UpdateMouseOver();
        //Debug.Log(mouseOver); //board collider -0.08 from edges, change when replacing asset

        if( (isWhite)?isWhiteTurn:!isWhiteTurn)//event (so both players can move) or is white? is white turn else black moves 
        {
            int x = (int)mouseOver.x;
            int y = (int)mouseOver.y;

            if(selectedPiece != null)
            {
                UpdatePieceDrag(selectedPiece);
            }

            if (Input.GetMouseButtonDown(0))
            {
                SelectPiece(x, y);
            }

            if (Input.GetMouseButtonUp(0))
            {
               TryMove((int)startDrag.x,(int)startDrag.y,x,y);
            }
        }
    }
     
    private void UpdateMouseOver()
    {
        //if player turn
        if (!mainCamera)
        {
            Debug.Log("No main camera found");
            return;
        }
        RaycastHit hit;
        if(Physics.Raycast(mainCamera.ScreenPointToRay(Input.mousePosition), out hit, 25.0f, LayerMask.GetMask("Board"))) //if the mouse is on board
        {
            mouseOver.x = (int)(hit.point.x - boardOffset.x); //int so it snaps to a decimal point
            mouseOver.y = (int)(hit.point.z - boardOffset.z); // on z since board is on floor not wall
            //Debug.Log("hit X: " + mouseOver.x + " hit Y: " + mouseOver.y);
        }
        else
        {
            mouseOver.x = -1;
            mouseOver.y = -1;
        }
    }
    private void UpdatePieceDrag(Piece p) //lift up piece
    {
        
        if (!mainCamera)
        {
            Debug.Log("No main camera found");
            return;
        }
        RaycastHit hit;
        if (Physics.Raycast(mainCamera.ScreenPointToRay(Input.mousePosition), out hit, 25.0f, LayerMask.GetMask("Board"))) 
        {
            p.transform.position = hit.point + Vector3.up;
        }
       
    }
    private void ResetMove()
    {
        startDrag = Vector2.zero;
        selectedPiece = null;
    }

    private void SelectPiece(int x, int y)
    {
        //out of bounds
        if ( x < 0 || x >= 8 || y < 0 || y >= 8)
            return;

        Piece p = pieces[x, y];
        if(p != null && p.isWhite == isWhite)
        {
            selectedPiece = p;
            startDrag = mouseOver;
            //Debug.Log(selectedPiece.name);

        }
    }
    private void TryMove(int x1, int y1, int x2, int y2)//move, start and end position
    {
        //multiplayer support
        startDrag = new Vector2(x1, y1);//redefines values for when multiplayer
        endDrag = new Vector2(x2, y2);
        selectedPiece = pieces[x1, y1];

        //check if out of bounds
        if(x2<0 || x2 >= 8 || y2<0 || y2 >= 8)
        {
            if(selectedPiece != null)
                MovePiece(selectedPiece, x1, y1);//return piece to initial pos

            ResetMove();
            return;
        }
        //check if selected piece
        if(selectedPiece != null)
        {
            //if piece didn't move
            if(endDrag == startDrag)
            {
                MovePiece(selectedPiece, x1, y1);
                ResetMove();
                return;
            }
        }

        //check if move valid
        if(selectedPiece.ValidMove(pieces, x1, y1, x2, y2))
        {
            //was anything killed
            //if the move involves a capture
            if(Mathf.Abs(x2-x1) == 2)
            {
                Piece p = pieces[(x1 + x2) / 2, (y1 + y2) / 2];
                if (p != null)
                {
                    pieces[(x1 + x2) / 2, (y1 + y2) / 2] = null;
                    Destroy(p.gameObject);
                    hasKilled = true;
                }
            }

            // Update board state
            pieces[x2, y2] = selectedPiece;
            pieces[x1, y1] = null;
            MovePiece(selectedPiece, x2, y2);

            if(hasKilled && CanContinueJump(selectedPiece, x2, y2))
            {
                startDrag = new Vector2(x2, y2); //update start pos
                return;
            }
            else
            {
                EndTurn();
            }
        }
        else //if move is not valid, reset
        {
            MovePiece(selectedPiece, x1, y1);
            ResetMove();
            return;
        }
    }
  
    private bool CanContinueJump(Piece p, int x, int y)
    {
        // Check all possible jump directions (diagonal moves by 2 spaces)
        if (p.isWhite || p.isKing) // White or King
        {
            // Check top-left
            if (x >= 2 && y <= 5)
            {
                Piece target = pieces[x - 1, y + 1];
                if (target != null && target.isWhite != p.isWhite && pieces[x - 2, y + 2] == null)
                    return true;
            }
            // Check top-right
            if (x <= 5 && y <= 5)
            {
                Piece target = pieces[x + 1, y + 1];
                if (target != null && target.isWhite != p.isWhite && pieces[x + 2, y + 2] == null)
                    return true;
            }
        }

        if (!p.isWhite || p.isKing) // Black or King
        {
            // Check bottom-left
            if (x >= 2 && y >= 2)
            {
                Piece target = pieces[x - 1, y - 1];
                if (target != null && target.isWhite != p.isWhite && pieces[x - 2, y - 2] == null)
                    return true;
            }
            // Check bottom-right
            if (x <= 5 && y >= 2)
            {
                Piece target = pieces[x + 1, y - 1];
                if (target != null && target.isWhite != p.isWhite && pieces[x + 2, y - 2] == null)
                    return true;
            }
        }

        // No more jumps available
        return false;
    }

    private void EndTurn()
    {
        int x = (int)endDrag.x;
        int y = (int)endDrag.y;

        if(selectedPiece != null)//becomes kingggggg
        {
            if(selectedPiece.isWhite && !selectedPiece.isKing && y == 7)
            {
                selectedPiece.isKing = true;
                //change sprite

                selectedPiece.transform.Rotate(Vector3.right * 180);  //if rotate
            }
            else if(!selectedPiece.isWhite && !selectedPiece.isKing && y == 0)
            {
                selectedPiece.isKing = true;
                //change sprite

                selectedPiece.transform.Rotate(Vector3.right * 180);  //if rotate
            }
        }

        //clear selected piece
        selectedPiece = null;
        startDrag = Vector2.zero;

        //switch turn
        isWhiteTurn = !isWhiteTurn;
        isWhite = !isWhite;  //makes game local, remove when multiplayer
        hasKilled=false;
        CheckVictory();
    }
    private void CheckVictory()
    {
        var ps = FindObjectsOfType<Piece>();
        bool hasWhite = false, hasBlack = false;
        for(int i = 0;i < ps.Length; i++)
        {
            if (ps[i].isWhite)
            {
                hasWhite = true;
            }
            else
            {
                hasBlack = true;
            }
        }
        if (!hasWhite)
        {
            Victory(false);
        }
        if (!hasBlack)
        {
            Victory(true);
        }
    }
    private void Victory(bool isWhite)
    {
        if (isWhite)
        {
            Debug.Log("White team has won");
        }
        else
        {
            Debug.Log("Black team has won");
        }
    }

    private void GenerateBoard()
    {
        //generate white  team
        for (int y = 0; y<3; y++)
        {
            bool oddRow = (y % 2 == 0);
            for(int x = 0; x<8; x+= 2)
            {
                if (oddRow)
                {
                    GeneratePiece(x, y);
                }
                else
                {
                    GeneratePiece(x+1, y);
                }
                
            }
        }

        //generate black team
        for (int y = 7; y > 4; y--)
        {
            bool oddRow = (y % 2 == 0);
            for (int x = 0; x < 8; x += 2)
            {
                if (oddRow)
                {
                    GeneratePiece(x, y);
                }
                else
                {
                    GeneratePiece(x +1, y);
                }

            }
        }
    }
    private void GeneratePiece(int x, int y) 
    {
        bool isPieceWhite = (y > 3) ? false : true; //if y>3 = false, else true;
        GameObject gopiece = Instantiate((isPieceWhite) ? whitePiecePrefab : blackPiecePrefab) as GameObject; //spawn whitePiece if false, else blackPiece
        gopiece.transform.SetParent(transform);
        Piece p = gopiece.GetComponent<Piece>();
        pieces[x, y] = p;
        MovePiece(p,x,y);
    }
    private void MovePiece(Piece p, int x, int y)
    {
        p.transform.position = (Vector3.right * x) + (Vector3.forward * y) + boardOffset + pieceOffset;
    }

    ///////////////////////
    //cheating mechanics
    //////////////////////

    //save state
   /* private void SaveBoardState()
    {
        // Create a copy of the current board state
        savedBoardState = (Piece[,])pieces.Clone();
        savedIsWhiteTurn = isWhiteTurn;
        savedHasKilled = hasKilled;
    }
    private void RestoreBoardState()
    {
        //restore board state
        pieces = (Piece[,])savedBoardState.Clone();
        isWhiteTurn = savedIsWhiteTurn;
        hasKilled = savedHasKilled;

        //visually reset board positions
        for (int x = 0; x < 8; x++)
        {
            for (int y = 0; y < 8; y++)
            {
                if (pieces[x,y] != null)
                {
                    MovePiece(pieces[x,y],x,y);
                }
            }
        }
    }
    void CallOutCheat()
    {
        if (gameSystem.isAcornEvent)
        {
            RestoreBoardState();
            gameSystem.isAcornEvent = false;
            Debug.Log("Cheater! Board Restored");
        }
    }*/
}
