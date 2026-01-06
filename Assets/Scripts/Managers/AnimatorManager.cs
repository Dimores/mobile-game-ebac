using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorManager : MonoBehaviour
{
    public Animator animator;
    public List<AnimationSetup> animatorSetups;

    public enum AnimatonType
    {
        IDLE,
        RUN,
        DEAD,
        FLY
    }

    public void Play(AnimatonType type, float currentSpeedFactor = 1f)
    {
        foreach (var animation in animatorSetups)
        {
            if (animation.type == type)
            {
                animator.SetTrigger(animation.trigger);
                animator.speed = animation.speed * currentSpeedFactor;
                break;
            }
        }
    }
    
}

[System.Serializable]
public class AnimationSetup
{
    public AnimatorManager.AnimatonType type;
    public string trigger;
    public float speed = 1f;
}
