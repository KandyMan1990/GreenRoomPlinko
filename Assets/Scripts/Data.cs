﻿using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class Data
{
    static string path = Application.persistentDataPath + "/playerData.dat";

    public static void Save(List<PlayerData> players)
    {
        FileStream fs = new FileStream(path, FileMode.Create);

        BinaryFormatter bf = new BinaryFormatter();

        try
        {
            bf.Serialize(fs, players);
        }
        catch(SerializationException e)
        {
            ErrorManager.Instance.ShowError("Failed to save, reason: " + e.Message);
            throw;
        }
        finally
        {
            fs.Close();
        }
    }

    public static List<PlayerData> Load()
    {
        List<PlayerData> data;

        if(!File.Exists(path))
        {
            ErrorManager.Instance.ShowError("Save file does not exist, file will be created on next save");
            return new List<PlayerData>();
        }

        FileStream fs = new FileStream(path, FileMode.Open);

        try
        {
            BinaryFormatter bf = new BinaryFormatter();

            data = (List<PlayerData>)bf.Deserialize(fs);
        }
        catch(SerializationException e)
        {
            ErrorManager.Instance.ShowError("Failed to load save data, reason: " + e.Message);
            data = new List<PlayerData>();
        }
        finally
        {
            fs.Close();
        }

        return data;
    }
}