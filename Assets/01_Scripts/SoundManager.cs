using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class SoundManager : MonoBehaviour
{
    [Header("---------Audio Source---------")]
    public AudioSource MusicBG, SFX;
    [Header("---------Audio Clip Music_BG---------")]
    [SerializeField] AudioClip audioClipBG_MainMenu;
    [SerializeField] AudioClip audioClipBG_Map1;
    [SerializeField] AudioClip audioClipBG_Map2;
    [SerializeField] AudioClip audioClipBG_Map3;
    [Header("---------Audio Clip SFX---------")]
    public AudioClip audioClipSFX_Run;
    public AudioClip audioClipSFX_Jump;
    public AudioClip audioClipSFX_Attack;
    public AudioClip audioClipSFX_Dash;
    public AudioClip audioClipSFX_Gethit;
    public AudioClip audioClipSFX_Die;
    public AudioClip audioClipSFX_HP;
    public AudioClip ButtonClick;
    public AudioClip transition;
    public AudioClip YouWin;
    [Header("----------------Slider--------------")]
    [SerializeField] Slider MusicBG_Bar;
    [SerializeField] Slider SFX_Bar;
    string SceneName;
    public static SoundManager instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Load();
        SceneName = SceneManager.GetActiveScene().name;
        if (SceneName == "MainMenu") MusicBG.clip = audioClipBG_MainMenu;
        else if (SceneName == "Level_1") MusicBG.clip = audioClipBG_Map1;
        else if (SceneName == "Level_2") MusicBG.clip = audioClipBG_Map2;
        else if (SceneName == "Level_3") MusicBG.clip = audioClipBG_Map3;
        MusicBG.Play();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void MusicBG_volume()
    {
        //Điều chỉnh slider nhạc nền
        MusicBG.volume = MusicBG_Bar.value;
        //Lưu nhạc nền
        PlayerPrefs.SetFloat("BG_volume", MusicBG.volume);

    }

    public void SFX_volume()
    {
        //Điều chỉnh slider SFX
        SFX.volume = SFX_Bar.value;
        //Lưu SFX
        PlayerPrefs.SetFloat("SFX", SFX.volume);
    }

    void Load()
    {
        //Load nhạc nền

        MusicBG_Bar.value = PlayerPrefs.GetFloat("BG_volume");
        MusicBG.volume = MusicBG_Bar.value;

        //Load SFX
        SFX_Bar.value = PlayerPrefs.GetFloat("SFX");
        SFX.volume = SFX_Bar.value;
    }

    public void PlaySFX(AudioClip audioClip)
    {
        SFX.PlayOneShot(audioClip);
    }


    public void Button_PlaySFX()
    {
        SFX.clip = ButtonClick;
        SFX.Play();
    }

}
