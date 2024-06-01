using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelMenuDataManager : MonoBehaviour
{
    [SerializeField]
    private UI_LevelPackList _levelPackList = null;

    [SerializeField]
    private PlayerProgress _playerProgress = null;

    [SerializeField]
    private TextMeshProUGUI _tempatKoin = null;

    [SerializeField]
    private LevelPackKuis[] _levelPacks = new LevelPackKuis[0];

    private void Start() 
    {
        if(!_playerProgress.MuatProgress())
        {
            _playerProgress.SimpanProgress();
        }
        _levelPackList.LoadLevelPack(_levelPacks, _playerProgress.progresData);
        _tempatKoin.text = $"{_playerProgress.progresData.koin}";
        AudioManager.instance.PlayBGM(0);
    }
}
