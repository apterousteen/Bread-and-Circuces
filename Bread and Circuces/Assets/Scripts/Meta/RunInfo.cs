using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunInfo : MonoBehaviour
{
    public static RunInfo Instance;
    public Player Player;

    private void Awake()
    {

        if (!Instance)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        Player = new Player();
        Player.team = Team.Player;

        DontDestroyOnLoad(this);

    }
}
