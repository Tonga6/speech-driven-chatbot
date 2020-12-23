using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class StoryModule : SerializedMonoBehaviour
{
    [SerializeField] GameState gameState;
    [SerializeField] AudioSource player = new AudioSource();
    [SerializeField] bool isFinished, isInterrupted, isGateway, isWaiting = false;
    public bool isInterruptable = true;
    public int branchSelect = 0;
    [SerializeField] SMFlow SMFlow;

    [SerializeField] AudioClip nextClip;

    private void Update()
    {
        if (!player.isPlaying && !isWaiting)
        {
            Debug.Log("No clip playing");
            //If interruption is finished
            if (isInterrupted)
            {
                isInterrupted = false;
                gameState.clipType = 3; //gateway
                nextClip = LoadGateway();
            }
            //If gateway is finished or just iterating through story clips
            else 
            {
                if (isGateway)
                    isGateway = false;

                if (SMFlow.HasNext())
                {
                    Debug.Log("Final story clip not played. Begin Load next proc");
                    gameState.clipType = 0; //story
                    nextClip = LoadStory();      
                }
            }
            
            PlayNext();        
        }
    }
    public void PlayNext()
    {
        player.clip = nextClip;
        player.Play();
    }
    public void Interrupt()
    {
        gameState.clipType = 1; //interrupt
        nextClip = LoadInterrupt();
    }
    public AudioClip LoadStory()
    {
        Debug.Log("Loading next story clip");
        return SMFlow.GetNext();        
    }
    private AudioClip LoadInterrupt()
    {
        return SMFlow.GetInterrupt();
    }
    private AudioClip LoadGateway()
    {
        return SMFlow.GetGateway();
    }


    public bool IsFinished()
    {
        return isFinished;
    }
    public void SetFinished()
    {
        isFinished = true;
    }
    public bool IsInterruptable()
    {
        return isInterruptable;
    }







    //[SerializeField] public Dictionary<int, bool> interruptableClips = new Dictionary<int, bool>();

    //[SerializeField] List<AudioClip> clips = new List<AudioClip>();
    //[SerializeField] Dictionary<string, AudioClip> customRes = new Dictionary<string, AudioClip>();


    //[SerializeField] int clipIndex, interruptIndex, gatewayIndex = 0;

    //public void Update()
    //{        
    //    //If clip is finished and isn't last clip
    //    if (ShouldLoadNext())
    //    {
    //        PlayNext();
    //    }

    //    //If was interrupted and has finished response
    //    else if (isInterrupted && !player.isPlaying)
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
    //    if (player.clip == clips[clipIndex] && interruptableClips[clipIndex])
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
    //    //If previously completed clip was core
    //    if (player.clip == clips[clipIndex])
    //        clipIndex++;

    //    if (clips.Count > clipIndex)
    //    {
    //        Debug.Log("Playing Story Mod: " + this.name + " clip: " + clips[clipIndex]);
    //        player.clip = clips[clipIndex]; //load next clip to be played
    //        Play();                
    //    }
    //    else
    //    {
    //        Debug.Log(this.name + ": All clips played");
    //        isFinished = true;
    //        this.enabled = false;   //All core clips played. Disable for performance
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


    //public int getClipIndex() {
    //    return clipIndex;
    //}
}
