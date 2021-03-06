// Date   : 11.08.2018 03:49
// Project: LD42
// Author : bradur

using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{

    private Direction direction = Direction.None;
    [SerializeField]
    private float speed = 1f;
    [SerializeField]
    private Rigidbody rb;

    private Vector3 velocityUp;
    private Vector3 velocityRight;
    private Vector3 velocityDown;
    private Vector3 velocityLeft;

    enum Direction
    {
        None,
        North,
        East,
        South,
        West
    }

    void Start()
    {
        velocityUp = rb.velocity = new Vector3(0, 0, speed);
        velocityRight = rb.velocity = new Vector3(speed, 0, 0);
        velocityDown = rb.velocity = new Vector3(0, 0, -speed);
        velocityLeft = rb.velocity = new Vector3(-speed, 0, 0);
    }

    private void SetDirection(Direction newDirection)
    {
        if (direction != newDirection)
        {
            direction = newDirection;
        }
    }

    private void SetRotation(Direction newDirection)
    {
        if (direction != newDirection)
        {
            if (newDirection == Direction.North)
            {
                transform.localRotation = Quaternion.Euler(0, 0, 0);
            }
            else if (newDirection == Direction.East)
            {
                transform.localRotation = Quaternion.Euler(0, 0, -90);
            }
            else if (newDirection == Direction.South)
            {
                transform.localRotation = Quaternion.Euler(0, 0, -180);
            }
            else if (newDirection == Direction.West)
            {
                transform.localRotation = Quaternion.Euler(0, 0, -270);
            }
        }
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            SetRotation(Direction.North);
            SetDirection(Direction.North);
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            SetRotation(Direction.East);
            SetDirection(Direction.East);
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            SetRotation(Direction.South);
            SetDirection(Direction.South);
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            SetRotation(Direction.West);
            SetDirection(Direction.West);
        }
        else
        {
            SetDirection(Direction.None);
        }
    }

    private void FixedUpdate()
    {
        if (direction == Direction.North)
        {
            rb.velocity = velocityUp;
        }
        else if (direction == Direction.East)
        {
            rb.velocity = velocityRight;
        }
        else if (direction == Direction.South)
        {
            rb.velocity = velocityDown;
        }
        else if (direction == Direction.West)
        {
            rb.velocity = velocityLeft;
        }
        else
        {
            rb.velocity = Vector3.zero;
        }
    }
}
