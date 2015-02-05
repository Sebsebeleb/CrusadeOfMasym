using System;
using System.Collections.Generic;

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
    public int x;
    public int y;

    public MapPosition(int xx, int yy)
    {
        x = xx;
        y = yy;
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
        switch (Dir) {
            case Direction.UPLEFT:
                return new MapPosition(x - 1, y + 1);
            case Direction.UPRIGHT:
                return new MapPosition(x, y + 1);
            case Direction.LEFT:
                return new MapPosition(x - 1, y);
            case Direction.RIGHT:
                return new MapPosition(x + 1, y);
            case Direction.DOWNLEFT:
                return new MapPosition(x - 1, y - 1);
            case Direction.DOWNRIGHT:
                return new MapPosition(x, y - 1);
        }
    }
}

internal class Utils
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
}