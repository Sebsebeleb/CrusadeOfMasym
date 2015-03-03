using UnityEngine;
using System.Collections;

public class OptionsBox : MonoBehaviour
{
    public float hSliderValue = 0.5f;
    public bool ShowGUI = false;
    private bool MusicEnabled = true;

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
        windowRect = GUI.Window(0, windowRect, WindowFunction, "My Window");
    }

    void WindowFunction(int windowID)
    {
        hSliderValue = GUI.HorizontalSlider(new Rect(80, 80, 100, 30), hSliderValue, 0.0f, 1.0f);
        hSliderValue = hSliderValue * 100;
        GUI.Label(new Rect(100, 100, 200, 20), "Music Volume = " + Mathf.Round(hSliderValue)+"%");
        hSliderValue = hSliderValue / 100;
        audio.volume = hSliderValue;

        MusicEnabled = GUI.Toggle(new Rect(25, 25, 100, 30), MusicEnabled, "Music Enabled");
    }

    void Update()
    {
        if (!MusicEnabled) audio.mute = true;
        else audio.mute = false;
        if (Input.GetKeyDown(KeyCode.Escape)) ShowGUI = !ShowGUI;
    }

}
