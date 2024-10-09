using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_HealthSystem : MonoBehaviour
{
    public enum characterState {Idle = 0, KnockDown = 1, Died = 2};
    public enum winOrLose {Draw = 0, Win = 1, Lose = 2};

    [SerializeField]
    private int maxHP = 3;
    public int currentHP;

    public characterState state = characterState.Idle;
    public winOrLose winState = winOrLose.Draw;

    [SerializeField] public Player_HealthSystem opponentSystem;

    [SerializeField] private GameSystemHandler gameSystemREF;

    public bool isMovingPiece = false;

    private Player_Animation playerAnim;

    [SerializeField] private AudioSource characterAudioSource;
    [SerializeField] private AudioClip getHit;
    [SerializeField] private AudioSource UIAudioSource;
    [SerializeField] private AudioClip lifeLose;

    private void Awake()
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        if (this.gameObject.name == "Player" && GameObject.Find("Opponent(Clone)") != null)
        {
            opponentSystem = GameObject.Find("Opponent(Clone)").GetComponent<Player_HealthSystem>();
        }
        else if (GameObject.Find("Opponent(Clone)") != null)
        {
            opponentSystem = GameObject.Find("Player").GetComponent<Player_HealthSystem>();
        }

        gameSystemREF = GameSystemHandler.instance; 

        playerAnim = GetComponent<Player_Animation>();
        currentHP = maxHP;
    }

    // Update is called once per frame
    void Update()
    {
        AnimatePlayer();

        if (this.gameObject.name == "Player" && GameObject.Find("Opponent(Clone)") != null)
        {
            opponentSystem = GameObject.Find("Opponent(Clone)").GetComponent<Player_HealthSystem>();
        }
    }

    public void TakeDamage (int healthDamage){
        currentHP = currentHP - healthDamage;
        //Debug.Log("Player is damaged.");
        characterAudioSource.clip = getHit;
        characterAudioSource.pitch = UnityEngine.Random.Range(1.0f , 1.5f);
        characterAudioSource.Play();
        UIAudioSource.clip = lifeLose;
        UIAudioSource.Play();
        state = characterState.KnockDown;
        StartCoroutine(Recovering());
        if(currentHP<=1){
            gameSystemREF.PlayEndgamePhase();
        }
        StartCoroutine(CheckDealth()); 

    }

    IEnumerator Recovering(){
        yield return new WaitForSeconds(3.0f);
        if(state == characterState.KnockDown){
            state = characterState.Idle;
        }
    }

    IEnumerator CheckDealth (){
        if (currentHP <= 0){


            state = characterState.Died;
            winState = winOrLose.Lose;
            opponentSystem.winState = Player_HealthSystem.winOrLose.Win;
            yield return new WaitForSeconds(2.0f);
            // shows gameover UI
            gameSystemREF.PlayGameoverPhase();
        }
    }

    void AnimatePlayer(){
        playerAnim.Play_State((int)state);
        playerAnim.Play_MovePiece(isMovingPiece);
        playerAnim.Play_WinOrLose((int)winState);
      }

}
