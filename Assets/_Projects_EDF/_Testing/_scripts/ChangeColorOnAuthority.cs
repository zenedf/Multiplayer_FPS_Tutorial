using UnityEngine;
using Mirror;

public class ChangeColorOnAuthority : NetworkBehaviour
{
    /// <summary>
    /// Called on a client when they receive authority over an object.
    /// </summary>
    public override void OnStartAuthority()  // This method is not called on the server.
    {
        base.OnStartAuthority();
        GetComponent<SpriteRenderer>().color = Color.blue;
    }
}
