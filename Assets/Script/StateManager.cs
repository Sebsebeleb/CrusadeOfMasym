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

    public static void RegisterAnimation(float time)
    {
        // Add a short delay to slow down playback time
        float finishTime = Time.time + time + 0.5f;
        
        // If the new animation is longer than the old one, update the finish time
        animationTime = Math.Min(finishTime, animationTime);
    }

    public static AnimState GetAnimationState()
    {
        if (Time.time >= animationTime) {
            return AnimState.Idle;
        }

        return AnimState.Playing;
    }

}
