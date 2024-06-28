using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{

    [Header("Movement")]
    public float runSpeed;
    public float maxX;
    public float inputSensitivity;


    private PlayerManager playerManager;
    private float _touchMousePos = 0;
    float screenWidth;
    float lastTouchPosX = 0;
    private float m_ZPos = 0;
    bool isTouchInput = false;
    [HideInInspector] public bool startMove = false;
    // Update is called once per frame
    void Update()
    {
        Movement();
    }

    public void StartMove(PlayerManager playerM)
    {
        playerManager = playerM;
        screenWidth = Screen.width;
        isTouchInput = true;
        lastTouchPosX = Input.mousePosition.x;
        startMove = true;
    }

    private void Movement()
    {

        if (Input.GetMouseButtonUp(0))
        {
            isTouchInput = false;
        }

        if (!startMove) return;

        float _runSpeed = runSpeed * Time.deltaTime;
        m_ZPos += _runSpeed;

        if (Input.GetMouseButton(0))
        {
            if (System.Math.Abs(lastTouchPosX - Input.mousePosition.x) > 0.1)
            {
                if (isTouchInput)
                {
                    float posX = Input.mousePosition.x - lastTouchPosX;

                    // playerShapeTrans.localEulerAngles = new Vector3(playerShapeTrans.localEulerAngles.x, playerShapeTrans.localEulerAngles.y + deltaPosX, playerShapeTrans.localEulerAngles.z);
                    float _movementVectorX = (posX / screenWidth) * inputSensitivity;
                    float fullWidth = maxX * 2.0f;
                    _touchMousePos += (fullWidth * _movementVectorX);

                }

                isTouchInput = true;

            }
        }

        lastTouchPosX = Input.mousePosition.x;

        
        transform.position = new Vector3(Mathf.Clamp(_touchMousePos, -maxX, maxX), transform.position.y, m_ZPos);
    }

   /* Quaternion ClampRotation(Quaternion rotation)
    {
        //convert rotation to euler angles for clamping:
        Vector3 euler = rotation.eulerAngles;
        euler.y = Mathf.Clamp(euler.y, -maxRotationAngle, maxRotationAngle);
        return Quaternion.Euler(euler);

    }*/
}
