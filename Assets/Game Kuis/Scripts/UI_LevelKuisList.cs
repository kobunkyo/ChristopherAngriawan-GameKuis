using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class UI_LevelKuisList : MonoBehaviour
{
    [SerializeField]
    private InisialDataGameplay _inisialData = null;
    [SerializeField]
    private PlayerProgress _playerProgress = null;
    [SerializeField]
    private UI_OpsiLevelKuis _tombolLevel = null;
    [SerializeField]
    private RectTransform _content = null;
    [SerializeField]
    private LevelPackKuis _levelPack = null;
    [SerializeField]
    private GameSceneManager _gameSceneManager = null;
    [SerializeField]
    private string _gameplayScene = null;
    private void Start() 
    {
        // if (_levelPack != null)
        // {
        //     UnloadLevelPack(_levelPack);
        // }

        // Subscribe Events
        UI_OpsiLevelKuis.EventSaatKlik += UI_OpsiLevelKuis_EventSaatKlick;
    }

    private void OnDestroy()
    {
        // Unsubscribe Events
        UI_OpsiLevelKuis.EventSaatKlik -= UI_OpsiLevelKuis_EventSaatKlick;
    }

    private void UI_OpsiLevelKuis_EventSaatKlick(int index)
    {
        _inisialData.levelIndex = index;
        _gameSceneManager.BukaScene(_gameplayScene);
    }
    public void UnloadLevelPack(LevelPackKuis levelPack)
    {
        HapusIsiKonten();
        
        var levelTerbukaTerakhir = _playerProgress.progresData.progresLevel[levelPack.name] - 1;

        _levelPack = levelPack;
        for (int i=0; i < levelPack.BanyakLevel; i++)
        {
            // Membuat salinan objek dari prefab tombol level pack
            var t = Instantiate(_tombolLevel);

            t.SetLevelKuis(levelPack.AmbilLevelKe(i), i);

            // Masukkan objek tombol sebagai anak dari objek "content"
            t.transform.SetParent(_content);
            t.transform.localScale = Vector3.one;
            
            if(i > levelTerbukaTerakhir)
            {
                t.InteraksiTombol = false;
            }
        }
        
    }

    private void HapusIsiKonten()
    {
        var cc = _content.childCount;
        for(int i=0; i < cc; i++)
        {
            Destroy(_content.GetChild(i).gameObject);
        }
    }
}
