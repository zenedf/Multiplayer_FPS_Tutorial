using UnityEngine;
using Mirror;

/// <summary>
/// TODO
/// </summary>
[RequireComponent(typeof(Player))]
public class PlayerSetup : NetworkBehaviour
{
    [SerializeField] Behaviour[] componentsToDisable;

    [SerializeField] string remoteLayerName = "RemotePlayer";

    Camera sceneCamera;

    void Start()
    {
        // check to see if we're on the network.
        if (!isLocalPlayer)
        {
            // if this object isn't controlled by the system, disable all the components.
            DisableComponents();
            //AssignRemoteLayer();
        }
        else
        {
            //Camera.main.gameObject.SetActive(false);

            sceneCamera = Camera.main;
            if (sceneCamera != null)
            {
                sceneCamera.gameObject.SetActive(false);
            }
        }

    }

    public override void OnStartClient() // called every time a client is set up locally
    {
        base.OnStartClient();


        string _netID = GetComponent<NetworkIdentity>().netId.ToString();
        Player _player = GetComponent<Player>();


        GameManager.RegisterPlayer(_netID, _player);
    }

    /// <summary>
    /// TODO
    /// </summary>
    void AssignRemoteLayer()
    {
        gameObject.layer = LayerMask.NameToLayer(remoteLayerName);
    }


    /// <summary>
    /// TODO
    /// </summary>
    void DisableComponents()
    {
        
        for (int i = 0; i < componentsToDisable.Length; i++)
        {
            componentsToDisable[i].enabled = false;
        }
    }

    /// <summary>
    /// This is called when an object is diabled or destroyed.
    /// </summary>
    void OnDisable()
    {
        if (sceneCamera != null)
        {
            sceneCamera.gameObject.SetActive(true);
        }

        //GameManager.UnRegisterPlayer(transform.name);
    }
}
