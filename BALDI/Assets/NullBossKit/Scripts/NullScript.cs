using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class NullScript : MonoBehaviour
{
    public NavMeshAgent agent;

    public Transform target;

    public AudioClip[] Aud_Hit;

    public AudioSource audioMachine;

    public bool Hit;

    public GameControllerScript gc;

    private void Start()
    {
		this.audioMachine = base.GetComponent<AudioSource>();
    }

    private void Update()
    {
        agent.SetDestination(target.position);
		if (gc.style == "glitch")
		{
			foreach (WindowScript w in FindObjectsOfType<WindowScript>())
			{
                if (!w.broken)
                {
                    if (Vector3.Distance(transform.position, w.transform.position) < 5)
                    {
                        w.BreakWindow();
                    }
                }
			}
		}
    }

    private void LateUpdate()
    {
        if (Hit)
        {
            if (PlayerPrefs.GetInt("NullDefeated") >= 1)
            {
                gc.baldiScrpt.baldiRenderer.color = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
            }
            else
            {
                gc.baldiScrpt.Nsprite.GetComponent<SpriteRenderer>().color = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
            }
        }
        else
        {
            if (PlayerPrefs.GetInt("NullDefeated") >= 1)
            {
                gc.baldiScrpt.baldiRenderer.color = Color.white;
            }
            else
            {
                gc.baldiScrpt.Nsprite.GetComponent<SpriteRenderer>().color = Color.white;
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Projectile" && other.GetComponent<ProjectileScript>() != null)
        {
            if (other.GetComponent<ProjectileScript>().Thrown)
            {
                Destroy(other.gameObject);
                this.gc.debugMode = true;
                this.Hit = true;
                this.audioMachine.Stop();
                if (PlayerPrefs.GetInt("NullDefeated") == 0)
                {
                    this.audioMachine.PlayOneShot(this.Aud_Hit[0]);
                }
                else
                {
                    this.audioMachine.PlayOneShot(this.Aud_Hit[1]);
                }
                this.agent.isStopped = true;
                StartCoroutine(gc.bossController.Hit());
            }
        }
        if (gc.exitsReached == 3 & other.name == "Office Trigger")
        {
            if (PlayerPrefs.GetInt("NullDefeated") >= 1)
            {
                gc.baldiScrpt.baldiRenderer.enabled = false;
            }
            else
            {
                gc.baldiScrpt.Nsprite.SetActive(false);
            }
        }
    }

    public IEnumerator AfterHit()
    {
        if (PlayerPrefs.GetInt("NullDefeated") == 0)
        {
            yield return new WaitForSeconds(Aud_Hit[0].length);
        }
        else
        {
            yield return new WaitForSeconds(Aud_Hit[1].length);
        }
        if (!gc.bossController.AntiDisable_Debug) this.gc.debugMode = false;
        this.Hit = false;
        this.agent.isStopped = false;
        this.agent.speed += 4f;
        yield break;
    }
}