using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

    static GameManager instance;

    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new GameManager();
            }

            return instance;
        }
    }

    SwarmController swarmController;

    void Start()
    {
        swarmController = GameObject.Find("SwarmController").GetComponent<SwarmController>();
    }

    public SwarmController GetSwarmController()
    {
        return swarmController;
    }
}
