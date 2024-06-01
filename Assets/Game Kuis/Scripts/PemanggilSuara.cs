using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PemanggilSuara : MonoBehaviour
{
    public void PanggilSuara(AudioClip suara)
    {
        AudioManager.instance.PlaySFX(suara);
    }
}
