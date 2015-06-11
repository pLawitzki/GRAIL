using UnityEngine;
using System.Collections;


public class GameJoltAPIManager : MonoBehaviour {

    public bool UserVerified = false;

    public int gameID;
    public string privateKey;
    public int TrophyID;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
        GJAPI.Init(gameID, privateKey);
        GJAPIHelper.Users.GetFromWeb(OnGetFromWeb);
    }

    void OnGetFromWeb (string name, string token)
    {
         Debug.Log (name + "@" + token);
         GJAPI.Users.Verify(name, token);
    }

    void OnEnable()
    {
        GJAPI.Users.VerifyCallback += OnVerifyUser;
        //GJAPI.Trophies.GetOneCallback += OnGetTrophy;
    }

    void OnDisable()
    {
        GJAPI.Users.VerifyCallback -= OnVerifyUser;
        //GJAPI.Trophies.GetOneCallback -= OnGetTrophy;
    }

    void OnVerifyUser(bool success)
    {
        if (success)
        {
            UserVerified = true;
            Debug.Log("GJAPI: Yepee!");
        }
        else
        {
            UserVerified = false;
            Debug.Log("GJAPI: user verification failed.");
        }
    }

    public void UnlockAchivement()
    {
        if(UserVerified)
        {
            //GJAPI.Trophies.Get((uint)TrophyID);
            GJAPIHelper.Trophies.ShowTrophyUnlockNotification((uint)TrophyID);
            GJAPI.Trophies.Add((uint)TrophyID);
        }
    }
}
