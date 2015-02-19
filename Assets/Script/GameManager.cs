using UnityEngine;
using System.Collections;

/// <summary>
/// Manages initalization of stuff
/// </summary>
public class GameManager : MonoBehaviour
{

    void Start()
    {
        DataLibrary.LoadEffects();
    }

    void Update()
    {

    }
}