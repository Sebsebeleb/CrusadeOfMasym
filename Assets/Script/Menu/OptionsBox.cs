using UnityEngine;
using System.Collections;

public class OptionsBox : MonoBehaviour
{
    public float musicSliderValue = 0.5f;
    public static float soundSliderValue = 0.5f;
    public static bool ShowGUI = false;
    private bool MusicEnabled = true;
    public static bool StopMusic = false;
    public static bool SoundEnabled = true;

    private AudioSource audio;
    public AudioClip Menu;
    public AudioClip Story;

    public static bool PlayStoryMusic = false;
    public static bool PlayMenuMusic = false;


    private Rect windowRect = new Rect(800, 500, 300, 200);

    void Awake() 
    {
        DontDestroyOnLoad(transform.gameObject);
        audio = GetComponent<AudioSource>();
        PlayMenuMusic = true;
    }

    /*public void OnButtonCallback(string id)
    {
        if (id == "Options")
        {
            ShowGUI = !ShowGUI;
        }
    }*/

    void OnGUI()
    {
        if (!ShowGUI) return;
        windowRect = GUI.Window(0, windowRect, WindowFunction, "Options");
    }

    void WindowFunction(int windowID)
    {
        musicSliderValue = GUI.HorizontalSlider(new Rect(35, 55, 100, 30), musicSliderValue, 0.0f, 1.0f);
        musicSliderValue = musicSliderValue * 100;
        GUI.Label(new Rect(160, 50, 200, 20), "Music Volume = " + Mathf.Round(musicSliderValue)+"%");
        musicSliderValue = musicSliderValue / 100;
        GetComponent<AudioSource>().volume = musicSliderValue;

        MusicEnabled = GUI.Toggle(new Rect(24, 25, 100, 30), MusicEnabled, "Music Enabled");

        soundSliderValue = GUI.HorizontalSlider(new Rect(35, 105, 100, 30), soundSliderValue, 0.0f, 1.0f);
        soundSliderValue = soundSliderValue * 100;
        GUI.Label(new Rect(160, 100, 200, 20), "Sound Volume = " + Mathf.Round(soundSliderValue) + "%");
        soundSliderValue = soundSliderValue / 100;

        SoundEnabled = GUI.Toggle(new Rect(24, 75, 105, 30), SoundEnabled, "Sound Enabled");
        if(GUI.Button(new Rect(165, 140, 100, 30), "Close Options")) ShowGUI=!ShowGUI;
        if (GUI.Button(new Rect(40, 140, 100, 30), "Main Menu")) { if (Application.loadedLevel > 0) { Application.LoadLevel(0); PlayMenuMusic = true; ShowGUI = !ShowGUI; } }

    }

    void AudioManager()
    {
        if (PlayMenuMusic)
        {
            audio.Stop();
            audio.pitch = 0.8f;
            audio.clip = Menu;
            audio.Play();
            PlayMenuMusic = false;
        }
        else if (PlayStoryMusic)
        {
            audio.Stop();
            audio.pitch = 1f;
            audio.clip = Story;
            audio.Play();
            PlayStoryMusic = false;
        }
        if (!MusicEnabled) audio.mute = true;
        else audio.mute = false;
        if (Input.GetKeyDown(KeyCode.Escape)) ShowGUI = !ShowGUI;
        if (StopMusic)
        {
            audio.Stop();
            StopMusic = false;
        }
    }

    void Update()
    {
        AudioManager();
        Debug.Log(Application.loadedLevel);
    }

}
