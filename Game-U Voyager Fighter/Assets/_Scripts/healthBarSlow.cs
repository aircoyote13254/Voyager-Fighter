// Copyright 2018 Game-U Enterprises, LLC
using UnityEngine;

public class healthBarSlow : MonoBehaviour
{
    public float xOffset = 0.05f;
    public float yOffset = 0.5f;
    public myplayer myPlayer;
   
    // Update is called once per frame
    void Update()
    {
        transform.localScale = Vector3.Lerp(transform.localScale, new Vector3(myPlayer.health * xOffset, yOffset, 1f), Time.deltaTime * 5f);
    }
}
