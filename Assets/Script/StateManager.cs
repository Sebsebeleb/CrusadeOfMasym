using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public enum AnimState
{
    Playing,
    Idle,
}

public static class StateManager
{
    // The Time.time number when all animations are done
    private static float animationTime = 0f;
    private const float animationDelay = 0.05f; // The delay added to all registered animations

    public static float AnimationTime
    {
        get { return animationTime; }
    }

    public static void RegisterAnimation(float time)
    {
        // Add a short delay to slow down playback time
        float finishTime = Time.time + time + animationDelay;

        // If the new animation is longer than the old one, update the finish time
        animationTime = Math.Max(finishTime, animationTime);
    }

    public static AnimState GetAnimationState()
    {
        if (Time.time >= animationTime) {
            return AnimState.Idle;
        }

        return AnimState.Playing;
    }
}