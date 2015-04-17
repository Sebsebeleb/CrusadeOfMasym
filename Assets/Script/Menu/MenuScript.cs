using UnityEngine;
using System.Collections;

public class MenuScript : MonoBehaviour {

    public static bool OptionsEnabled = false;

    /*
    public void OnButtonCallback(string id)
    {
        Debug.Log(id);

        if (id == "StoryMode")
        {
            //OptionsBox.StopMusic = true;
            OptionsBox.PlayStoryMusic = true;
            Application.LoadLevel(1);
        }
        else if (id == "DeckBuilder")
        {
            //#GoToDeckBuildingScreen
        }
        else if (id == "Exit") Application.Quit();
    }
     */

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
        OptionsBox.ShowGUI = !OptionsBox.ShowGUI;
    }
    public void Exit()
    {
        Application.Quit();
    }

}
