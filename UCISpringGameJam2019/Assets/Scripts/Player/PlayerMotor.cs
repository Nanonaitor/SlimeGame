using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerMotor
{
    private CharacterController controller;
    private Transform transform;

    [SerializeField] private float speed;
    public float Speed { get => speed; set => speed = value; }

    private Vector3 velocity;
    float turnDirection;
    float forwardDirection;

    public PlayerMotor(CharacterController c, PlayerMotor motor, Transform t)
    {
        controller = c;
        transform = t;

        speed = motor.speed;
    }

    public void Move(float deltaTime, float horizontal, float vertical)
    {
        velocity = new Vector3(horizontal, 0, vertical);
        velocity = Quaternion.Euler(0, 0 - transform.eulerAngles.y + Camera.main.transform.eulerAngles.y, 0) * velocity;

        turnDirection = velocity.x;
        forwardDirection = velocity.z;

        velocity = transform.TransformDirection(velocity);
        velocity = Vector3.ClampMagnitude(velocity, 1);
        velocity *= speed;

        controller.Move(velocity * deltaTime);
    }
}

