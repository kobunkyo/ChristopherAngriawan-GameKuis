using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

[CreateAssetMenu(
    fileName = "Player Progress",
    menuName = "Game Kuis/Player Progress"
)]

public class PlayerProgress : ScriptableObject
{
    [System.Serializable]
    public struct MainData
    {
        public int koin;
        public Dictionary<string, int> progresLevel;
    }

    [SerializeField]
    private string _filename = "contoh.txt";

    [SerializeField]
    private string _startingLevelPackName = string.Empty;

    public MainData progresData = new MainData();

    public void SimpanProgress()
    {
        // Contoh Data
        // progresData.koin = 200;
        // if(progresData.progresLevel == null) progresData.progresLevel = new();
        // progresData.progresLevel.Add("Level Pack 1", 3);
        // progresData.progresLevel.Add("Level Pack 3", 5);

        // Simpan Starting Data saat objek DIctionary tidak ada saat dimuat
        if (progresData.progresLevel == null)
        {
            progresData.progresLevel = new();
            progresData.koin = 0;
            progresData.progresLevel.Add(_startingLevelPackName, 1);
        }

        // Informasi penyimpanan data
#if UNITY_EDITOR
        string directory = Application.dataPath + "/Temporary/";
#elif (UNITY_ANDROID || UNITY_IOS) && !UNITY_EDITOR
        string directory = Application.persitentDataPath + "/ProgreLokal/";
#endif 
        var path = directory + "/" + _filename;
        

        // Membuat Direcotry temp
        if(!Directory.Exists(directory))
        {
            Directory.CreateDirectory(directory);
            Debug.Log("Directory has been Created: " + directory);
        }

        // Create File
        if(File.Exists(path))
        {
            File.Create(path).Dispose();
            Debug.Log("File created: " + path);
        }

        var fileStream = File.Open(path, FileMode.OpenOrCreate);
        
        // // Simpan data dengan binary formater
        // var formatter = new BinaryFormatter();
        
        fileStream.Flush();
        // formatter.Serialize(fileStream, progresData);

        // Binary Writer
        var writer = new BinaryWriter(fileStream);

        writer.Write(progresData.koin);
        foreach(var i in progresData.progresLevel)
        {
            writer.Write(i.Key);
            writer.Write(i.Value);
        }

        writer.Dispose();// Mirip free() di C

        fileStream.Dispose();

        // var konten = $"{progresData.koin}\n";
        // foreach(var i in progresData.progresLevel)
        // {
        //     konten += $"{i.Key} {i.Value}\n";
        // }

        // File.WriteAllText(path, konten);

        Debug.Log($"{_filename} Berhasil Disimpan");
    }

    public bool MuatProgress()
    {
        string directory = Application.dataPath + "/Temporary";
        string path = directory + "/" + _filename;

        // Membuat Direcotry temp
        if(!Directory.Exists(directory))
        {
            Directory.CreateDirectory(directory);
            Debug.Log("Directory has been Created: " + directory);
        }

        // Buka file
        var fileStream = File.Open(path, FileMode.OpenOrCreate);        
        try
        {
            var reader = new BinaryReader(fileStream);
            try
            {
                progresData.koin = reader.ReadInt32();
                if(progresData.progresLevel == null) progresData.progresLevel = new();
                while (reader.PeekChar() != -1)
                {
                    var namaLevelPack = reader.ReadString();
                    var levelKe = reader.ReadInt32();
                    progresData.progresLevel.Add(namaLevelPack, levelKe);
                    Debug.Log($"{namaLevelPack}:{levelKe}");
                }
            }
            catch(System.Exception e)
            {
                Debug.Log($"ERROR: Terjadi kesalahan saat memuat progres binari.\n{e.Message}");
                reader.Dispose();
                fileStream.Dispose();
                return false;
            }
            // // Load data
            // var formatter = new BinaryFormatter();

            // progresData = (MainData)formatter.Deserialize(fileStream);

            fileStream.Dispose();

            Debug.Log($"{progresData.koin}; {progresData.progresLevel.Count}");

            return true;
        }
        catch(System.Exception e)
        {
            fileStream.Dispose();
            Debug.Log($"ERROR: Terjadi kesalahan saat memuat progres\n{e.Message}");
            return false;
        }
    }
}
