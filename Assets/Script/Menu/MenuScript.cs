using UnityEngine;
using System.Collections;

public class MenuScript : MonoBehaviour {

    public static bool OptionsEnabled = false;

    public static bool ShowGUI2 = false;

    private bool MusicEnabled = true;
    public static bool StopMusic = false;
    public static bool SoundEnabled = true;

    private AudioSource audio;
    public AudioClip Menu;
    public AudioClip Story;

    public static bool PlayStoryMusic = false;
    public static bool PlayMenuMusic = false;

    public static bool OnlyOne = false;
    public static bool FirstLoad = true;

    private Rect windowRect = new Rect(Screen.width - 800,Screen.height - 600, 300, 200);

    public void ToggleGUI2() { ShowGUI2 = !ShowGUI2; }

    void OnGUI()
    {
        if (!ShowGUI2) return;
        windowRect = GUI.Window(0, windowRect, WindowFunction, "Credits");
    }

    void WindowFunction(int windowID)
    {
        if (GUI.Button(new Rect(265, 5, 25, 25), "X")) ShowGUI2 = !ShowGUI2;
        GUI.Label(new Rect(10, 20, 200, 20), "ERSS");
        GUI.Label(new Rect(10, 40, 200, 20), "Code: ");
        GUI.Label(new Rect(100, 60, 200, 20), "Sebastian & Even");
        GUI.Label(new Rect(10, 80, 100, 50), "Animation & Sprites: ");
        GUI.Label(new Rect(100, 100, 200, 20), "Kristoffer & Sigurd");
        GUI.Label(new Rect(10, 160, 200, 100), "Takk til tegneserie-elever for card-art");
    }

    public void StoryMode()
    {
        OptionsBox.PlayStoryMusic = true;
        Application.LoadLevel(1);
    }
    public void DeckBuilder()
    {
        Debug.LogError("DeckBuilder has not been created, yet");
    }
    public void Options()
    {
        OptionsBox.ShowGUI = true;
    }
    public void Exit()
    {
        Application.Quit();
    }

}
