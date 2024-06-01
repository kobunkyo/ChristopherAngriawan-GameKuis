using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField]
    private InisialDataGameplay _inisialData = null;

    [SerializeField]
    private PlayerProgress _playerProgress = null;

    [SerializeField]
    private LevelPackKuis _soalSoal = null;

    [SerializeField]
    private UI_Pertanyaan _pertanyaan = null; // Penghubung script ke UI_Pertanyaan

    [SerializeField]
    private UI_PoinJawaban[] _pilihanJawaban = new UI_PoinJawaban[0]; // Penghubung script ke UI_PoinJawaban

    [SerializeField]
    private GameSceneManager _gameSceneManager = null;

    [SerializeField]
    private string _namaScenePilihMenu = string.Empty;

    [SerializeField]
    private PemanggilSuara _pemanggiSuara = null;

    [SerializeField]
    private AudioClip _suaraMenang = null;

    [SerializeField]
    private AudioClip _suaraKalah = null;

    private int _indexSoal = -1;

    public void Start()
    {
        
        
        _soalSoal = _inisialData.levelPack;
        _indexSoal = _inisialData.levelIndex - 1;

        NextLevel();
        AudioManager.instance.PlayBGM(1);
        // Subscribe events
        UI_PoinJawaban.EventJawabSoal += UI_PoinJawaban_EventJawabSoal;
    }

    private void OnDestroy() 
    {
        // Unsubscribe events
        UI_PoinJawaban.EventJawabSoal -= UI_PoinJawaban_EventJawabSoal;
    }

    private void UI_PoinJawaban_EventJawabSoal(string jawaban, bool adalahBenar)
    {
        _pemanggiSuara.PanggilSuara(adalahBenar ? _suaraMenang : _suaraKalah);
        // Cek jika tidak benar, abaikan prosedur
        if (!adalahBenar) return;

        string namaLevelPack = _inisialData.levelPack.name;
        int levelTerakhir = _playerProgress.progresData.progresLevel[namaLevelPack];

        // Cek apabila level terakhir kali main terlah diselesaikan
        if(_indexSoal + 2 > levelTerakhir)
        {
            _playerProgress.progresData.koin += 20;

            _playerProgress.progresData.progresLevel[namaLevelPack] = _indexSoal + 2;
            _playerProgress.SimpanProgress();
        }
    }

    private void OnApplicationQuit() 
    {
        _inisialData.SaatKalah = false;
    }

    public void NextLevel()
    {
        _indexSoal++;

        if(_indexSoal >= _soalSoal.BanyakLevel)
        {   
            // _indexSoal = 0;
            _gameSceneManager.BukaScene(_namaScenePilihMenu);
            return;
        }

        LevelSoalKuis soal = _soalSoal.AmbilLevelKe(_indexSoal);

        _pertanyaan.SetPertanyaan($"Soal {_indexSoal + 1}", soal.pertanyaan, soal.petunjukJawaban); // Masukin value dari Level Manager ke script UI_Pertanyaan

        for(int i = 0; i<_pilihanJawaban.Length; i++)
        {
            UI_PoinJawaban poin = _pilihanJawaban[i]; // Masukin ke jawaban A - D lewat for loop
            LevelSoalKuis.OpsiJawaban opsi = soal.opsiJawaban[i];
            poin.SetJawaban(opsi.jawabanTeks, opsi.adalahBenar); // Masukin value dari Level Manager ke script UI_PoinJawaban
        }
    }
}
