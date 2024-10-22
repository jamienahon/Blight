using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationSounds : MonoBehaviour
{

    AudioSource Audio;
    // Start is called before the first frame update
    void Start()
    {
        Audio = GetComponent<AudioSource>();
    }


    public void playSound()
    {
        Audio.Play();
        
    }
        
    public void StopSound()
    {
        Audio.Stop();

    }
}
