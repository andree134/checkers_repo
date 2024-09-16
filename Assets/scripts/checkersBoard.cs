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
    private List<Piece> forcedPieces;

    private Vector2 mouseOver; // mouse pos
    private Vector2 startDrag; //will drag the pieces instead of point and click, might change later
    private Vector2 endDrag;

    private void Start()
    {
        isWhite = true;
        isWhiteTurn = true;
        forcedPieces = new List<Piece>();
        GenerateBoard();
    }

    private void Update()
    {
        UpdateMouseOver();
        //Debug.Log(mouseOver); //board collider -0.08 from edges, change when replacing asset

        if((isWhite)?isWhiteTurn:!isWhiteTurn)//is white and is white turn? else is black
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

    private void SelectPiece(int x, int y)
    {
        //out of bounds
        if ( x < 0 || x >= 8 || y < 0 || y >= 8)
            return;

        Piece p = pieces[x, y];
        if(p != null && p.isWhite == isWhite)
        {
            if (forcedPieces.Count == 0)
            {
                selectedPiece = p;
                startDrag = mouseOver;
                //Debug.Log(selectedPiece.name);
            }
            else
            { 
                //look for piece in forced piece list
                if(forcedPieces.Find(fp => fp == p) == null)
                    return;
                
                selectedPiece = p;
                startDrag = mouseOver;
            }
                
        }
        /*else
        {
            Debug.Log("AGHGHGAHGHAAHGAAHGAHAGAHGAHAGAH");
        }*/
    }
    private void TryMove(int x1, int y1, int x2, int y2)//move, start and end position
    {
        forcedPieces = ScanForPossibleMove();  //if forced pieces uncomment

        //multiplayer support
        startDrag = new Vector2(x1, y1);//redefines values for when multiplayer
        endDrag = new Vector2(x2, y2);
        selectedPiece = pieces[x1, y1];

        //check if out of bounds
        if(x2<0 || x2 >= 8 || y2<0 || y2 >= 8)
        {
            if(selectedPiece != null)
                MovePiece(selectedPiece, x1, y1);//return piece to initial pos
            

            startDrag = Vector2.zero;
            selectedPiece = null;
            return;
        }
        //check if selected piece
        if(selectedPiece != null)
        {
            //if piece didn't move
            if(endDrag == startDrag)
            {
                MovePiece(selectedPiece, x1, y1);

                startDrag = Vector2.zero;
                selectedPiece = null;
                return;
            }
        }

        //check if move valid
        if(selectedPiece.ValidMove(pieces, x1, y1, x2, y2))
        {
            //was anything killed

            //if jump
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

            //kill?
            if(forcedPieces.Count != 0 && !hasKilled) //comment if no forced pieces
            {
                MovePiece(selectedPiece, x1, y1);
                startDrag = Vector2.zero;
                selectedPiece = null;
                return;
            }
            pieces[x2, y2] = selectedPiece;
            pieces[x1, y1]=null;
            MovePiece(selectedPiece, x2, y2);

            EndTurn();
        }
        else //if move is not valid, reset
        {
            MovePiece(selectedPiece, x1, y1);
            startDrag = Vector2.zero;
            selectedPiece = null;
            return;
        }
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

        selectedPiece = null;
        startDrag = Vector2.zero;

        if (ScanForPossibleMove(selectedPiece, x, y).Count != 0 && hasKilled)
        {
            return;
        }

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
    private List<Piece> ScanForPossibleMove(Piece p, int x, int y)
    {
        forcedPieces = new List<Piece>();

        if (pieces[x, y].IsForcedToMove(pieces, x, y))
        {
            forcedPieces.Add(pieces[x,y]);
        }

        return forcedPieces;
    }
    private List<Piece> ScanForPossibleMove()//if force pieces
    {
        forcedPieces = new List<Piece>();
        //check all pieces individually
        for(int i=0; i < 8; i++)
        {
            for(int j = 0; j < 8;j++)
            {
                if (pieces[i,j] != null && pieces[i,j].isWhite == isWhiteTurn)
                {
                    if (pieces[i,j].IsForcedToMove(pieces, i, j))
                    {
                        forcedPieces.Add(pieces[i,j]);
                    }
                }
            }
        }
        return forcedPieces;
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
}
