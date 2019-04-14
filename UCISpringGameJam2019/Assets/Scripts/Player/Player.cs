using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private CharacterController controller;

    [SerializeField] private PlayerMotor motor;
    [SerializeField] private Weapon weapon; //For now, it is the currently equipped weapon

    private LayerMask groundMask;

    public PlayerMotor Motor { get => motor; set => motor = value; }

    void Start()
    {
        controller = GetComponent<CharacterController>();
        Motor = new PlayerMotor(controller, Motor, transform);

        groundMask = LayerMask.GetMask("Ground");
    }

    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        Motor.Move(Time.deltaTime, horizontal, vertical);
        RotateToMouse();

        if (Input.GetMouseButton(0))
        {
            if (weapon != null)
            {
                weapon.Shoot();
            }

        }
    }

    void RotateToMouse()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 2000f, groundMask))
        {
            Vector3 pos = new Vector3(hit.point.x, 0, hit.point.z);
            Vector3 relative = pos - transform.position;

            Quaternion newRot = Quaternion.LookRotation(relative);
            newRot.x = 0;
            newRot.z = 0;
            transform.rotation = newRot;
        }
    }
}
