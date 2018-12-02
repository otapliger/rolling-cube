using UnityEngine;

public class RollingCubeMovement : MonoBehaviour
{
    enum Direction { none, right, left, forward, backward };
    Direction direction;

    int rotationSpeed = 400;
    BoxCollider boxCollider;
    GameObject bottomEdge;
    Transform parent;

    void Start()
    {
        parent = transform.parent;
        boxCollider = GetComponent<BoxCollider>();
        bottomEdge = new GameObject("Edge");
    }

    void Update()
    {
        var position = transform.position;

        if (direction == Direction.none)
        {
            if (Input.GetAxis("Horizontal") > 0)
            {
                bottomEdge.transform.position = new Vector3(position.x + boxCollider.bounds.extents.x, position.y - boxCollider.bounds.extents.y, position.z);
                transform.parent = bottomEdge.transform;
                direction = Direction.right;
            }
            else if (Input.GetAxis("Horizontal") < 0)
            {
                bottomEdge.transform.position = new Vector3(position.x - boxCollider.bounds.extents.x, position.y - boxCollider.bounds.extents.y, position.z);
                transform.parent = bottomEdge.transform;
                direction = Direction.left;
            }
            else if (Input.GetAxis("Vertical") > 0)
            {
                bottomEdge.transform.position = new Vector3(position.x, position.y - boxCollider.bounds.extents.y, position.z + boxCollider.bounds.extents.z);
                transform.parent = bottomEdge.transform;
                direction = Direction.forward;
            }
            else if (Input.GetAxis("Vertical") < 0)
            {
                bottomEdge.transform.position = new Vector3(position.x, position.y - boxCollider.bounds.extents.y, position.z - boxCollider.bounds.extents.z);
                transform.parent = bottomEdge.transform;
                direction = Direction.backward;
            }
        }

        Rotate(direction);
    }

    void Rotate(Direction direction)
    {
        switch (direction)
        {
            case Direction.right:
                bottomEdge.transform.Rotate(-Vector3.forward * Time.deltaTime * rotationSpeed);
                if (bottomEdge.transform.localEulerAngles.z <= 275)
                    EndRotation(new Vector3(bottomEdge.transform.localEulerAngles.x, bottomEdge.transform.localEulerAngles.y, 270));
                break;

            case Direction.left:
                bottomEdge.transform.Rotate(Vector3.forward * Time.deltaTime * rotationSpeed);
                if (bottomEdge.transform.localEulerAngles.z >= 85)
                    EndRotation(new Vector3(bottomEdge.transform.localEulerAngles.x, bottomEdge.transform.localEulerAngles.y, 90));
                break;

            case Direction.forward:
                bottomEdge.transform.Rotate(Vector3.right * Time.deltaTime * rotationSpeed);
                if (bottomEdge.transform.localEulerAngles.x >= 85)
                    EndRotation(new Vector3(90, bottomEdge.transform.localEulerAngles.y, bottomEdge.transform.localEulerAngles.z));
                break;

            case Direction.backward:
                bottomEdge.transform.Rotate(-Vector3.right * Time.deltaTime * rotationSpeed);
                if (bottomEdge.transform.localEulerAngles.x <= 275)
                    EndRotation(new Vector3(270, bottomEdge.transform.localEulerAngles.y, bottomEdge.transform.localEulerAngles.z));
                break;

            default:
                break;
        }
    }

    void EndRotation(Vector3 eulerAngles)
    {
        bottomEdge.transform.localEulerAngles = eulerAngles;
        transform.parent = parent;
        bottomEdge.transform.rotation = Quaternion.identity;
        direction = Direction.none;
    }
}
