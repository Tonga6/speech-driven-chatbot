//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using Sirenix.OdinInspector;

//public class GameController : SerializedMonoBehaviour
//{
//    [SerializeField] GameState gameState;
//    [SerializeField] VoiceParser parser;
//    AudioListener master;

//    //[x][y], where x = module depth, y = module width(which branch to choose based on player input)
//    [SerializeField] List<List<StoryModule>> modules = new List<List<StoryModule>>();

//    [SerializeField] int modIndex = 0;


//    private void Awake()
//    {
//        gameState.clipType = -1;
//        Debug.Log("Starting story");
//        modules[0][0].LoadStory(); //play next story module
//    }
//    private void Update()
//    {
//        //if module finished but not all modules finished
//        if (modIndex < modules.Count)
//        {
//            if (modules[modIndex][gameState.branchPointer].IsFinished() && modIndex + 1 < modules.Count)
//            {
//                Debug.Log("Loading next Story Mod: " + modules[modIndex][gameState.branchPointer].name);
//                modIndex++;
//                PlayNextMod();
//            }
//        }

//        //If Player interrupts AI
//        if (hasInterruption() && modules[modIndex][gameState.branchPointer].isInterruptable)
//        {
//            Debug.Log("Player interrupted story module:" + modules[modIndex][gameState.branchPointer].name);
//            modules[modIndex][gameState.branchPointer].Interrupt();
//        }

            
//    }

//    void PlayNextMod()
//    {
//        Debug.Log("Starting next mod: " + modules[modIndex][gameState.branchPointer].name);
//        modules[modIndex][gameState.branchPointer].PlayNext();
//    }
//    bool hasInterruption()
//    {
//        return (parser.isPlayerSpeaking && !modules[modIndex][gameState.branchPointer].IsFinished()); //true if player is speaking and story module isn't finished
//    }
//}
