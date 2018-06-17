using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class UserData
{
    static string userDataPath = Application.persistentDataPath + "/userData.dat";

    public static void Save(User user)
    {
        FileStream fs = new FileStream(userDataPath, FileMode.Create);
        BinaryFormatter bf = new BinaryFormatter();

        try
        {
            bf.Serialize(fs, user);
        }
        catch (SerializationException e)
        {
            Debug.Log("Failed to save, reason: " + e.Message);
            throw;
        }
        finally
        {
            fs.Close();
        }
    }

    public static User Load()
    {
        User data;
        FileStream fs = new FileStream(userDataPath, FileMode.Open);
        BinaryFormatter bf = new BinaryFormatter();

        try
        {
            data = (User)bf.Deserialize(fs);
        }
        catch (SerializationException e)
        {
            ErrorManager.Instance.ShowError("Failed to load save data, reason: " + e.Message);
            data = new User();
        }
        finally
        {
            fs.Close();
        }

        return data;
    }
}

[System.Serializable]
public class User
{
    public string Name { get; set; }
    public int Played { get; set; }
    public int Won { get; set; }
    public int Lost { get; set; }
    public int Made { get; set; }
    public int Received { get; set; }
    public int Average { get; set; }
    public string Vertical { get; set; }
    public string Room { get; set; }
    public string ChallengeId { get; set; }
    public bool isChallenger { get; set; }

    public User()
    {
        Name = string.Empty;
        Vertical = string.Empty;
        Room = string.Empty;
        Played = 0;
        Won = 0;
        Lost = 0;
        Made = 0;
        Received = 0;
        Average = 0;
        ChallengeId = string.Empty;
        isChallenger = false;
    }
}