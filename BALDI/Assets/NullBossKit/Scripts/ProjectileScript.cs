using System;
using UnityEngine;

public class ProjectileScript : MonoBehaviour
{
    private GameObject cm;

    private bool pickedUp;

    public bool Thrown;

    private GameControllerScript gc;

    private Rigidbody rb;

    public int spawnID;

    private static int nextID = 0;



    private void Start()
    {
        rb = base.GetComponent<Rigidbody>();
        cm = GameObject.FindWithTag("MainCamera");
        gc = GameObject.FindWithTag("GameController").GetComponent<GameControllerScript>();
        base.GetComponent<BsodaSparyScript>().enabled = false;
        spawnID = nextID++;
    }

    private void Update()
    {
        if (this.pickedUp)
        {
            if (Input.GetMouseButtonDown(0) && Time.timeScale != 0f)
            {
                this.pickedUp = false;
                this.Thrown = true;
                base.transform.position += Vector3.up * 2f;
                this.gc.bossController.currentProjectile = null;
                this.gc.bossController.objects -= 1f;
            }
            if (this.Thrown)
            {
                this.GetComponent<BsodaSparyScript>().enabled = true;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && !this.pickedUp & !this.Thrown)
        {
            if (gc.bossController.currentProjectile == null)
            {
                this.gc.bossController.currentProjectile = base.gameObject;
                this.pickedUp = true;
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Projectile" && !other.GetComponent<ProjectileScript>().pickedUp && !other.GetComponent<ProjectileScript>().Thrown && !this.pickedUp && !this.Thrown)
        {
            if (gc.bossController.currentProjectile != other.gameObject && other.GetComponent<ProjectileScript>().spawnID > this.spawnID)
            {
                UnityEngine.Object.Destroy(other.gameObject, 0f);
                this.gc.bossController.objects -= 1f;
            }
        }
    }

    private void LateUpdate()
    {
        if (this.pickedUp && !this.Thrown)
        {
            if (!Input.GetButton("Look Behind"))
            {
                base.transform.position = this.gc.player.transform.position + this.gc.player.transform.forward * 3f + Vector3.up * -2f;
                base.transform.rotation = this.cm.transform.rotation;
            }
            else
            {
                base.transform.position = this.gc.player.transform.position + this.gc.player.transform.forward * -3f + Vector3.up * -2f;
                base.transform.rotation = this.cm.transform.rotation;
            }
        }
    }
}