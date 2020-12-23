using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class SMFlow : SerializedMonoBehaviour
{
    //2D list to accomodate alt takes of same line/scenario
    [SerializeField] List<List<AudioClip>> storyClips = new List<List<AudioClip>>();
    [SerializeField] List<List<AudioClip>> gateClips = new List<List<AudioClip>>();
    [SerializeField] List<List<AudioClip>> intClips = new List<List<AudioClip>>();
    [SerializeField] int story_i, story_j, interruptIndex, gatewayIndex = 0;
    [SerializeField] string choiceStateKey;
    [SerializeField] GameState gameState;
    AudioClip next;
    public AudioClip GetNext()
    {
        Debug.Log("glipType: " + gameState.clipType);
        //if (gameState.clipType == 0)    //if last clip finished was story
        //    story_i++;

        Debug.Log(story_i);
        Debug.Log(storyClips[story_i].Count);
        if (story_i < storyClips[story_i].Count)
        {
            next = storyClips[story_i][story_j];
            story_j = 0;
        }
        Debug.Log("Returning storyClip: " + next);
        return next;
    }
    public AudioClip GetInterrupt()
    {
        if (story_i < intClips.Count)
             next = intClips[story_i][story_j];
        return next;
    }
    public AudioClip GetGateway()
    {
        if (story_i < intClips.Count)
            next = gateClips[story_i][story_j];
        return next;
    }

    public bool HasNext()
    {
        if (story_i < storyClips[story_i].Count)
        {
            Debug.Log("HasNext() returned true");
            return true;
        }
        return false;
    }
    //[SerializeField] StoryModule SM;
    //[SerializeField] public Dictionary<int, bool> interruptableClips = new Dictionary<int, bool>();
    //[SerializeField] AudioSource player = new AudioSource();
    //[SerializeField] Dictionary<string, AudioClip> customRes = new Dictionary<string, AudioClip>();

    //[SerializeField] InterruptionResponses genericRes;

    //[SerializeField] bool isFinished, isInterrupted, isGateway = false;
    //public bool isInterruptable = true;

    //[SerializeField] SMFlow modFlow;

    //public void Update()
    //{
    //    //If clip is finished and isn't last clip
    //        //if (ShouldLoadNext())
    //        //{
    //        //    PlayNext();
    //        //}

    //    //If was interrupted and has finished response
    //    /*else*/ if (isInterrupted && !player.isPlaying)
    //    {
    //        Debug.Log("Load Gateway Bridge:" + genericRes.gateways[gatewayIndex]);
    //        isInterrupted = false;
    //        isGateway = false;
    //        //If all gateways exhausted restart from beginning
    //        if (gatewayIndex >= genericRes.gateways.Count)
    //            gatewayIndex = 0;

    //        player.clip = genericRes.gateways[gatewayIndex];
    //        Play();
    //    }

    //}
    //public void Play()
    //{
    //    //if current story clip is loaded and interruptableDictionary returns true value for current clipIndex, allow interrupts
    //    if (player.clip == clips[clipIndex][0] && interruptableClips[clipIndex])    //[0] temp
    //        isInterruptable = true;
    //    else
    //        isInterruptable = false;

    //    Debug.Log("Starting clip" + this.name);
    //    player.Play();
    //}
    //bool ShouldLoadNext()
    //{
    //    //if no interruptions or gateways playing & player isn't playing (gateway finished) & all clips not played
    //    return (!isInterrupted && !isGateway && !player.isPlaying && clips.Count > clipIndex);
    //}
    //public void PlayNext()
    //{
    //    if (ShouldLoadNext()){
    //        //If previously completed clip was core
    //        if (player.clip == clips[clipIndex][0])
    //            clipIndex++;

    //        if (clips.Count > clipIndex)
    //        {
    //            Debug.Log("Playing Story Mod: " + this.name + " clip: " + clips[clipIndex]);
    //            player.clip = clips[clipIndex][0]; //load next clip to be played
    //            Play();
    //        }
    //        else
    //        {
    //            Debug.Log(this.name + ": All clips played");
    //            SM.SetFinished();
    //            this.enabled = false;   //All core clips played. Disable for performance
    //        }
    //    }
    //}


    //public void Interrupt()
    //{
    //    //Debug.Log("Stopped " + this.name + "at clip: " + clips[clipIndex]);
    //    player.Stop();
    //    HandleInterrupt();
    //}
    //private void HandleInterrupt()
    //{
    //    //If custom interrupt response found and haven't exhausted all custom resonss play next res
    //    if (customRes.Count > 0 && interruptIndex >= customRes.Count)
    //        ; //player.clip = customRes[interruptIndex]; 

    //    //else take generic interrupt
    //    else
    //    {
    //        if (interruptIndex >= genericRes.interruptions.Count)   //If all generic responses exhausted, reset index
    //            interruptIndex = 0;

    //        player.clip = genericRes.interruptions[interruptIndex];
    //        interruptIndex++;
    //    }

    //    isInterrupted = true;   //indicate to let current clip play through and then resume pre-interrupt
    //    Play();
    //}


    //public int getClipIndex()
    //{
    //    return clipIndex;
    //}
    //public bool IsFinished()
    //{
    //    return isFinished;
    //}
}


