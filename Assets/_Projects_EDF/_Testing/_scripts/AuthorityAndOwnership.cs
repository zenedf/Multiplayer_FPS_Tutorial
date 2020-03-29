using Mirror;
using UnityEngine;

public class AuthorityAndOwnership : NetworkBehaviour
{
    private GameObject _somePrefab;

    // Update is called once per frame
    private void Update()
    {
        bool playerDead = false;

        // I'm using the base.hasAuthority to check for authority beforecalling RequestRespawn()
        //if (playerDead && networkIdentity.hasAuthority)
        if (playerDead && base.hasAuthority)
        {
            CmdRequestRespawn();
        }

        RequestOwnershipOnClick();
    }

    [Command] // commands are run on the server.
    private void CmdRequestRespawn()
    {
        NetworkServer.Spawn(_somePrefab, GetComponent<NetworkIdentity>().connectionToClient);

        //NetworkServer.SpawnWithClientAuthority(_somePrefab, GetComponent<NetworkIdentity>().connectionToClient);  // This deprecated.
    }


    private void RequestOwnershipOnClick()
    {
        // This sends a command. Since commands cannot be seen without authority, there is no reason to continue if the client does not have authority.
        if (!base.hasAuthority)
        {
            return; 
        }

        // Mouse not pressed, exit method.
        if (!Input.GetKeyDown(KeyCode.Mouse0))
        {
            return;
        }

        RaycastHit hit;
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100))
        {
            NetworkIdentity id = hit.collider.GetComponent<NetworkIdentity>();
            // If other object has a NetworkIdentity and client doesn't already own that object, then request autority for it
            if (id != null && !id.hasAuthority)
            {
                Debug.Log("Sending request authority for " + hit.collider.gameObject.name);
                CmdRequestAuthority(id);
            }
        }
    }

    [Command]
    private void CmdRequestAuthority(NetworkIdentity otherId)
    {
        Debug.Log("Receieved request authority for " + otherId.gameObject.name);
        // Let's assume this method is being run on PlayerA while otherId belongs to PlayerB.
        // We are telling the NetworkIdentity on PlayerB to assign authority to the NetworkIdentity that
        // owns the object this script runs on, which is PlayerA.
        otherId.AssignClientAuthority(base.connectionToClient);
    }


}
