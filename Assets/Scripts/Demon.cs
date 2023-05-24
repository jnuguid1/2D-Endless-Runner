using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Demon : MonoBehaviour
{
    public float speed;
    public Animator anim;
    public Background bg1;
    public Background bg2;
    public Background bg3;
    public Background bg4;
    public Background bg5;
    public Background bg6;
    public Background bg7;
    public Background bg8;
    public Background bg9;
    public Background bg10;
    public Background bg11;
    public Background bg12;
    public Background bg13;
    public Background bg14;
    public Background bg15;
    public Background bg16;
    public Background bg17;
    public Background bg18;
    public Background bg19;
    public Background bg20;
    public Player player;
    public Animator playerAnim;
    private SpriteRenderer rend;

    public AudioSource cleaveSound;

    private void Update()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            anim.SetBool("Cleave", true);
            // Debug.Log("Demon collide");
            // Stop the background movement and demon.
            bg1.speed = 0;
            bg2.speed = 0;
            bg3.speed = 0;
            bg4.speed = 0;
            bg5.speed = 0;
            bg6.speed = 0;
            bg7.speed = 0;
            bg8.speed = 0;
            bg9.speed = 0;
            bg10.speed = 0;
            bg11.speed = 0;
            bg12.speed = 0;
            bg13.speed = 0;
            bg14.speed = 0;
            bg15.speed = 0;
            bg16.speed = 0;
            bg17.speed = 0;
            bg18.speed = 0;
            bg19.speed = 0;
            bg20.speed = 0;
            speed = 0;
            // Prevent Player from moving.
            player.isDead = true;
            // Flip Player horizontally to face the demon and initiate death animaiton.
            rend = player.GetComponent(typeof(SpriteRenderer)) as SpriteRenderer;
            rend.flipX = true;
            playerAnim.SetTrigger("Death");
            // Destroy all collectibles and the spawner.
            var Spawner = GameObject.FindGameObjectsWithTag("Spawner");
            var Treasures = GameObject.FindGameObjectsWithTag("Treasure");
            var Potions = GameObject.FindGameObjectsWithTag("Potion");
            for (var i = 0; i < Spawner.Length; i++)
            {
                Destroy(Spawner[i]);
            }
            for (var i = 0; i < Treasures.Length; i++)
            {
                Destroy(Treasures[i]);
            }
            for (var i = 0; i < Potions.Length; i++)
            {
                Destroy(Potions[i]);
            }
            CameraController.instance.StopMusic();
            StartCoroutine(EndSceneRoutine());
        }

        IEnumerator EndSceneRoutine()
        {
            yield return new WaitForSeconds(0.8f);
            cleaveSound.Play();
            ScreenShakeController.instance.StartShake(.9f, 0.7f);
            yield return new WaitForSeconds(1.7f);
            SceneManager.LoadScene("GameOver");
        }
    }
}
