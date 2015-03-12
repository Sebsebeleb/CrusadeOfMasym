using UnityEngine;
using System.Collections;

public class OptionsBox : MonoBehaviour
{
    public float musicSliderValue = 0.5f;
    public static float soundSliderValue = 0.5f;
    public bool ShowGUI = false;
    private bool MusicEnabled = true;
    public static bool StopMusic = false;
    public static bool SoundEnabled = true;

    private Rect windowRect = new Rect(800, 500, 300, 200);

    void Awake() 
    {
        DontDestroyOnLoad(transform.gameObject);
    }

    public void OnButtonCallback(string id)
    {
        if (id == "Options")
        {
            ShowGUI = !ShowGUI;
        }
    }

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
        if (GUI.Button(new Rect(40, 140, 100, 30), "Test Sound")) ; //Create Thingy With Sound?//

    }

    void Update()
    {
        if (!MusicEnabled) GetComponent<AudioSource>().mute = true;
        else GetComponent<AudioSource>().mute = false;
        if (Input.GetKeyDown(KeyCode.Escape)) ShowGUI = !ShowGUI;
        if (StopMusic)
        {
            GetComponent<AudioSource>().Stop();
            StopMusic = false;
        }
    }

}
