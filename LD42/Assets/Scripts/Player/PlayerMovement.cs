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
        velocityUp = rb.velocity = new Vector3(0, speed, 0);
        velocityRight = rb.velocity = new Vector3(speed, 0, 0);
        velocityDown = rb.velocity = new Vector3(0, -speed, 0);
        velocityLeft = rb.velocity = new Vector3(-speed, 0, 0);
    }

    private void SetDirection(Direction newDirection)
    {
        if (direction == newDirection)
        {
            direction = Direction.None;
        }
        else
        {
            direction = newDirection;
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            SetDirection(Direction.North);
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            SetDirection(Direction.East);
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            SetDirection(Direction.South);
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            SetDirection(Direction.West);
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
