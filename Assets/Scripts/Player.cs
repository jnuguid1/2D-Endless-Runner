using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
	private Rigidbody2D rb;
	public float jumpAmount = 1f;
	private bool isGrounded;
	public bool isDead;
	public Transform groundCheck;
	public LayerMask groundLayer;
	public Animator anim;
	private int potionNum;
	private int treasureAmt;
	private int Skull;
	public TMP_Text scoreDisplay;
	public Image Bar;
	public int MaxPotion;
	public float fillAmount;
	public TMP_Text barLabel;

	public Animator demonAnim;
	public Demon demon;
	private SpriteRenderer rend;
	public SpriteRenderer slashRenderer;
	private bool isEndlessMode;
	public AudioSource jumpSound;
	public AudioSource demonHitSound;
	public AudioSource potionSound;
	public AudioSource treasureSound;
	public AudioSource demonDeathSound;

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
	public Background g12;
	public Background bg13;
	public Background bg14;
	public Background bg15;
	public Background bg16;
	public Background bg17;
	public Background bg18;
	public Background bg19;
	public Background bg20;

	private void Start()
	{
		rb = base.gameObject.GetComponent<Rigidbody2D>();
		Bar.fillAmount = 0f;
		rend = (GetComponent(typeof(SpriteRenderer)) as SpriteRenderer);
		int @int = PlayerPrefs.GetInt("isEndless");
		UnityEngine.Debug.Log(@int);
		if (@int == 1)
		{
			isEndlessMode = true;
		}
		else
		{
			isEndlessMode = false;
		}
	}

	private void Update()
	{
		if (UnityEngine.Input.GetKeyDown(KeyCode.Space) && !isDead && isGrounded)
		{
			jump();
		}
		if (isGrounded)
		{
			anim.SetBool("Jump", value: false);
		}
		else
		{
			anim.SetBool("Jump", value: true);
		}
		scoreDisplay.text = treasureAmt.ToString();
		Potionbar();
	}

	private void FixedUpdate()
	{
		isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
	}

	private void jump()
	{
		jumpSound.Play();
		ScreenShakeController.instance.StartShake(0.2f, 0.05f);
		rb.AddForce(Vector2.up * jumpAmount, ForceMode2D.Impulse);
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.CompareTag("Treasure"))
		{
			treasureAmt++;
			treasureSound.Play();
		}
		if (other.CompareTag("Potion"))
		{
			potionNum++;
			Bar.fillAmount += fillAmount;
			potionSound.Play();
		}
		if (treasureAmt >= 10 && !isEndlessMode)
		{
			bg1.speed = 0f;
			bg2.speed = 0f;
			bg3.speed = 0f;
			bg4.speed = 0f;
			bg5.speed = 0f;
			bg6.speed = 0f;
			bg7.speed = 0f;
			bg8.speed = 0f;
			bg9.speed = 0f;
			bg10.speed = 0f;
			bg11.speed = 0f;
			bg12.speed = 0f;
			bg13.speed = 0f;
			bg14.speed = 0f;
			bg15.speed = 0f;
			bg16.speed = 0f;
			bg17.speed = 0f;
			bg18.speed = 0f;
			bg19.speed = 0f;
			bg20.speed = 0f;
			demon.speed = 0f;
			demonAnim.SetTrigger("Death");
			rend.flipX = true;
			anim.SetTrigger("Idle");
			StartCoroutine(FinalAttackRoutine());
			GameObject[] array = GameObject.FindGameObjectsWithTag("Spawner");
			GameObject[] array2 = GameObject.FindGameObjectsWithTag("Treasure");
			GameObject[] array3 = GameObject.FindGameObjectsWithTag("Potion");
			for (int i = 0; i < array.Length; i++)
			{
				UnityEngine.Object.Destroy(array[i]);
			}
			for (int j = 0; j < array2.Length; j++)
			{
				UnityEngine.Object.Destroy(array2[j]);
			}
			for (int k = 0; k < array3.Length; k++)
			{
				UnityEngine.Object.Destroy(array3[k]);
			}
			CameraController.instance.StopMusic();
			StartCoroutine(EndSceneRoutine());
		}
	}

	private IEnumerator FinalAttackRoutine()
	{
		slashRenderer.enabled = true;
		demonHitSound.Play();
		yield return new WaitForSeconds(0.4f);
		slashRenderer.enabled = false;
		yield return new WaitForSeconds(0.05f);
		demonHitSound.Play();
		slashRenderer.enabled = true;
		yield return new WaitForSeconds(0.4f);
		slashRenderer.enabled = false;
		demonDeathSound.Play();
	}

	private IEnumerator EndSceneRoutine()
	{
		yield return new WaitForSeconds(3f);
		SceneManager.LoadScene("WinScene");
	}

	private void Potionbar()
	{
		if (potionNum >= MaxPotion)
		{
			barLabel.text = "BAR IS FULL";
		}
		if (potionNum >= MaxPotion && UnityEngine.Input.GetKeyDown(KeyCode.Q) && !isDead)
		{
			Bar.fillAmount = 0f;
			potionNum = 0;
			barLabel.text = "";
			rend.flipX = true;
			anim.SetTrigger("Attack");
			StartCoroutine(AttackCoroutine());
			demonAnim.SetTrigger("Hit");
			slashRenderer.enabled = true;
			(demon.GetComponent(typeof(Transform)) as Transform).position = new Vector2(-2.9f, -2.869f);
			ScreenShakeController.instance.StartShake(0.6f, 0.3f);
			demonHitSound.Play();
		}
	}

	private IEnumerator AttackCoroutine()
	{
		yield return new WaitForSeconds(0.2f);
		slashRenderer.enabled = false;
		yield return new WaitForSeconds(0.2f);
		rend.flipX = false;
		anim.SetTrigger("Run");
	}
}
