using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : IMapDataGenerator {

    private int islands;
    private int tileCount;
    private int width;
    private int height;
    private CoordinateValidator validator;
    private static Vector2Int ERROR_VECTOR = new Vector2Int(-1, -1);
    private enum DIRECTIONS {LEFT,RIGHT,UP,DOWN };

    private struct coordMoveInfo
    {
        public Vector2Int newCoords;
        public DIRECTIONS direction;

        public coordMoveInfo (Vector2Int newCoords,DIRECTIONS direction)
        {
            this.newCoords = newCoords;
            this.direction = direction;
        }
    }

    public MapGenerator(int islands,int tileCount,int width,int height)
    {
        this.islands = islands;
        this.tileCount = tileCount;
        this.width = width;
        this.height = height;
        validator = new CoordinateValidator(width, height, 1);
    }

    public Tile[][] generateMapData()
    {
        Tile[][] map = generateEmpty(width, height);
        for (int i = 0; i <= islands; i++)
        {
            generateIsland(Random.Range(1,width - 1), Random.Range(1,height - 1), tileCount/islands, map, validator);
        }
        return map;
    }

    //Generates an island on a given map and returns the a list of the coordinates it has filled
    private static void generateIsland(int startX,int startY,int tileCount,Tile[][] map,CoordinateValidator validator)
    {
        Vector2Int currentCoords = new Vector2Int(startX, startY);
        DIRECTIONS lastDir = DIRECTIONS.DOWN;
        map[startY][startX] = tileFromCoords(currentCoords,true);
        List<Vector2Int> filledCoords = new List<Vector2Int>() { new Vector2Int(startX,startY) };
        for (int i = 0;i < tileCount - 1; i++)
        {
            List<DIRECTIONS> randomSet = getRandomSet(validator, currentCoords, map,lastDir);
            if (randomSet.Count == 0)
            {
                currentCoords = getViableCoordinates(filledCoords, validator, map,lastDir);
                if (currentCoords.Equals(ERROR_VECTOR)) { Debug.Log("no viable coords"); break; }
                randomSet = getRandomSet(validator,currentCoords,map,lastDir);
            }
            coordMoveInfo moveInfo = coordsFromRandomSet(randomSet, currentCoords);
            currentCoords = moveInfo.newCoords;
            lastDir = moveInfo.direction;
            filledCoords.Add(currentCoords);
            map[currentCoords.y][currentCoords.x] = tileFromCoords(currentCoords,true);
        }
    }

    //Instantiates tile at given coordinates * scaling factor from sprite
    private static Tile tileFromCoords(Vector2Int coords,bool groundTile)
    {
        float x = coords.x * Assets.assets.tilePrefabWidth;
        float y = coords.y * Assets.assets.tilePrefabWidth;
        if (groundTile)
        {
            return new GroundTile(x, y);
        }
        else
        {
            return new WaterTile(x, y);
        }
    }

    //Finds the coordinates of a tile with free adjacent spaces
    private static Vector2Int getViableCoordinates(List<Vector2Int> list,CoordinateValidator validator,Tile[][] map,DIRECTIONS lastDir)
    {
        for(int i = 0;i < list.Count; i++)
        {
            List<DIRECTIONS> currentSet = getRandomSet(validator, list[i], map,lastDir);
            if (currentSet.Count > 0) return list[i];
        }
        return ERROR_VECTOR;
    }

    //Helper function updating given coordinates to an available set of adjacent coordinates 
    private static coordMoveInfo coordsFromRandomSet(List<DIRECTIONS> randomSet,Vector2Int currentCoords)
    {
        DIRECTIONS direction = randomSet[Random.Range(0, randomSet.Count)];
        switch (direction)
        {
            case DIRECTIONS.LEFT:
                return new coordMoveInfo(currentCoords + new Vector2Int(-1, 0),direction);
            case DIRECTIONS.RIGHT:
                return new coordMoveInfo(currentCoords + new Vector2Int(1, 0), direction);
            case DIRECTIONS.DOWN:
                return new coordMoveInfo(currentCoords + new Vector2Int(0, -1), direction);
            case DIRECTIONS.UP:
                return new coordMoveInfo(currentCoords + new Vector2Int(0, 1), direction);
            default:
                throw new System.Exception("error in island generation");
        }
    }

    //Helper function giving set of integers to be chosen from corresponding to valid positions for next tile in generateIsland
    private static List<DIRECTIONS> getRandomSet(CoordinateValidator validator,Vector2Int currentCoords,Tile[][] map,DIRECTIONS lastDir)
    {
        List<DIRECTIONS> randomSet = new List<DIRECTIONS> {
            DIRECTIONS.LEFT, DIRECTIONS.LEFT,DIRECTIONS.LEFT,
            DIRECTIONS.RIGHT,DIRECTIONS.RIGHT, DIRECTIONS.RIGHT,
            DIRECTIONS.UP,DIRECTIONS.UP, DIRECTIONS.UP,
            DIRECTIONS.DOWN,DIRECTIONS.DOWN, DIRECTIONS.DOWN };
        randomSet.Add(lastDir);
        if (!validator.valid(currentCoords.x - 1, currentCoords.y) || map[currentCoords.y][currentCoords.x - 1].isGroundTile) randomSet.RemoveAll(d => d == DIRECTIONS.LEFT);
        if (!validator.valid(currentCoords.x + 1, currentCoords.y) || map[currentCoords.y][currentCoords.x + 1].isGroundTile) randomSet.RemoveAll(d => d == DIRECTIONS.RIGHT);
        if (!validator.valid(currentCoords.x, currentCoords.y - 1) || map[currentCoords.y - 1][currentCoords.x].isGroundTile) randomSet.RemoveAll(d => d == DIRECTIONS.DOWN);
        if (!validator.valid(currentCoords.x, currentCoords.y + 1) || map[currentCoords.y + 1][currentCoords.x].isGroundTile) randomSet.RemoveAll(d => d == DIRECTIONS.UP);
        return randomSet;
    }

    private static Tile[][] generateEmpty(int width,int height)
    {
        List<Tile[]> map = new List<Tile[]>();
        for (int y = 0; y < height; y++)
        {
            List<Tile> row = new List<Tile>();
            for (int x = 0; x < width; x++)
            {
                row.Add(tileFromCoords(new Vector2Int(x,y),false));
            }
            map.Add(row.ToArray());
        }
        return map.ToArray();
    }
}
