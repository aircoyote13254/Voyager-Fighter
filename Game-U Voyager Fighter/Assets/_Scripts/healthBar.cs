// Copyright 2018 Game-U Enterprises, LLC
using UnityEngine;

public class healthBar : MonoBehaviour
{
    public player myPlayer;

    // Update is called once per frame
    void Update()
    {
        transform.localScale = new Vector3(myPlayer.health * 0.05f, 0.5f, 1f);
    }
}
