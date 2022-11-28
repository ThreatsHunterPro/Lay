using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class AnimParam 
{
    public const string PLAYER_SPEED_PARAM = "playerSpeed";
    public static readonly int playerSpeedParam = Animator.StringToHash(PLAYER_SPEED_PARAM);
    public const string PLAYER_ISFALLING_PARAM = "isFalling";
    public static readonly int isFalling = Animator.StringToHash(PLAYER_ISFALLING_PARAM);
}
