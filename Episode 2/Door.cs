using UnityEngine;

public class Door : Openable
{
    // Update is called once per frame
    void Update()
    {
        Vector3 targetPos = isOpen ? openVector : closeVector;

        transform.localRotation = Quaternion.Lerp(transform.localRotation, Quaternion.Euler(targetPos), speed * Time.deltaTime);
    }
}
