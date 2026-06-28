using UnityEngine;

public abstract class Openable : MonoBehaviour, IInteractable
{
    [SerializeField] protected Vector3 openVector;
    [SerializeField] protected Vector3 closeVector;

    [SerializeField] protected float speed;

    [SerializeField] protected bool isOpen = false;

    public void Interact(PlayerInteraction player)
    {
        isOpen = !isOpen;
    }
   
}
