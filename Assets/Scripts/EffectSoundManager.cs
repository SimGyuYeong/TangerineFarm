using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectSoundManager : MonoBehaviour
{
    public void SetMusicVolume(float volume)
    {
        GameManager.Instance.CurrentUser.effectSoundVolume = volume;
    }
}
