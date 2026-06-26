using UnityEngine;

public class CubeDestroy : MonoBehaviour, IInteractable
{
    public void Interact(PlayerInteraction player)
    {
        Debug.Log(gameObject.name);
        Destroy(gameObject);
    }
}
