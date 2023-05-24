using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Treasure : MonoBehaviour
{
    public float speed;
    public GameObject treasureParticle;

    private void Update()
    {
        transform.Translate(Vector2.left * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Destroy(gameObject);
            ScreenShakeController.instance.StartShake(.2f, 0.1f);
            Instantiate(treasureParticle, transform.position, transform.rotation);
        }

        if (other.CompareTag("Demon"))
        {
            Destroy(gameObject);
        }
    }
}
