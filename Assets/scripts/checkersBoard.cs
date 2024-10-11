using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class checkersBoard : MonoBehaviour
{
    
    //piece rotation make game object, switch
    //cheating on and off handle turn end
    //line 362 mesh change

    public Piece[,] pieces = new Piece[8, 8];
    public GameObject whitePiecePrefab;
    public GameObject blackPiecePrefab;

    //Jason's edit for recording the remain pieces
    public int whitePieceLeft;
    public int blackPieceLeft;
    

    private Vector3 boardOffset = new Vector3(-4.0f, 0, -4.0f);  //offset for pieces to be on board
    private Vector3 pieceOffset = new Vector3(0.5f, 0, 0.5f);   //offset for pieces to match boxes

    public bool isWhite;
    private bool isWhiteTurn;
    private bool hasKilled;
    private bool hasCheated; 

    //controller
    [SerializeField] Transform cursor1;
    [SerializeField] Transform cursor2;

    //camera references
    public Camera mainCamera; 
    public Camera p1Camera; //Player 1
    public Camera p2Camera; //Player 2

    //players' System REF
    [SerializeField] private Player_HealthSystem whitePlayerSystem;
    [SerializeField] private Player_HealthSystem blackPlayerSystem;

    [SerializeField] public GameSystemHandler gameSystem;

    private Piece selectedPiece;
    
    private Vector2 mouseOver1; // mouse pos
    private Vector2 mouseOver2; // mouse pos
    private Vector2 startDrag; //will drag the pieces instead of point and click, might change later
    private Vector2 endDrag;

    //controller support
   /* private Controller currentControls;
    private Controller player1Controls;
    private Controller player2Controls;

    //player specific controls
    public bool isPlayer1Active = true; //player 1 turn*/ //i want to cry


    //cheating variables//

    private Piece[,] savedBoardState;
    private bool savedIsWhiteTurn;
    private bool savedHasKilled;

    //timer
    private GameTimer gameTimer;

    [SerializeField] private PlayerInput playerInput;
    [SerializeField] private PlayerInput playerInput2;

    //Audio
    [SerializeField] private AudioSource checkerAudioSource;
    [SerializeField] private AudioClip placingPiece;

    private void OnValidate()
    {
        //playerInput = GameObject.Find("Player(Clone)").GetComponent<PlayerInput>();
    }

    private void Awake()
    {
        gameTimer = FindAnyObjectByType<GameTimer>(); 
    }

    private void Start()
    {


        playerInput = GameObject.Find("Player").GetComponent<PlayerInput>();
        
        cursor1 = GameObject.Find("MouseP1").transform;
        

        mainCamera = GameObject.Find("TopDownCamP1").GetComponent<Camera>();
        p1Camera = GameObject.Find("TopDownCamP1").GetComponent<Camera>();
        

        whitePlayerSystem = GameObject.Find("Player").GetComponent<Player_HealthSystem>();
        

        if (GameObject.Find("Opponent(Clone)") != null)
        {
            cursor2 = GameObject.Find("MouseP2").transform;
            playerInput2 = GameObject.Find("Opponent(Clone)").GetComponent<PlayerInput>();
            p2Camera = GameObject.Find("TopDownCamP2").GetComponent<Camera>();
            blackPlayerSystem = GameObject.Find("Opponent(Clone)").GetComponent<Player_HealthSystem>();
        }

        isWhite = true;
        isWhiteTurn = true;
        hasCheated = false;
        whitePieceLeft = 12;
        blackPieceLeft = 12;
        GenerateBoard();
    }

    private void Update()
    {
        if(GameObject.Find("Opponent(Clone)") != null && blackPlayerSystem == null)
        {
            blackPlayerSystem = GameObject.Find("Opponent(Clone)").GetComponent<Player_HealthSystem>();
            p2Camera = GameObject.Find("TopDownCamP2").GetComponent<Camera>();
        }
        UpdateMouseOver();
        //Debug.Log(mouseOver); //board collider -0.08 from edges, change when replacing asset
        if((isWhite) ? isWhiteTurn : !isWhiteTurn)
        {
            int x = (int)mouseOver1.x;
            int y = (int)mouseOver1.y;
            /*if (gameSystem.isAcornEvent)//event (so both players can move) or is white? is white turn else black moves 
            {*/
            //Debug.Log("Select");

            if (isWhiteTurn)
            {
                

                if (selectedPiece != null)
                {
                    UpdatePieceDrag(selectedPiece);
                }

                //if (Input.GetButtonDown("Fire1"))
                //{
                //    SelectPiece(x, y);
                //}

                //if (Input.GetButtonUp("Fire1"))
                //{
                //    TryMove((int)startDrag.x, (int)startDrag.y, x, y);
                //}

                if (playerInput.actions["Action"].WasPerformedThisFrame() && selectedPiece == null)
                {
                    SelectPiece(x, y);
                    return;
                }

                if (playerInput.actions["Action"].WasPerformedThisFrame())
                {
                    TryMove((int)startDrag.x, (int)startDrag.y, x, y);
                }

                if (playerInput2 != null )
                {
                    if (playerInput2.actions["Callout"].WasPerformedThisFrame() && gameSystem.isAcornEvent)
                    {
                        if (hasCheated)
                        {
                            CallOutCheat();
                            gameSystem.isAcornEvent = false;
                            EndTurn();
                            hasCheated = false; 
                        }
                        else
                        {
                            blackPlayerSystem.TakeDamage(1);
                            Debug.Log("Red not cheating!");
                            gameSystem.isAcornEvent = false;
                        }
                    }
                }
            }
            else
            {
                if (GameObject.Find("Opponent(Clone)") != null)
                    playerInput2 = GameObject.Find("Opponent(Clone)").GetComponent<PlayerInput>();

                int x2 = (int)mouseOver2.x;
                int y2 = (int)mouseOver2.y;

                if (selectedPiece != null)
                {
                    UpdatePieceDrag(selectedPiece);
                }

                //if (Input.GetButtonDown("Fire2"))
                //{
                //    SelectPiece(x, y);
                //    Debug.Log("Pressed");
                //}

                //if (Input.GetButtonUp("Fire2"))
                //{
                //    TryMove((int)startDrag.x, (int)startDrag.y, x, y);
                //}

                if (playerInput2.actions["Action"].WasPerformedThisFrame() && selectedPiece == null)
                {
                    SelectPiece(x2, y2);
                    return; 
                }

                if (playerInput2.actions["Action"].WasPerformedThisFrame())
                {
                    TryMove((int)startDrag.x, (int)startDrag.y, x2, y2);
                }

                if (playerInput.actions["Callout"].WasPerformedThisFrame() && gameSystem.isAcornEvent)
                {
                    if (hasCheated)
                    {
                        CallOutCheat();
                        gameSystem.isAcornEvent = false;
                        EndTurn();
                        hasCheated = false; 
                    }
                    else
                    {
                        whitePlayerSystem.TakeDamage(1);
                        Debug.Log("Blue not cheating!");
                        gameSystem.isAcornEvent = false;
                    }
                }
            }
        //}

            //timer stuff
            if (gameTimer.GetTimer() <= 0)
            {
                if (selectedPiece != null )
                {
                        TryMove((int)startDrag.x, (int)startDrag.y, x, y);
                        return;
                }
                EndTurn();
            }
           
            
        }
        
    }

    /*private void UpdateMouseOver()
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
    }*/

    
    private void UpdateMouseOver()
    {
        if (cursor2 == null && GameObject.Find("Opponent(Clone)") != null) //todo change this to stop exceptions
        {
            cursor2 = GameObject.Find("Opponent(Clone)").GetComponentInChildren<CursorMovement>().gameObject.transform;
        }

        //if player turn
        if (!mainCamera)
        {
            Debug.Log("No main camera found");
            return;
        }
        if (isWhiteTurn)
        {

            RaycastHit hit;
            if (Physics.Raycast(cursor1.position, -Vector3.up, out hit, 25.0f, LayerMask.GetMask("Board"))) //if the mouse is on board
            {
                mouseOver1.x = (int)(hit.point.x - boardOffset.x); //int so it snaps to a decimal point
                mouseOver1.y = (int)(hit.point.z - boardOffset.z); // on z since board is on floor not wall
                //Debug.Log("hit X: " + mouseOver.x + " hit Y: " + mouseOver.y);
            }
            else
            {
                mouseOver1.x = -1;
                mouseOver1.y = -1;
            }
        }
        else
        {
            RaycastHit hit;
            if (Physics.Raycast(cursor2.position, -Vector3.up, out hit, 25.0f, LayerMask.GetMask("Board"))) //if the mouse is on board
            {
                mouseOver2.x = (int)(hit.point.x - boardOffset.x); //int so it snaps to a decimal point
                mouseOver2.y = (int)(hit.point.z - boardOffset.z); // on z since board is on floor not wall
                //Debug.Log("hit X: " + mouseOver.x + " hit Y: " + mouseOver.y);
            }
            else
            {
                mouseOver2.x = -1;
                mouseOver2.y = -1;
            }
        }
    }
    private void UpdatePieceDrag(Piece p) //lift up piece
    {
        
        if (!mainCamera)
        {
            Debug.Log("No main camera found");
            return;
        }
        if (isWhiteTurn)
        {

            RaycastHit hit;
            if (Physics.Raycast(cursor1.position, -Vector3.up, out hit, 25.0f, LayerMask.GetMask("Board")))
            {
                p.transform.position = hit.point + Vector3.up;
            }
        }
        else
        {
            RaycastHit hit;
            if (Physics.Raycast(cursor2.position, -Vector3.up, out hit, 25.0f, LayerMask.GetMask("Board")))
            {
                p.transform.position = hit.point + Vector3.up;
            }
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
        if (p != null && p.isWhite == isWhite)
        {
         if(isWhiteTurn)
               {
            selectedPiece = p;
            startDrag = mouseOver1;

            }
            else
            {
                selectedPiece = p;
                startDrag = mouseOver2;


            }

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
                MovePiece(selectedPiece, x1, y1, false);//return piece to initial pos

            ResetMove();
            return;
        }
        //check if selected piece
        if(selectedPiece != null)
        {
            //if piece didn't move
            if(endDrag == startDrag)
            {
                MovePiece(selectedPiece, x1, y1, false);
                ResetMove();
                return;
            }
        }

        //check if move valid
        
            if (selectedPiece.ValidMove(pieces, x1, y1, x2, y2))
            {
                if (gameSystem.isAcornEvent)
                    hasCheated = true;
                else
                    hasCheated = false; 
                //was anything killed
                //if the move involves a capture

                if (Mathf.Abs(x2 - x1) == 2)
                {
                    Piece p = pieces[(x1 + x2) / 2, (y1 + y2) / 2];
                    if (p != null)
                    {
                        
                        if(p.isWhite == true)  //jason's edit starts
                        {
                            whitePieceLeft--;
                             if (whitePieceLeft<=3){
                                gameSystem.PlayEndgamePhase();
                            }

                            //Debug.Log("White Piece" + whitePieceLeft.ToString()+" left");
                        }
                        else
                        {
                            //Debug.Log("Black Piece" + blackPieceLeft.ToString()+" left");
                            blackPieceLeft--;
                             if(blackPieceLeft<=3){
                                gameSystem.PlayEndgamePhase();
                            }
                        }                      //jason's edit ends
                        pieces[(x1 + x2) / 2, (y1 + y2) / 2] = null;
                        Destroy(p.gameObject);
                        hasKilled = true;
                    }
                }

                // Update board state
                pieces[x2, y2] = selectedPiece;
                pieces[x1, y1] = null;
                MovePiece(selectedPiece, x2, y2, true);

                if (hasKilled && CanContinueJump(selectedPiece, x2, y2)|| gameSystem.isAcornEvent)//Does not end turn
                {
                    startDrag = new Vector2(x2, y2); //update start pos
                    selectedPiece = null; 
                    return;
                }
                else
                {
                    EndTurn();
                }
            }
            else //if move is not valid, reset
            {
                MovePiece(selectedPiece, x1, y1, false);
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
                //change mesh

                selectedPiece.transform.Rotate(Vector3.right * 180);  //if rotate
            }
            else if(!selectedPiece.isWhite && !selectedPiece.isKing && y == 0)
            {
                selectedPiece.isKing = true;
                //change mesh

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
        if (mainCamera == p1Camera)
        {
            mainCamera = p2Camera;
        }
        else
        {
            mainCamera = p1Camera;
        }
        CheckVictory();
        gameTimer.ResetTimer();
        Debug.Log("Turn Ended");
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
            StartCoroutine(Victory(false));
        }
        if (!hasBlack)
        {
            StartCoroutine(Victory(true));
        }
    }
    IEnumerator Victory(bool isWhite)
    {
        if (isWhite)
        {
            Debug.Log("White team has won");
            whitePlayerSystem.winState = Player_HealthSystem.winOrLose.Win;
            blackPlayerSystem.winState = Player_HealthSystem.winOrLose.Lose;
            yield return new WaitForSeconds(2.0f);
            // shows gameover UI
            gameSystem.PlayGameoverPhase();
        }
        else
        {
            Debug.Log("Black team has won");
            whitePlayerSystem.winState = Player_HealthSystem.winOrLose.Lose;
            blackPlayerSystem.winState = Player_HealthSystem.winOrLose.Win;
            yield return new WaitForSeconds(2.0f);
            // shows gameover UI
            gameSystem.PlayGameoverPhase();
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
        MovePiece(p,x,y,false);
    }
    private void MovePiece(Piece p, int x, int y, bool anim)
    {
        p.transform.position = (Vector3.right * x) + (Vector3.forward * y) + boardOffset + pieceOffset;

        if(p.isWhite == true && anim ==true)
        {
            StartCoroutine(WhiteIsMoveing());
            checkerAudioSource.clip = placingPiece;
            checkerAudioSource.pitch = UnityEngine.Random.Range(1.0f , 1.5f);
            checkerAudioSource.Play();
        }

        else if(p.isWhite == false && anim ==true)
        {
             StartCoroutine(BlackIsMoveing());
             checkerAudioSource.clip = placingPiece;
             checkerAudioSource.pitch = UnityEngine.Random.Range(1.0f , 1.5f);
             checkerAudioSource.Play();
        }

    }

    public bool IsPieceSelected()
    {
        if (selectedPiece == null)
            return false;
        else
            return true;
    }

    public bool IsWhiteTurn()
    {
        return isWhiteTurn; 
    }

    ///////////////////////
    //cheating mechanics
    //////////////////////

    //save state
    public void SaveBoardState()
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
                if (pieces[x, y] != null)
                {
                    MovePiece(pieces[x, y], x, y, false);
                }
            }
        }
    }
    private void CallOutCheat()
    {
        if (gameSystem.isAcornEvent)
        {
            RestoreBoardState();
            gameSystem.isAcornEvent = false;
            Debug.Log("Cheater! Board Restored");
        }
    }


    //Calling Player "MovePiece" Animations//
    IEnumerator WhiteIsMoveing(){
        whitePlayerSystem.isMovingPiece = true;
        yield return new WaitForSeconds(2.0f);
        whitePlayerSystem.isMovingPiece = false;

    }

    IEnumerator BlackIsMoveing(){
        blackPlayerSystem.isMovingPiece = true;
        yield return new WaitForSeconds(2.0f);
        blackPlayerSystem.isMovingPiece = false;

    }
}
