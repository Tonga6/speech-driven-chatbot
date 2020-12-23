using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class ClipHandler : SerializedStateMachineBehaviour
{
    //2D list to accomodate alt takes of same line/scenario
    [SerializeField] List<List<AudioClip>> storyClips = new List<List<AudioClip>>();
    [SerializeField] private static int story_i, story_j= 0;
    [SerializeField] GameState gameState;
    AudioClip next;


    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }


    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
    }

    public AudioClip GetNext()
    {
        Debug.Log(this.name + "story_i: " + story_i);
        if (story_i < storyClips.Count && story_j < storyClips.Count)
        {
            next = storyClips[story_i][story_j];
            story_j = 0;
        }
        return next;
    }
    public AudioClip GetInvalidResponse()
    {
        return gameState.GetInvalidResponse();
    }

    public AudioClip GetGateway()
    {
        return gameState.GetGateway();
    }
    public void AddClip(AudioClip clip)
    {
        
    }

    public bool HasNext()
    {
        if (gameState.clipType == 0)    //if last clip finished was story
            story_i++;
        if (story_i < storyClips.Count)
        {
            return true;
        }
        story_i = 0;
        Debug.Log("story_i: " + story_i);
        return false;
    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
