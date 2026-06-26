using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance;

    public PlayerInputs Inputs;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        Inputs = new PlayerInputs();
        Inputs.Enable();

        //DontDestroyOnLoad(this);
    }

    private void OnDisable()
    {
        Inputs.Disable();
    }
}
