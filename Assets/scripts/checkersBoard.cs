using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checkersBoard : MonoBehaviour
{
    public Piece[,] pieces = new Piece[8, 8];
    public GameObject whitePiecePrefab;
    public GameObject blackPiecePrefab;

    private Vector3 boardOffset = new Vector3(-4.0f, 0, -4.0f);  //offset for pieces to be on board
    private Vector3 pieceOffset = new Vector3(0.5f, 0, 0.5f);   //offset for pieces to match boxes

    private bool isWhite;
    private bool isWhiteTurn;

    private Piece selectedPiece;

    private Vector2 mouseOver; // mouse pos
    private Vector2 startDrag; //will drag the pieces instead of point and click, might change later
    private Vector2 endDrag;

    private void Start()
    {
        isWhiteTurn = true;
        GenerateBoard();
    }

    private void Update()
    {
        UpdateMouseOver();
        //Debug.Log(mouseOver); //board collider -0.08 from edges, change when replacing asset

        //if player turn
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
        if (!Camera.main)
        {
            Debug.Log("No main camera found");
            return;
        }
        RaycastHit hit;
        if(Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 25.0f, LayerMask.GetMask("Board"))) //if the mouse is on board
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
        
        if (!Camera.main)
        {
            Debug.Log("No main camera found");
            return;
        }
        RaycastHit hit;
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 25.0f, LayerMask.GetMask("Board"))) 
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
        if(p != null)
        {
            selectedPiece = p;
            startDrag = mouseOver;
            //Debug.Log(selectedPiece.name);
        }
        /*else
        {
            Debug.Log("AGHGHGAHGHAAHGAAHGAHAGAHGAHAGAH");
        }*/
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
            if(Mathf.Abs(x2-x2) == 2)
            {
                Piece p = pieces[(x1 + x2) / 2, (y1 + y2) / 2];
                if (p != null)
                {
                    pieces[(x1 + x2) / 2, (y1 + y2) / 2] = null;
                    Destroy(p);
                }
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
        selectedPiece = null;
        startDrag = Vector2.zero;

        isWhiteTurn = !isWhiteTurn;
        CheckVictory();
    }
    private void CheckVictory()
    {

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
