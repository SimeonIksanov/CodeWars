// https://www.codewars.com/kata/58905bfa1decb981da00009e

using System;
using System.Drawing;
using System.Linq;

namespace CodeWars;

public class Dinglemouse
{
    public static int[] TheLift(int[][] queues, int capacity)
    {
        List<List<Human>> peopleWaiting = ConvertQueuesToHumansList(queues);
        var lift = new Lift((uint)capacity, peopleWaiting);
        lift.Run();
        return lift.FloorsStopedAt.ToArray();
    }

    private static List<List<Human>> ConvertQueuesToHumansList(int[][] queues)
    {
        var peopleWaiting = new List<List<Human>>();
        for (var floor = 0; floor < queues.Length; floor++)
        {
            var peopleOnFloor = new List<Human>();
            foreach (var goingto in queues[floor])
            {
                var human = new Human(goingto);
                peopleOnFloor.Add(human);
            }
            peopleWaiting.Add(peopleOnFloor);
        }

        return peopleWaiting;
    }
}

public class Lift
{
    private uint _capacity;
    private int _currentFloor;
    private int _minFloor;
    private int _maxFloor;
    private Direction _direction;
    private readonly List<Human> _people = new();
    private readonly List<List<Human>> _peopleWaiting;

    public readonly List<int> FloorsStopedAt = new();

    public Lift(uint capacity, List<List<Human>> peopleWaiting)
    {
        _capacity = capacity;
        _peopleWaiting = peopleWaiting;
        _minFloor = 0;
        _currentFloor = _minFloor;
        _maxFloor = peopleWaiting.Count - 1;
        FloorsStopedAt.Add(_currentFloor);
    }

    public Direction Direction => _direction;
    public bool IsFull => _people.Count >= _capacity;

    public void Run()
    {
        GoTo(_currentFloor); // started here
        while (HasWaitingPeople() || !IsEmpty())
        {
            PassengerOut();
            PassengerIn();
            int? nextStop = FindNextStopAlongDirection(_currentFloor);
            if (!nextStop.HasValue)
            {
                ChangeDirection();
                nextStop = _direction == Direction.Up
                    ? FindLowestWaitingPassangerGoingUp()
                    : FindHighestWaitingPassangerGoingDown();
            }
            if (nextStop.HasValue)
                GoTo(nextStop.Value);
            else
                continue;
        }
        GoTo(_minFloor);
    }


    private void PassengerIn()
    {
        var newPassangers = new List<Human>();
        var humansOnFloor = _peopleWaiting[_currentFloor];

        for (int i = 0; i < humansOnFloor.Count; i++)
        {
            if (IsFull) break;
            if (_direction == Direction.Up && humansOnFloor[i].To > _currentFloor)
            {
                PassangerEnters(newPassangers, humansOnFloor, i);
            }
            else if (_direction == Direction.Down && humansOnFloor[i].To < _currentFloor)
            {
                PassangerEnters(newPassangers, humansOnFloor, i);
            }
        }

        foreach (var human in newPassangers)
        {
            humansOnFloor.Remove(human);
        }

        void PassangerEnters(List<Human> newPassangers, List<Human> humansOnFloor, int i)
        {
            newPassangers.Add(humansOnFloor[i]);
            _people.Add(humansOnFloor[i]);
        }
    }

    private void PassengerOut()
    {
        for (int i = _people.Count - 1; i >= 0; i--)
        {
            if (_people[i].To == _currentFloor)
                _people.Remove(_people[i]);
        }
    }

    private int? FindNextStopAlongDirection(int fromFloor)
    {
        int? nextPassangerStop = NextPassangerStop();
        if (_direction == Direction.Up)
        {
            var lowestWaitingGoingUp = FindLowestWaitingPassangerGoingUp(fromFloor + 1);
            return lowestWaitingGoingUp is null
                ? nextPassangerStop
                : nextPassangerStop is null
                    ? lowestWaitingGoingUp.Value
                    : Math.Min(nextPassangerStop.Value, lowestWaitingGoingUp.Value);
        }
        else
        {
            var highestWaitingGoingUp = FindHighestWaitingPassangerGoingDown(fromFloor - 1);
            return highestWaitingGoingUp is null
                ? nextPassangerStop
                : nextPassangerStop is null
                    ? highestWaitingGoingUp.Value
                    : Math.Max(nextPassangerStop.Value, highestWaitingGoingUp.Value);
        }
    }

    private int? NextPassangerStop()
    {
        if (_people.Count == 0)
            return null;

        return _direction == Direction.Up
            ? _people.Min(h => h.To)
            : _people.Max(h => h.To);
    }

    private int? FindLowestWaitingPassangerGoingUp()
    {
        return FindLowestWaitingPassangerGoingUp(_minFloor);
    }
    private int? FindLowestWaitingPassangerGoingUp(int fromFloor)
    {
        while (fromFloor <= _maxFloor)
        {
            if (_peopleWaiting[fromFloor].Any(human => fromFloor < human.To))
            {
                return fromFloor;
            }
            fromFloor++;
        }
        return null;
    }

    private int? FindHighestWaitingPassangerGoingDown()
    {
        return FindHighestWaitingPassangerGoingDown(_maxFloor);
    }
    private int? FindHighestWaitingPassangerGoingDown(int fromFloor)
    {
        while (_minFloor <= fromFloor)
        {
            if (_peopleWaiting[fromFloor].Any(human => fromFloor > human.To))
            {
                return fromFloor;
            }
            fromFloor--;
        }
        return null;
    }

    private void ChangeDirection()
    {
        _direction = _direction == Direction.Up ? Direction.Down : Direction.Up;
    }

    private void GoTo(int floor)
    {
        if (_currentFloor != floor)
        {
            FloorsStopedAt.Add(floor);
            _currentFloor = floor;
        }
    }

    private bool HasWaitingPeople() =>_peopleWaiting.Any(pw => pw.Count > 0);

    private bool IsEmpty() => _people.Count == 0;
}

public enum Direction
{
    Up,
    Down
}

public record Human(int To)
{
    public int To { get; private set; } = To;
}