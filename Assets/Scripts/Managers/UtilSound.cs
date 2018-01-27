using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UtilSound : MonoBehaviour
{

    const string DEFAULT_SOUNDS_PATH = "Audio/Sounds/";

    public AudioClip[] clips;

    public static UtilSound instance = null;

    private List<GameObject> sounds = null;
    private Dictionary<string, AudioClip> clipsDictionary;

    private bool _focus = true;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
            sounds = new List<GameObject>();
            clipsDictionary = new Dictionary<string, AudioClip>();
            foreach (AudioClip ac in clips)
            {
                clipsDictionary.Add(ac.name, ac);
            }
            _focus = true;
        }
        else
        {
            Destroy(this.gameObject); // Destroy the newest UtilSound instance
        }
    }

    private void Update()
    {
        if (sounds == null) { return; }
        for (int i = 0; i < sounds.Count; ++i)
        { // Check every playing sound
            if (!sounds[i].GetComponent<AudioSource>().isPlaying && _focus)
            { // If the sound exists
                Destroy(sounds[i]); // Destroy the AudioSource
                sounds.RemoveAt(i); // Remove from the list
            }
        }
    }

    public void PlaySound(string name, float volume = 1.0f, bool loop = false)
    {
        string path = DEFAULT_SOUNDS_PATH + name;
        //AudioClip clip = Resources.Load<AudioClip>(path); // Load sound from disk
        AudioClip clip = clipsDictionary[name];
        if (clip == null) { Debug.LogError("[UtilSound] Error. Clip " + path + " was no found"); return; } // Exit of the sound was not found
        GameObject newObject = new GameObject(); // New scene object
        AudioSource newSource = newObject.AddComponent<AudioSource>(); // Create a new AudioSouce and set it to the new object
        newObject.transform.parent = gameObject.transform; // UtilSound is the parent of the new object
        newObject.name = name; // Assign the given clip name
        newSource.clip = clip; // Assign clip to new AudioSource
        newSource.volume = volume;
        newSource.loop = loop; // Assign given loop property
        newSource.Play(); // Play the sound
        sounds.Add(newObject); // Store the new AudioSource
    }

    public void StopSound(string name)
    {
        if (sounds == null) { return; }
        for (int i = 0; i < sounds.Count; ++i)
        { // Check every playing sound
            if (sounds[i].name == name)
            { // If the sound exists
                Destroy(sounds[i]); // Destroy the AudioSource
                sounds.RemoveAt(i); // Remove from the list
                break; // Just the oldest sound with that name
            }
        }
    }

    public void StopAllSounds()
    {
        if (sounds == null) { return; }
        for (int i = 0; i < sounds.Count; ++i)
        { // Check every playing sound
            Destroy(sounds[i]); // Destroy the AudioSource
            sounds.RemoveAt(i); // Remove from the list
        }
    }

    public bool IsPlaying(string name)
    {
        if (sounds == null) { return false; }
        for (int i = 0; i < sounds.Count; ++i)
        { // Check every playing sound
            if (sounds[i].name == name)
            { // If the sound exists
                return true; // It Is playing
            }
        }
        return false; // Not found. It is not playing
    }

    public bool IsPlayingFamilySound(string name)
    {
        if (sounds == null) { return false; }
        for (int i = 0; i < sounds.Count; ++i)
        { // Check every playing sound
            if (sounds[i].name.Contains(name))
            { // If the sound exists
                return true; // It Is playing
            }
        }
        return false; // Not found. It is not playing
    }

    void OnApplicationFocus(bool focus)
    {
        _focus = focus;
    }
}