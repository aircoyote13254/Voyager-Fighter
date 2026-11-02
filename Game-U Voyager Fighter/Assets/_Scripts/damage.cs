// Copyright 2018 Game-U Enterprises, LLC
using UnityEngine;

public class damage : MonoBehaviour
{
    public int damagePoints = 50;
    public AudioClip hitSound;
    public AudioClip blockSound;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<player>())
        {
            if (collision.gameObject.GetComponent<player>().blocking == true)
            {
                collision.gameObject.GetComponent<player>().health -= damagePoints / 5;
                GetComponent<AudioSource>().PlayOneShot(blockSound);
            }
            else
            {
                collision.gameObject.GetComponent<player>().health -= damagePoints;
                collision.gameObject.GetComponent<Animator>().SetTrigger("hit");
                GetComponent<AudioSource>().PlayOneShot(hitSound);
            }         
        }
    }
}
