using UnityEngine;
using Mirror;

public class PlayerSetup : NetworkBehaviour
{
    [SerializeField] Behaviour[] componentsToDisable;

    void Start()
    {
        // check to see if we're on the network.
        if (!isLocalPlayer)
        {
            // if this object isn't controlled by the system, disable all the components.
            for (int i = 0; i < componentsToDisable.Length; i++)
            {
                componentsToDisable[i].enabled = false;
            }
        }
    }
}
