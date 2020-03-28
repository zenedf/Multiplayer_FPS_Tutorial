using UnityEngine;
using Mirror;
using System;

public class PlayerShoot : NetworkBehaviour
{
    private const string PLAYER_TAG = "Player";

    public PlayerWeapon weapon;

    [SerializeField] private Camera cam;

    [SerializeField] private LayerMask mask;

    void Start()
    {
        if (cam == null)
        {
            Debug.LogError("PlayerShoot: No camera referenced");
            this.enabled = false;
        }
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }

    /// <summary>
    /// TODO
    /// </summary>
    [Client] // This is only called on the client. Never on the server.
    void Shoot()
    {
        RaycastHit _hit;
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out _hit, weapon.range, mask))
        {
            //Debug.Log("We hit" + _hit.collider.name);

            if (_hit.collider.tag == PLAYER_TAG)
            {
                CmdPlayerShot(_hit.collider.name);
            }
        }

    }

    [Command] // Commands are methods that are called only on the server.
    void CmdPlayerShot(string _ID)
    {
        Debug.Log(_ID + " has been shot.");
    }
}
