// Copyright 2018 Game-U Enterprises, LLC
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private const float SPEED = 10f;

    public Vector2 Dir;

    void Start()
    {
    }

    void Update()
    {
        Vector2 pos = transform.position;
        pos.x += Dir.x * Time.deltaTime * SPEED;
        transform.position = pos;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        transform.position = new Vector3(0f, -100f, 0f); // move offscreen
        Destroy(gameObject, 2); // cleanup 2 seconds later to allow audio to play
    }
}
