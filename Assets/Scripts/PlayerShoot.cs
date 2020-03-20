
using UnityEngine;
using UnityEngine.Networking;

public class PlayerShoot : NetworkBehaviour
{
    public PlayerWeapon weapon;
    // Start is called before the first frame update
    private const string playerTag = "Player";
    [SerializeField]
    private Camera cam;

    [SerializeField]
    private LayerMask mask;
    void Start()
    {
        if (cam == null)
        {
            Debug.LogError("PlayerShoot: no camera referenced");
            this.enabled=false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }
    [Client]
    void Shoot()
    {
        RaycastHit hit;
        if(Physics.Raycast(cam.transform.position,cam.transform.forward,out hit, weapon.range, mask))
        {
      
            if(hit.collider.tag==playerTag)
            {
                CmdPLayerShot(hit.collider.name);
            }
        }
    }
    [Command]
    void CmdPLayerShot(string player_id)
    {
        Debug.Log(player_id + "has been shot");
    }
}
