using UnityEngine;

public class ElevatorManager : MonoBehaviour
{
    public ElevatorController[] elevators;

    public void RequestFloor(int floor)
    {
        foreach (ElevatorController e in elevators)
        {
            if (e.HasRequest(floor))
                return;
        }

        ElevatorController best = null;
        int bestDistance = int.MaxValue;

        foreach (ElevatorController e in elevators)
        {
            if (e.IsIdle())
            {
                int d = e.Distance(floor);

                if (d < bestDistance)
                {
                    bestDistance = d;
                    best = e;
                }
            }
        }

        if (best == null)
        {
            foreach (ElevatorController e in elevators)
            {
                int d = e.Distance(floor);

                if (d < bestDistance)
                {
                    bestDistance = d;
                    best = e;
                }
            }
        }

        if (best != null)
            best.AddRequest(floor);
    }
}