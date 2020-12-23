using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Sirenix.OdinInspector;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/GameState", order = 2)]
public class GameState : SerializedScriptableObject
{
    //string = identifier of question's answer being stored ("hasDreamtOfHeaven")
        //could replace with int indicating which story module which preceded the answer
    //int = index of answer given (0 = Yes, 1 = No, 2 = Other)
    public Dictionary<string, int> choiceDict = new Dictionary<string, int>();
    public int clipType = -1;    // 0 = story, 1 = interrupt, 2 = gateway
    public string playerIn = null;
    public bool hasNewInput, isOliviaSpeaking = false;
    //public delegate void hasInterrupt();
    //public static event hasInterrupt OnInterrupt;

    public List<AudioClip> invalidAnswer = new List<AudioClip>();
    public List<AudioClip> gateways = new List<AudioClip>();
    [SerializeField] int randomIndex = 0;
    public void SetInput(string input)
    {
        Debug.Log("setting player input to: " + input);
        playerIn = input;
        hasNewInput = true;
    }
    public bool hasInput()
    {
        return playerIn != null;
    }
    public string GetInput()
    {
            return playerIn;

    }

    public AudioClip GetInvalidResponse()
    {
        randomIndex = Random.Range(0, invalidAnswer.Count - 1);
        return invalidAnswer[randomIndex];
    }

    public AudioClip GetGateway()
    {
        randomIndex = Random.Range(0, gateways.Count - 1);
        return gateways[randomIndex];
    }
}
