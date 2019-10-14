using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public Transform cameraT;

    public float turnSmoothTime = 0.2f;
    float turnSmoothVelocity;

    public float speedSmoothTime = 0.1f;
    float speedSmoothVelocity;
    float currentSpeed;

    public GameObject boulox;

    void Update()
    {
        Movement();
        Action();
    }

    void Action()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Instantiate(boulox, new Vector3(transform.position.x, 1.5f, transform.position.z) + transform.forward * 2, transform.rotation);
        }
    }

    void Movement()
    {
        Vector3 input = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        Vector3 inputDir = input.normalized;

        Vector3 direction = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(-angle + 90f, Vector3.up);

        float targetSpeed = speed * inputDir.magnitude;
        currentSpeed = Mathf.SmoothDamp(currentSpeed, targetSpeed, ref speedSmoothVelocity, speedSmoothTime);

        transform.Translate(inputDir * currentSpeed * Time.deltaTime, Space.World);
    }
}
