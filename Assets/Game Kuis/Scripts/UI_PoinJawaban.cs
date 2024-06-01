using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UI_PoinJawaban : MonoBehaviour
{
    public static event System.Action<string, bool> EventJawabSoal;

    // [SerializeField]
    // private UI_PesanLevel _tempatPesan = null;

    [SerializeField]
    private TextMeshProUGUI _teksJawaban = null;
    [SerializeField]
    private bool _adalahBenar = false;

    // Console log message
    public void PilihJawabann()
    {
        // Debug.Log($"Jawaban anda adalah {_teksJawaban.text} ({_adalahBenar})");
        // _tempatPesan.Pesan = $"Jawaban anda adalah {_teksJawaban.text} ({_adalahBenar})";

        EventJawabSoal?.Invoke(_teksJawaban.text, _adalahBenar);
    }

    public void SetJawaban(string teksJawaban, bool adalahBenar)
    {
        _teksJawaban.text = teksJawaban;
        _adalahBenar = adalahBenar;
    }
}
