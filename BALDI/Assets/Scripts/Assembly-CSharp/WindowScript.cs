using UnityEngine.AI;
using UnityEngine;

public class WindowScript : MonoBehaviour
{
    public bool broken;
    public MeshRenderer window_In;
    private MeshRenderer window_Out;
    private MeshCollider meshCollider_In;
    private MeshCollider meshCollider_Out;
    [SerializeField] private GameControllerScript gc;
    public Material window_Broken;
    public Material In_windowbroken;

    private void Awake()
    {
        window_Out = GetComponent<MeshRenderer>();
        meshCollider_In = window_In.gameObject.GetComponent<MeshCollider>();
        meshCollider_Out = window_Out.gameObject.GetComponent<MeshCollider>();
    }

    public void BreakWindow()
    {
        if (!this.broken)
        {
            this.gameObject.GetComponent<AudioSource>().Play();
            this.window_In.material = In_windowbroken;
            this.window_Out.material = window_Broken;
            this.meshCollider_In.enabled = false;
            this.meshCollider_Out.enabled = false;
            this.broken = true;
        }
    }

    public void Update()
    {
        if (this.broken || gc.style == "glitch")
        {
            this.gameObject.GetComponent<NavMeshObstacle>().enabled = false;
        }
    }
}
