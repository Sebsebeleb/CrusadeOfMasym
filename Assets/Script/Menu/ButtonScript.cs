using UnityEngine;
using System.Collections;

public class ButtonScript : MonoBehaviour
{
    public string ButtonID;
    public MenuScript menu;
    public OptionsBox options;

    void OnMouseUpAsButton()
    {
        menu.OnButtonCallback(ButtonID);
        options.OnButtonCallback(ButtonID);
    }
}
