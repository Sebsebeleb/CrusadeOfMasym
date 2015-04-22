using UnityEngine;
using System.Collections;

public class MenuScript : MonoBehaviour {

    public static bool OptionsEnabled = false;

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
