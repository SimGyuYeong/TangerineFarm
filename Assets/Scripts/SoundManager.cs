using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private Slider backmusicSilderBar;

    public void Start()
    {
        musicSource.volume = GameManager.Instance.CurrentUser.backmusicVolume;
        backmusicSilderBar.value = GameManager.Instance.CurrentUser.backmusicVolume;
    }

    public void SetMusicVolume(float volume)
    {
        GameManager.Instance.CurrentUser.backmusicVolume = volume;
        musicSource.volume = volume;
    }
}
