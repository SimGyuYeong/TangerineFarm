using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioSource backSoundSource;
    [SerializeField] private Slider backSoundSilderBar;
    [SerializeField] private Slider effectSoundSilderBar;
    [SerializeField] private AudioClip[] effectSoundList;
    private AudioSource audioCllip;

    public void Start()
    {
        audioCllip = GetComponent<AudioSource>();
        backSoundSource.volume = GameManager.Instance.CurrentUser.backSoundVolume;
        backSoundSilderBar.value = GameManager.Instance.CurrentUser.backSoundVolume;
        effectSoundSilderBar.value = GameManager.Instance.CurrentUser.effectSoundVolume;
    }

    public void SetMusicVolume(float volume)
    {
        GameManager.Instance.CurrentUser.backSoundVolume = volume;
        backSoundSource.volume = volume;
    }

    public void PlayEffectSound(int num)
    {
        audioCllip.volume = GameManager.Instance.CurrentUser.effectSoundVolume;
        audioCllip.clip = effectSoundList[num];
        audioCllip.Play();
    }
}
