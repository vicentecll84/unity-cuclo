using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class BossController : MonoBehaviour
{
    [SerializeField] private GameObject[] projectileprefabs; // Drag the Projectile Prefabs here.

    [SerializeField] private GameObject[] AIPoints; // You are supposed to drag the AI Points of the hallways in here, not the rooms one but if you want you can also do that so go ahead, i'm not the one making the mod afterall.

    public GameObject currentProjectile; // Do not Interact with this unless you know what you're doing.

    public int maxHealth = 10; // Change this to whatever you want in the inspector.

    private int health;

    [Header("Projectile Spawner")]

    public float maxObjects = 10f;

    public float objects;

    public float spawnCooldown;

    [Header("MISC")]

    public bool BossFight;

    private bool realBossStart;

    public bool AntiDisable_Debug;

    [SerializeField] private Slider healthSlider;

    [SerializeField] private string onBossEnd;

    [SerializeField] private AudioClip aud_baldloonline;

    [SerializeField] private AudioClip[] NULL_Lines;

    [SerializeField] private AudioClip[] NULL_Music;

    [SerializeField] private GameControllerScript gc; // Assign the GameController GameObject Here.

    public NullScript ns;

    [SerializeField] private AudioSource audioDevice; // Same as above.

    private void Awake()
    {
        health = maxHealth;
        healthSlider.maxValue = this.maxHealth - 1;
    }

    private void Update()
    {
        if (!(health >= maxHealth))
        {
            healthSlider.value = this.health;
        }
        if (this.BossFight)
        {
            //Projectile Spawner
            if (this.objects > maxObjects)
            {
                this.objects = maxObjects;
            }
            if (this.spawnCooldown > 0f)
            {
                this.spawnCooldown -= Time.deltaTime;
            }
            if (this.spawnCooldown <= 0f)
            {
                if (this.objects < maxObjects)
                {
                    GameObject AIPoint = AIPoints[Random.Range(0, AIPoints.Length)];
                    GameObject projectile = Instantiate(projectileprefabs[Random.Range(0, projectileprefabs.Length)], AIPoint.transform.position, AIPoint.transform.rotation);
                    projectile.transform.position += Vector3.up * 4f;
                    this.objects++;
                }
                this.spawnCooldown = UnityEngine.Random.Range(5f, 25f);
            }
        }
    }
    
	public IEnumerator WaitForNULL()
	{
        if (PlayerPrefs.GetInt("NullDefeated") >= 1)
        {
            gc.baldiScrpt.baldiRenderer.enabled = true;
        }
        else
        {
            gc.baldiScrpt.Nsprite.SetActive(true);
        }
		ns.agent.speed = 100f;
		ns.target = this.gc.entrances[gc.lastestExit].bossSpawn.transform;
		while (ns.gameObject.transform.position.x != gc.entrances[gc.lastestExit].bossSpawn.transform.position.x && ns.gameObject.transform.position.z != gc.entrances[gc.lastestExit].bossSpawn.transform.position.z)
		{
			yield return null;
		}
		BossIntro(gc.lastestExit);
    }

    public void BossIntro(int ExitID)
    {
        this.audioDevice.PlayOneShot(this.gc.aud_Switch, 0.8f);
        this.BossFight = true;
        this.gc.debugMode = true;
        this.ns.agent.speed = 0f;
        //this.ns.agent.Warp(this.gc.entrances[ExitID].bossSpawn.transform.position); // Null Spawns on assigned spawn in the Entrance
        this.gc.entrances[ExitID].Lower();
        PickupScript[] pickupScripts = Resources.FindObjectsOfTypeAll<PickupScript>(); // Disable all of the Pick ups's scripts
        foreach (PickupScript pickUp in pickupScripts)
        {
            pickUp.enabled = false;
        }
        this.gc.player.runSpeed = this.gc.player.walkSpeed;
        this.ns.gameObject.SetActive(true);
        this.ns.agent.isStopped = true;
        if (PlayerPrefs.GetInt("NullDefeated") == 0) this.ns.audioMachine.PlayOneShot(NULL_Lines[0]);
        this.audioDevice.clip = this.NULL_Music[0];
        this.audioDevice.loop = true;
        this.audioDevice.loop = true;
        this.audioDevice.Play();
        this.gc.player.hud.enabled = false;
        this.gc.LoseItem(0);
        this.gc.LoseItem(1);
        this.gc.LoseItem(2);
        if (PlayerPrefs.GetInt("NullDefeated") == 0) StartCoroutine(WaitForPlayer());
    }

    public IEnumerator WaitForPlayer()
    {
        float time = NULL_Lines[0].length;
    	while (time > 0f & health == maxHealth)
		{
            if (Time.timeScale != 0f)
            {
                time -= Time.deltaTime;
            }
			yield return null;
		}
        if (health == maxHealth)
        {
            this.ns.audioMachine.clip = NULL_Lines[1];
            this.ns.audioMachine.loop = true;
            this.ns.audioMachine.Play();
        }
    }

    private void BossBegin()
    {
        this.ns.enabled = true;
        this.ns.target = gc.player.gameObject.transform;
        this.ns.agent.isStopped = false;
        if (!AntiDisable_Debug) this.gc.debugMode = false;
        this.healthSlider.gameObject.SetActive(true);
        this.ns.agent.speed = 20f;
        this.BossIncrease(10f, 0f);
        this.audioDevice.clip = this.NULL_Music[2];
        this.audioDevice.loop = true;
        this.audioDevice.Play();
    }

    public void BossIncrease(float playerSpeed, float musicPitch)
    {
        if (realBossStart)
        {
            this.gc.player.walkSpeed += playerSpeed;
            this.gc.player.runSpeed = this.gc.player.walkSpeed;
            this.audioDevice.pitch += musicPitch;
        }
    }

    public IEnumerator Hit()
    {
        this.health--;
        if (health <= 0)
        {
            if (PlayerPrefs.GetInt("NullDefeated") >= 1)
            {
                PlayerPrefs.SetInt("UnlockNull", 1);
            }
            PlayerPrefs.SetInt("NullDefeated", 1);
            SceneManager.LoadScene(onBossEnd); // Change this to the scene you want to end up at after the fight is over!
        }
        if (PlayerPrefs.GetInt("NullDefeated") == 0)
        {
            yield return new WaitForSeconds(this.ns.Aud_Hit[0].length);
        }
        else
        {
            yield return new WaitForSeconds(this.ns.Aud_Hit[1].length);
        }
        this.AfterHit();
        yield return null;
    }

    public void AfterHit()
    {
        if (this.BossFight)
        {
            StartCoroutine(ns.AfterHit());
            if (health <= maxHealth - 1 & !realBossStart)
            {
                realBossStart = true;
                this.ns.agent.isStopped = true;
                this.spawnCooldown = this.NULL_Music[1].length;
                if (PlayerPrefs.GetInt("NullDefeated") >= 1)
                {
                    this.ns.audioMachine.PlayOneShot(this.aud_baldloonline);
                }
                else
                {
                    this.ns.audioMachine.PlayOneShot(this.NULL_Lines[2]);
                }
                this.audioDevice.clip = this.NULL_Music[1];
                this.audioDevice.loop = false;
                this.audioDevice.Play();
                DeleteProjectiles();
                StartCoroutine(this.BeforeBegin());
                return;
            }
            if (health < maxHealth - 1)
            {
                BossIncrease(4f, 0.01f);
                return;
            }
            if (this.health == 1)
            {
                DeleteProjectiles();
                this.maxObjects = 1f;
                this.spawnCooldown = 5f;
                return;
            }
        }
    }

    private IEnumerator BeforeBegin()
    {
        yield return new WaitForSeconds(this.NULL_Music[1].length);
        this.BossBegin();
        yield break;
    }

    private void DeleteProjectiles()
    {
        foreach (GameObject projectile in GameObject.FindGameObjectsWithTag("Projectile")) // Removes all projectiles
        {
            if (currentProjectile != projectile) // checks if one of the projectiles isnt the one that the player is holding
            {
                if (!projectile.GetComponent<ProjectileScript>().Thrown) // if projectile hasnt been thrown
                {
                    Destroy(projectile);
                    this.objects--;
                }
            }
        }
    }
}
