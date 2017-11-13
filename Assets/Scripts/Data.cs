using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class Data
{
    static string playerDataPath = Application.persistentDataPath + "/playerData.dat";
    static string lastGamePlayersPath = Application.persistentDataPath + "/lastGamePlayers.dat";

    public static void Save(List<PlayerData> players)
    {
        FileStream fs = new FileStream(playerDataPath, FileMode.Create);

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

    public static void Save(string winner, string[] losers)
    {
        string[] players = new string[losers.Length + 1];

        players[0] = winner;
        for (int i = 0; i < losers.Length; i++)
        {
            players[i + 1] = losers[i];
        }

        FileStream fs = new FileStream(lastGamePlayersPath, FileMode.Create);

        BinaryFormatter bf = new BinaryFormatter();

        try
        {
            bf.Serialize(fs, players);
        }
        catch (SerializationException e)
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

        if(!File.Exists(playerDataPath))
        {
            ErrorManager.Instance.ShowError("Save file does not exist, file will be created on next save");
            return new List<PlayerData>();
        }

        FileStream fs = new FileStream(playerDataPath, FileMode.Open);

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

    public static string[] LoadLastGame()
    {
        string[] data;

        if (!File.Exists(lastGamePlayersPath))
        {
            return new string[0];
        }

        FileStream fs = new FileStream(lastGamePlayersPath, FileMode.Open);

        try
        {
            BinaryFormatter bf = new BinaryFormatter();

            data = (string[])bf.Deserialize(fs);
        }
        catch
        {
            data = new string[0];
        }
        finally
        {
            fs.Close();
        }

        return data;
    }
}