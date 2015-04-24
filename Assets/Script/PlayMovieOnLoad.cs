using UnityEngine;

public class PlayMovieOnLoad : MonoBehaviour
{
    public MovieTexture movie;

    private bool IsPlaying;

    void Start()
    {
        IsPlaying = true;
        movie.Play();

        // Disable movie after it is done playing
        LeanTween.delayedCall(movie.duration + 0.5f, o => { Destroy(gameObject); });
    }

    void Update()
    {
    }

    void OnGUI()
    {

        if (IsPlaying)
        {
            GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), movie, ScaleMode.StretchToFill);
        }
    }
}