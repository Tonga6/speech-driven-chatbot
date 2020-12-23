using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
public class InputHandler : SerializedStateMachineBehaviour
{
    [SerializeField] GameState gameState;
    [SerializeField] List<List<string>> branches = new List<List<string>>();    //branches[x][0] = fsm string condition to set true
    [SerializeField] string input = null;
    [SerializeField] bool inputChecked = false;
    int branch_i = 0;
    int input_j = 0;
    private void Awake()
    {
    }
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        inputChecked = false;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (branches.Count != 0)
        {
            input = gameState.GetInput();        
            if (input != null)
            {
                analyseInput(animator);
            }
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetInteger("branchSelector", -1);
        Debug.Log("Resetting branchSelector to -1");
        inputChecked = false;
        Debug.Log("Resetting inputChecked to false");
    }
    private void analyseInput(Animator animator)
    {   //i = branch. j = Possible inputs which lead to branch [i]. j = 0 for parameter to set for fsm state transition.
        Debug.Log("Analysing input: " + input);
        for (int i = 0; i < branches.Count && ! inputChecked; i++)
        {
            for (int j = 0; j < branches[i].Count && !inputChecked; j++)
            {
                if (input.Contains(branches[i][j]))
                {
                    //Debug.Log("Setting bool:" + branches[i][0]);
                    //animator.SetBool(branches[i][0], true);
                    //Debug.Log("Setting trigger: " + branches[i][0]);
                    //animator.SetTrigger(branches[i][0]);
                    Debug.Log("Setting branchSelector to: " + i);
                    animator.SetInteger("branchSelector", i);

                    //hack to get out of loop (Only one response allowed)
                    return;
                }
            }
        }
        Debug.Log("Setting branchSelector to: -2");
        animator.SetInteger("branchSelector", -2);  //invalid or undefined input received
    }
}
