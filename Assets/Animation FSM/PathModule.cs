using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class PathModule : SerializedStateMachineBehaviour
{
    [SerializeField] GameState gameState;
    public static GameObject GC;
    [SerializeField] AudioSource player = new AudioSource();
    [SerializeField] bool isFinished, incLoopOnFinish, isInterrupted, isGateway, isWaiting = false;
    [SerializeField] float waitTimer = 4;
    // public bool isInterruptable = true;
    [SerializeField] ClipHandler clipBank;
    [SerializeField] InputHandler inHandler;

    [SerializeField] AudioClip nextClip;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        isFinished = false;
        gameState.clipType = -1;
        GC = GameObject.FindWithTag("GameController");
        player = GC.GetComponent<AudioSource>();
        gameState.SetInput(null);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(waitTimer > 0 && isFinished)
            Timer();
        else if (isFinished)
            animator.SetBool("isSilent", true);

        if (!player.isPlaying && !isWaiting && !isFinished)
        {
            HandlePlayer();
        }

        if (isFinished)
        {
            animator.SetBool("isFinished", true);
            if (incLoopOnFinish)
                animator.SetInteger("loopCounter", animator.GetInteger("loopCounter") + 1); //inc loopCounter
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        gameState.SetInput(null);
        Debug.Log("Resetting trigger: isFinished");
        animator.SetBool("isFinished", false);
        animator.SetBool("isSilent", false);
    }
    private void HandlePlayer()
    {
        gameState.isOliviaSpeaking = false;
        Debug.Log("No clip playing");
        //If interruption is finished
        if (isInterrupted)
        {
            isInterrupted = false;
            isGateway = true;
            gameState.clipType = 3; //gateway
            nextClip = LoadGateway();
        }
        //If gateway is finished or just iterating through story clips
        else
        {
            if (isGateway)
                isGateway = false;

            if (clipBank.HasNext())
            {
                gameState.isOliviaSpeaking = true;
                Debug.Log("Final story clip not played. Begin Load next proc");
                nextClip = LoadStory();
                gameState.clipType = 0; //story
            }
            else
            {
                Debug.Log("Final clip is played.");
                gameState.clipType = -1;    //prevent next mod from ++ before first clip
                isFinished = true;

            }
        }
        if (!isFinished)
            PlayNext();
    }
    public void PlayNext()
    {
        player.clip = nextClip;
        Debug.Log("Playing clip: " + player.clip);
        player.Play();
    }
    //public void Interrupt()
    //{
    //    Debug.Log("Interrupt");
    //    gameState.clipType = 1; //interrupt
    //    player.Stop();
    //    nextClip = LoadInterrupt();
    //    isInterrupted = true;
    //    isInterruptable = false;
    //    PlayNext();
    //    Debug.Log("Interupted. Set next clip to: " + nextClip);
    //}
    private AudioClip LoadStory()
    {
        Debug.Log("Loading next story clip");
        return clipBank.GetNext();
    }
    private AudioClip LoadInterrupt()
    {
        return clipBank.GetInvalidResponse();
    }
    private AudioClip LoadGateway()
    {
        return clipBank.GetGateway();
    }
    
    private void Timer()
    {
        waitTimer -= Time.deltaTime;
    }
}
