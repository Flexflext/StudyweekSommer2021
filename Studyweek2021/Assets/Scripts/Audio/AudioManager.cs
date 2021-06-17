using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using Random = UnityEngine.Random;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;


    public float MasterVolume;

    [SerializeField] private AudioMixer MasterMixer;
    [SerializeField] private AudioMixerGroup mixerGroup;

    public Sound[] sounds;

    //[Header("Arrays for Random Sounds")]
    

    private void Awake()
    {
        if (AudioManager.Instance == null)
        {
            AudioManager.Instance = this;
        }
        else if (AudioManager.Instance != this)
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this);

        // Initializes the array of Sounds with the Different Values of the Inspector
        foreach (Sound s in sounds)
        {
            s.Source = this.gameObject.AddComponent<AudioSource>();
            s.Source.outputAudioMixerGroup = mixerGroup;

            s.Source.clip = s.Clip;
            s.Source.volume = s.Volume;
            s.Source.pitch = s.Pitch;
            s.Source.loop = s.Loop;

            s.Source.playOnAwake = false;
        }
    }

    private void Start()
    {
        // Sets the Volume of the Mixer with the MasterVolume float
        MasterMixer.SetFloat("MasterVolume", Mathf.Log10(MasterVolume) * 20);
    }

    /// <summary>
    /// Plays a Sound in the Sound array by Index
    /// </summary>
    /// <param name="_audioname"></param>
    public void Play(string _audioname)
    {
        Sound s = System.Array.Find(sounds, sound => sound.Name == _audioname);
        s.Source.Play();
    }

    /// <summary>
    /// Plays a Random AudioClip in the given Source by Index
    /// </summary>
    /// <param name="_audioname"></param>
    /// <param name="_audioClipArraySound"></param>
    public void PlayRandom(string _audioname, string _audioClipArraySound)
    {
        Sound s = System.Array.Find(sounds, sound => sound.Name == _audioname);

        AudioClip clip = null;

        //Choses the Array with the Random Sound given by a String
        



        // Replaces the audioClip in the Souce with the Random one that has been chosen
        if (clip != null)
        {
            s.Source.clip = clip;
        }

        s.Source.Play();
    }

    /// <summary>
    /// Play an AudioClip if another Clip isnt Playing
    /// </summary>
    /// <param name="_audioname"></param>
    /// <param name="_audioClipArraySound"></param>
    public void PlayRandomIfNotPlaying(string _audioname, string _audioClipArraySound)
    {
        Sound s = System.Array.Find(sounds, sound => sound.Name == _audioname);

        AudioClip clip = null;

        // Check if another Clip isnt already Playing
        if (s.Source.isPlaying)
        {
            return;
        }

        // Change Clip To Random Clip
        

        // Replaces the audioClip in the Souce with the Random one that has been chosen
        if (clip != null)
        {
            s.Source.clip = clip;
        }

        s.Source.Play();
    }

    /// <summary>
    /// Stop the Audio from a Source by String Index
    /// </summary>
    /// <param name="_audioname"></param>
    public void Stop(string _audioname)
    {
        Sound s = System.Array.Find(sounds, sound => sound.Name == _audioname);
        s.Source.Stop();
    }

    /// <summary>
    /// Set the Master Volume and the value in the Mixer
    /// </summary>
    /// <param name="_volume"></param>
    public void SetMasterVolume(float _volume)
    {
        MasterVolume = _volume;
        MasterMixer.SetFloat("MasterVolume", Mathf.Log10(MasterVolume) * 20);
    }

    /// <summary>
    /// Stops all Playing Sounds
    /// </summary>
    public void StopAllSounds()
    {
        foreach (Sound mySound in sounds)
        {
            Stop(mySound.Name);
        }
    }

    /// <summary>
    /// Sets Volume of Given Sound to Value
    /// </summary>
    /// <param name="_audioname"></param>
    /// <param name="_value">Value Between 0 and 1</param>
    public void ChangeVolumeOnMixer(string _audioname, float _value)
    {
        Sound s = System.Array.Find(sounds, sound => sound.Name == _audioname);
        //_value = Mathf.Clamp01(_value);
        s.Volume = _value;
        s.Source.volume = s.Volume;
    }
}
