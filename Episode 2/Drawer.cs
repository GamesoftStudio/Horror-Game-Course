using UnityEngine;

public class Drawer : Openable
{
    void Update()
    {
        Vector3 targetPos = isOpen ? openVector : closeVector;

        transform.localPosition = Vector3.Lerp(transform.localPosition, targetPos, speed *  Time.deltaTime);
    }
}
