using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;


[System.Serializable]
public class SoundEntry
{
    public string name;
    public AudioClip clip;
}

public class SoundManager : MonoBehaviour
{
    
    public static SoundManager instance;

    [SerializeField] List<SoundEntry> sfxList = new List<SoundEntry>();

    [SerializeField] AudioClip lobbyBGM;
    [SerializeField] AudioClip gameBGM;

    [SerializeField] float sfxVolume = 1f;
    [SerializeField] float bgmVolume = 0.5f;

    AudioSource sfxSource;
    AudioSource bgmSource;
    Dictionary<string, AudioClip> sfxDic = new Dictionary<string, AudioClip>();

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        sfxSource = gameObject.AddComponent<AudioSource>();
        bgmSource = gameObject.AddComponent<AudioSource>();
        bgmSource.loop = true;

        sfxSource.spatialBlend = 0f;
        bgmSource.spatialBlend = 0f;

        foreach (SoundEntry entry in sfxList)
        {
            if (!sfxDic.ContainsKey(entry.name))
                sfxDic.Add(entry.name, entry.clip);
        }

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void Start()
    {
        PlayBgmForScene(SceneManager.GetActiveScene().name);
    }


    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        PlayBgmForScene(scene.name);
    }


    void PlayBgmForScene(string sceneName)
    {
        AudioClip targetClip = null;

        if (sceneName == "LobbyScene")
            targetClip = lobbyBGM;
        else if (sceneName == "GameScene")
            targetClip = gameBGM;

        if (targetClip == null) return;
        if (bgmSource.clip == targetClip && bgmSource.isPlaying) return;

        bgmSource.clip = targetClip;
        bgmSource.volume = bgmVolume;
        bgmSource.Play();
    }

    public void PlaySFX(string name)
    {
        if (sfxDic.ContainsKey(name) && sfxDic[name] != null)
        {
            sfxSource.PlayOneShot(sfxDic[name], sfxVolume);
        }
    }
        
    public void StopBGM()
    {
        bgmSource.Stop();
    }

    void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

}
