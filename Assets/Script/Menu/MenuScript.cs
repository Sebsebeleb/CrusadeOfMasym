using UnityEngine;
using System.Collections;

public class MenuScript : MonoBehaviour {

    public static bool OptionsEnabled = false;

    public void OnButtonCallback(string id)
    {
        Debug.Log(id);

        if (id == "StoryMode")
        {
            OptionsBox.StopMusic = true;
            Application.LoadLevel(1);
        }
        else if (id == "DeckBuilder")
        {
            //#GoToDeckBuildingScreen
        }
        else if (id == "Exit") Application.Quit();
    }
}
