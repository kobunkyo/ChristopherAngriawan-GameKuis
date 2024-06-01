using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class UI_Pertanyaan : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _namaLevel = null;

    [SerializeField]
    private TextMeshProUGUI _tempatTeks = null;
    [SerializeField]
    private Image _tempatGambar = null;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Isi tempat teks yaitu:");
        Debug.Log(_tempatTeks.text);
    }

    public void SetPertanyaan(string namaJudul, string teksPertanyaan, Sprite gambarHint)
    {
        _namaLevel.text = namaJudul;
        _tempatGambar.sprite = gambarHint;
        _tempatTeks.text = teksPertanyaan;
    }
}
