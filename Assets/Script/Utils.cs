using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public enum Direction
{
    UPLEFT,
    UPRIGHT,
    LEFT,
    RIGHT,
    DOWNLEFT,
    DOWNRIGHT,
}

public enum Owner
{
    PLAYER,
    ENEMY, // also used as player 2
}

public class MapPosition
{
    public readonly int x;
    public readonly int y;

    public bool Equals(MapPosition other)
    {
        if (other == null) return false;
        return x == other.x && y == other.y;
    }

    public static bool operator ==(MapPosition a, MapPosition b)
    {
        // If both are null, or both are same instance, return true.
        if (ReferenceEquals(a, b)) {
            return true;
        }

        // If one is null, but not both, return false.
        if (((object) a == null) || ((object) b == null)) {
            return false;
        }

        // Return true if the fields match:
        return a.x == b.x && a.y == b.y;
    }

    public static bool operator !=(MapPosition a, MapPosition b)
    {
        return !(a == b);
    }

    public MapPosition(int xx, int yy)
    {
        x = xx;
        y = yy;
    }

    /// <summary>
    /// Returns the distances in tiles (how many tiles you would have to walk) between two positions
    /// </summary>
    /// <param name="a">From position</param>
    /// <param name="b">To position</param>
    /// <returns>Distance in tiles</returns>
    public static int Distance(MapPosition a, MapPosition b)
    {
        float deltaX = Mathf.Pow(a.x - b.x, 2);
        float deltaY = Mathf.Pow(a.y - b.y, 2);
        float squared = Mathf.Sqrt(deltaX + deltaY);

        return (int) Mathf.Round(squared);
    }

    public override string ToString()
    {
        return "MapPosition(" + x + "," + y + ")";
    }

    // returns a mapposition with coords in the given direction relative to this position
    public MapPosition InDirection(Direction Dir)
    {
        int nx;
        int ny;

        bool rowIsOdd = y%2 == 1;

        switch (Dir) {
            case Direction.UPLEFT:
                if (rowIsOdd) {
                    return new MapPosition(x - 1, y + 1);
                }
                return new MapPosition(x, y+1);
            case Direction.UPRIGHT:
                if (rowIsOdd) {
                    return new MapPosition(x, y + 1);
                }
                return new MapPosition(x + 1, y + 1);
            case Direction.LEFT:
                return new MapPosition(x - 1, y);
            case Direction.RIGHT:
                return new MapPosition(x + 1, y);
            case Direction.DOWNLEFT:
                if (rowIsOdd) {
                    return new MapPosition(x - 1, y - 1);
                }
                return new MapPosition(x, y - 1);
            case Direction.DOWNRIGHT:
                if (rowIsOdd) {
                    return new MapPosition(x, y - 1);
                }
                return new MapPosition(x + 1, y - 1);
            default:
                return null;
        }
    }

    public static implicit operator Vector2(MapPosition pos)
    {
        return new Vector2(pos.x, pos.y);
    }
}

public class Utils
{
    public static bool OutOfBounds(MapPosition position)
    {
        if (position.x < 0 || position.y < 0 || position.y > 5) {
            return true;
        }

        //
        if (position.y%2 == 0) {
            if (position.x > 14)
                return true;
        }
        else {
            if (position.x > 15) {
                return true;
            }
        }
        return false;
    }

    //Util map functions
    public static List<MapPosition> GetAdjacent(MapPosition position)
    {
        var adj = new List<MapPosition>();

        // Iterate over all the directions and check if they are valid positions
        foreach (Direction dir in Enum.GetValues(typeof (Direction))) {
            MapPosition tile = position.InDirection(dir);
            if (!OutOfBounds(tile)) {
                adj.Add(tile);
            }
        }

        return adj;
    }

    /// <summary>
    /// Is position A adjacent to position B?
    /// </summary>
    /// <param name="a"></param>
    /// <param name="b"></param>
    /// <returns>True if A is Adjacent to B</returns>
    public static bool IsAdjacent(MapPosition a, MapPosition b)
    {
        // Not exactly optimized
        foreach (Direction d in Enum.GetValues(typeof (Direction))) {
            MapPosition indir = a.InDirection(d);
            bool IsEqual = b == indir;
            if (a.InDirection(d) == b) {
                return true;
            }
        }
        return false;
    }

    /// <summary>
    /// Returns true if we are at the last tile of a lane
    /// </summary>
    /// <param name="pos"></param>
    /// <param name="faction"></param>
    /// <returns></returns>
    public static bool IsAtEndOfLane(MapPosition pos, Owner faction)
    {
        if (faction == Owner.PLAYER) {
            int add = pos.y%2 == 1 ? 1 : 0;

            if (pos.x == 13 + add) {
                return true;
            }

            return false;
        }
        if (faction == Owner.ENEMY) {
            if (pos.x == 0) {
                return true;
            }

            return false;
        }
        throw new Exception("Unknown faction used: " + faction);
    }
}