// Copyright 2018 Game-U Enterprises, LLC
using UnityEngine;

public class projectile : MonoBehaviour
{
    void Start()
    {
        GetComponent<Rigidbody2D>().linearVelocity = transform.right * 20f;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        transform.position = new Vector3(0f, -100f, 0f); // move offscreen
        Destroy(gameObject, 2); // cleanup 2 seconds later to allow audio to play
    }
}
