using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSoundFX : MonoBehaviour
{
    AudioSource audioSource;
    Dictionary<string, AudioClip[]> clips;
    [SerializeField] AudioClip[] footStepsRoad;
    [SerializeField] AudioClip[] footStepsSand;
    [SerializeField] AudioClip[] footStepsSnow;
    [SerializeField] AudioClip[] footStepsWater;
    [SerializeField] AudioClip[] footStepsDirt;
    [SerializeField] AudioClip[] footStepsBush;



    private string tagName = "Normal";
    public string TagName
        {
         set => tagName=value;
        }
    // Start is called before the first frame update
    void Start()
    {
        clips = new Dictionary<string, AudioClip[]>()
        {
            {"Road", footStepsRoad},
            {"Sand", footStepsSand},
            {"Snow", footStepsSnow},
            {"Water",footStepsWater},
            {"Dirt" ,footStepsDirt},
            {"Bush" ,footStepsBush} 
        };
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        VolumeControll();
    }

    public void FootSoundFx()
    {
        audioSource.PlayOneShot(clips[tagName][UnityEngine.Random.Range(0, clips[tagName].Length)]);
    }
    public void VolumeControll()
    {
     audioSource.volume = SoundManager.instance.SoundFxVolume;
    }
}
