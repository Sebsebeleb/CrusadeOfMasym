using UnityEngine;

public class SpeechBubble : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }


    public void OnSignal(bool b)
    {
        transform.GetChild(0).gameObject.SetActive(b);

    }
}
