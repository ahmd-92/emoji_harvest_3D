using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class AnimateProduct : MonoBehaviour
{
    public float rotationSpeed_X;
    public float rotationSpeed_Y;
    public float rotationSpeed_Z;

    private void Start()
    {
        if (rotationSpeed_X > 0)
            rotationSpeed_X = Random.Range(rotationSpeed_X - 10, rotationSpeed_X + 10);

        if (rotationSpeed_Y > 0)
            rotationSpeed_Y = Random.Range(rotationSpeed_Y - 10, rotationSpeed_Y + 10);

        if (rotationSpeed_Z > 0)
            rotationSpeed_Z = Random.Range(rotationSpeed_Z - 10, rotationSpeed_Z + 10);
    }

    private void FixedUpdate()
    {
        transform.eulerAngles += new Vector3(rotationSpeed_X * Time.deltaTime, rotationSpeed_Y * Time.deltaTime, rotationSpeed_Z * Time.deltaTime);

    }

}
