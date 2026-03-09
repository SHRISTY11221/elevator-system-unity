using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ElevatorController : MonoBehaviour
{
    public float speed = 2f;

    public float[] floorY = new float[5];

    public TMP_Text floorText;

    public int currentFloor = 0;

    Queue<int> requests = new Queue<int>();

    bool moving = false;
    int targetFloor = -1;
    private void Start()
    {
        floorText.text = "Floor: " + currentFloor;
    }
    void Update()
    {
        if (!moving && requests.Count > 0)
        {
            int target = requests.Dequeue();
            StartCoroutine(MoveToFloor(target));
        }
    }

    public void AddRequest(int floor)
    {
        if (floor >= 0 && floor < floorY.Length)
        {
            if (!requests.Contains(floor))
                requests.Enqueue(floor);
        }
    }

    IEnumerator MoveToFloor(int floor)
    {
        moving = true;
        targetFloor = floor;

        float targetY = floorY[floor];

        while (Mathf.Abs(transform.position.y - targetY) > 0.01f)
        {
            float newY = Mathf.MoveTowards(
                transform.position.y,
                targetY,
                speed * Time.deltaTime
            );

            transform.position = new Vector3(transform.position.x, newY, 0);

            yield return null;
        }

        transform.position = new Vector3(transform.position.x, targetY, 0);

        currentFloor = floor;
        targetFloor = -1;

        if (floorText != null)
            floorText.text = "Floor: " + currentFloor;

        moving = false;
    }

    public int Distance(int floor)
    {
        return Mathf.Abs(currentFloor - floor);
    }

    public bool IsIdle()
    {
        return !moving;
    }

    public bool HasRequest(int floor)
    {
        return requests.Contains(floor) || targetFloor == floor;
    }
}