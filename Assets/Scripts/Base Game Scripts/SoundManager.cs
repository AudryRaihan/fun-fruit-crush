using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SoundManager : MonoBehaviour
{
    public AudioSource SfxMusic;
    public AudioSource soundMusic;

    [Header("UI Buttons")]
    public string musicButtonOnName = "MusicButtonOn"; // Nama GameObject tombol On
    public string musicButtonOffName = "MusicButtonOff"; // Nama GameObject tombol Off
    public string sfxButtonOnName = "SfxButtonOn";
    public string sfxButtonOffName = "SfxButtonOff";

    public static SoundManager instance { get; private set; }
    private bool musicEnabled = true;
    private Button musicButtonOn;
    private Button musicButtonOff;
    private Button sfxButtonOn;
    private Button sfxButtonOff;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            SceneManager.sceneLoaded += OnSceneLoaded; // Subscribe ke event scene loaded
        }
        else if (instance != this)
        {
            Destroy(gameObject);
            return;
        }
    }


    void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded; // Unsubscribe
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        FindAndSetupButtons();
    }

    void Start()
    {
        LoadSoundSettings();
        UpdateAudioSources();
        FindAndSetupButtons(); // Cari tombol di scene awal
        UpdateMusicPlayback();
    }

    public void FindAndSetupButtons()
    {
        Button[] allButtons = Resources.FindObjectsOfTypeAll<Button>();
        foreach (Button btn in allButtons)
        { 
            if (btn.name == musicButtonOnName)
            { 
                musicButtonOn = btn.GetComponent<Button>();
            }
            else if (btn.name == musicButtonOffName)
            {
                musicButtonOff = btn.GetComponent<Button>();
            }
            else if (btn.name == sfxButtonOnName)
            {
                sfxButtonOn = btn.GetComponent<Button>();
            }
            else if (btn.name == sfxButtonOffName)
            {
                sfxButtonOff = btn.GetComponent<Button>();
            }
        }

        SetupButtonListeners();
    }

    void SetupButtonListeners()
    {
        if (musicButtonOn != null)
        {
            Debug.Log(musicButtonOn);
            musicButtonOn.onClick.RemoveAllListeners();
            musicButtonOn.onClick.AddListener(() => EnableMusicForced());
        }

        if (musicButtonOff != null)
        {
            Debug.Log("Activateafjawoef;awef");
            musicButtonOff.onClick.RemoveAllListeners();
            musicButtonOff.onClick.AddListener(() => DisableMusicForced());
        }

        if (sfxButtonOn != null)
        {
            sfxButtonOn.onClick.RemoveAllListeners();
            sfxButtonOn.onClick.AddListener(() => SfxMusicOn());
        }

        if (sfxButtonOff != null)
        {
            sfxButtonOff.onClick.RemoveAllListeners();
            sfxButtonOff.onClick.AddListener(() => SfxMusicOff());
        }
    }

    public void EnableMusicForced()
    {
        musicEnabled = true;
        PlayerPrefs.SetInt("SoundMusic", 1);
        PlayerPrefs.Save();
        UpdateMusicPlayback();
    }

    public void DisableMusicForced()
    {
        musicEnabled = false;
        PlayerPrefs.SetInt("SoundMusic", 0);
        PlayerPrefs.Save();
        UpdateMusicPlayback();
    }

    void UpdateMusicPlayback()
    {
        if (soundMusic != null)
        {
            if (musicEnabled)
            {
                if (!soundMusic.isPlaying)
                {
                    soundMusic.Play();
                }
                soundMusic.mute = false;
            }
            else
            {
                soundMusic.Stop();
            }
        }
    }

    public void SfxMusicOn()
    {
        PlayerPrefs.SetInt("SfxMusic", 1);
        Debug.Log("SFX On: " + PlayerPrefs.GetInt("SfxMusic"));
        PlaySfxMusic();
    }

    public void SfxMusicOff()
    {
        PlayerPrefs.SetInt("SfxMusic", 0);
        Debug.Log("SFX Off: " + PlayerPrefs.GetInt("SfxMusic"));
        if (SfxMusic != null && SfxMusic.isPlaying)
        {
            SfxMusic.Stop();
        }
    }

    public void MuteSfxMusic()
    {
        if (SfxMusic != null)
        {
            SfxMusic.Pause();
        }
    }

    public void PlaySfxMusic()
    {
        if (PlayerPrefs.HasKey("SfxMusic"))
        {
            if (PlayerPrefs.GetInt("SfxMusic") == 1)
            {
                if (SfxMusic != null && !SfxMusic.isPlaying)
                {
                    SfxMusic.Play();
                }
            }
            else
            {
                if (SfxMusic != null && SfxMusic.isPlaying)
                {
                    SfxMusic.Stop();
                }
            }
        }
        else
        {
            if (SfxMusic != null && !SfxMusic.isPlaying)
            {
                SfxMusic.Play();
            }
        }
    }

    void LoadSoundSettings()
    {
        musicEnabled = PlayerPrefs.GetInt("SoundMusic", 1) == 1;
    }

    void UpdateAudioSources()
    {
        if (soundMusic != null)
        {
            soundMusic.mute = !musicEnabled;
        }
        if (SfxMusic != null)
        {
            SfxMusic.mute = PlayerPrefs.GetInt("SfxMusic", 1) == 0;
        }
    }
}