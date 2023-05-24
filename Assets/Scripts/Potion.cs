using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potion : MonoBehaviour
{
    public float speed;
    public GameObject potionParticle;

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
            Instantiate(potionParticle, transform.position, transform.rotation);
        }

        if (other.CompareTag("Demon"))
        {
            Destroy(gameObject);
        }
    }
}
