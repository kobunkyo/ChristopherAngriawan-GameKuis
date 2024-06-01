using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AudioManager : MonoBehaviour
{
    public static AudioManager instance = null;

    [SerializeField]
    private AudioSource _bgmPrefab = null;

    [SerializeField]
    private AudioSource _sfxPrefab = null;

    [SerializeField]
    private AudioClip[] _bgmClips = new AudioClip[0];

    private AudioSource _bgm = null;
    private AudioSource _sfx = null;

    void Awake()
    {
        if (instance != null)
        {
            Debug.Log("Objek \"Audio Manager\" sudah ada.\n" + "Hapus Objek serupa.", instance);
            Destroy(this.gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(this.gameObject);
        // Buat objek BGM
        _bgm = Instantiate(_bgmPrefab);
        DontDestroyOnLoad(_bgm);

        // Buat objek SFX
        _sfx = Instantiate(_sfxPrefab);
        DontDestroyOnLoad(_sfx);
    }

    private void OnDestroy()
    {
        if(this == instance)
        {
            instance = null;

            if(_bgm != null) Destroy(_bgm.gameObject);
            if(_sfx != null) Destroy(_sfx.gameObject);
        }
        
    }
    
    public void PlayBGM(int index)
    {
        // Abaikan jika suara background yang sama digantikan
        if(_bgm.clip == _bgmClips[index]) return;

        _bgm.clip = _bgmClips[index];
        _bgm.Play();
    }

    public void PlaySFX(AudioClip clip)
    {
        _sfx.PlayOneShot(clip);
    }
}
