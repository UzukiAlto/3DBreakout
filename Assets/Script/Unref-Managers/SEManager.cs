using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SEManager : MonoBehaviour
{
    public AudioSource seAudioSource;
    public AudioClip sound_ball;
    public AudioClip sound_fail;
    public AudioClip sound_next;
    public AudioClip sound_transition;
    public AudioClip sound_select;
    public enum SoundName {
        ball, fail, next, transition, select
    }
    // Start is called before the first frame update
    void Start()
    {
    }
    public void PlaySE(SoundName sound)
    {
        AudioClip audio;
        switch (sound)
        {
            case SoundName.ball:
                audio = sound_ball;
                break;
            case SoundName.fail:
                audio = sound_fail;
                break;
            case SoundName.next:
                audio = sound_next;
                break;
            case SoundName.transition:
                audio = sound_transition;
                break;
            case SoundName.select:
                audio = sound_select;
                break;
            default:
                audio = null;
                break;
        }
        seAudioSource.PlayOneShot(audio);
    }
}
