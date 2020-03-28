using UnityEngine;
using Mirror;

/// <summary>
/// TODO
/// </summary>
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
            AssignRemoteLayer();
        }
        else
        {
            sceneCamera = Camera.main;
            if (sceneCamera != null)
            {
                Camera.main.gameObject.SetActive(false);
            }
        }

        RegisterPlayer();

    }

    /// <summary>
    /// Assigning a player a unique ID, and therefore registering him in the scene.
    /// </summary>
    void RegisterPlayer()
    {
        string _ID = "Player " + GetComponent<NetworkIdentity>().netId;
        transform.name = _ID;
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
    }
}
