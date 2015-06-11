using UnityEngine;
using System.Collections;

public class Persistency : MonoBehaviour {

    private static int checkpointLevel = 0;
    private static GameObject checkpointStatue;
    
    public static Persistency instance;
    public static GameObject particles;
    public static bool spawnPlayer = false;

    public static int deaths = 0;

	void Awake ()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
        instance = this;
        checkpointLevel = Application.loadedLevel;
        checkpointStatue = GameObject.Find("Checkpoint");
	}

    void OnLevelWasLoaded(int level)
    {
        if (spawnPlayer)
        {
            GameObject player = GameObject.Find("Knight");
            DestroyImmediate(player);

            checkpointStatue = GameObject.Find("Checkpoint");
            checkpointStatue.SendMessage("SetChecked");

            spawnPlayer = false;
            OnDeath();
        }
    }

    public static void SetCheckpoint(GameObject Checkpoint)
    {
        checkpointStatue = Checkpoint;
        checkpointLevel = Application.loadedLevel;
    }

    public static void OnDeath()
    {
        deaths++;
        if (checkpointLevel != Application.loadedLevel)
        {
            spawnPlayer = true;
            Application.LoadLevel(checkpointLevel);
        }
        else
        {
            particles = Instantiate(Resources.Load("Particles_implode"), checkpointStatue.transform.position, Quaternion.identity) as GameObject;
            particles.transform.position += new Vector3(0f, 1f, -particles.transform.position.z);
            instance.Invoke("SpawnPlayer", 0.8f);
        }
    }

    public void SpawnPlayer()
    {
        GameObject player = Instantiate(Resources.Load("Knight"), checkpointStatue.transform.position, Quaternion.identity) as GameObject;
        player.transform.position += new Vector3(0f, 0f, -player.transform.position.z);
        player.transform.Rotate(Vector3.up, 180f);
        Destroy(particles);
    }
}
