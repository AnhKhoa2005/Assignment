using UnityEngine;
using UnityEngine.UI;
public class SoundManager : MonoBehaviour
{

    [SerializeField] AudioSource music;
    [SerializeField] AudioSource Button;
    [SerializeField] AudioClip button;
    [SerializeField] AudioClip musicClip;
    [SerializeField] Slider volumeSlider;
    [SerializeField] Toggle mute;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Load();
        music.clip = musicClip;
        music.Play();

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ChangeSound()
    {
        music.volume = volumeSlider.value;
        Save();
    }

    void Load()
    {
        volumeSlider.value = PlayerPrefs.GetFloat("volume");
        if (PlayerPrefs.GetInt("mute") == 1)
        {
            mute.isOn = true;
        }
        else mute.isOn = false;
    }
    void Save()
    {
        PlayerPrefs.SetFloat("volume", music.volume);
        if (music.mute == true)
        {
            PlayerPrefs.SetInt("mute", 1);
        }
        else
        {
            PlayerPrefs.SetInt("mute", 0);
        }
    }

    public void Mute()
    {
        music.mute = mute.isOn;
        Save();
    }

    public void SoundButton()
    {
        Button.clip = button;
        Button.PlayOneShot(button);
    }
}
