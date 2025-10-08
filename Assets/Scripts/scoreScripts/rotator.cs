using UnityEngine;

public class rotator : MonoBehaviour
{
    public float rotateSpeed = 10.0f;

    public bool positiveX = false;
    public bool negativeX = false;

    public bool positiveY = false;
    public bool negativeY = false;

    public bool positiveZ = false;
    public bool negativeZ = false;

    private void Update()
    {
       if(positiveX)
        {
            transform.Rotate(Time.deltaTime * rotateSpeed, 0, 0, Space.Self);
        }
       if(negativeX)
        {
            transform.Rotate(-Time.deltaTime * rotateSpeed, 0, 0, Space.Self);
        }

       if(positiveY)
        {
            transform.Rotate(0, Time.deltaTime * rotateSpeed, 0, Space.Self);
        }
       if(negativeY)
        {
            transform.Rotate(0, -Time.deltaTime * rotateSpeed, 0, Space.Self);
        }

       if(positiveZ)
        {
            transform.Rotate(0, 0, Time.deltaTime * rotateSpeed, Space.Self);
        }
       if(negativeZ)
        {
            transform.Rotate(0, 0, -Time.deltaTime * rotateSpeed, Space.Self);
        }
    }
}
