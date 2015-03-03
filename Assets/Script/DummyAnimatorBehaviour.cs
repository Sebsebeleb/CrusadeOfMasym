using UnityEngine;
using System.Collections;

/// <summary>
/// Handles automatically destroying the gameobject after the animation is done playing
/// </summary>
public class DummyAnimatorBehaviour : MonoBehaviour
{
    private Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
    }
    void Update()
    {
        AnimatorStateInfo animationState = anim.GetCurrentAnimatorStateInfo(0);
        if (animationState.IsName("done")) {
            Destroy(gameObject);
        }
    }
}