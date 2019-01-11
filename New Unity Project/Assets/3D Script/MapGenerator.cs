using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{

    public Transform tilePrefab;
    public Transform obstaclePrefab;
    public Vector2 mapSize;

    [Range(0, 1)]
    public float obstaclePercent;
    [Range(0, 1)]
    public float outlinePercent;

    List<Coord> allTileCoords;
    Queue<Coord> shuffledTileCoords;

    public int seed = 10;

    void Start()
    {
        GenerateMap();
    }

    public void GenerateMap()
    {

        //장애물 생성을 위해 모든 타일 좌표 저장
        allTileCoords = new List<Coord>();
        for (int x = 0; x < mapSize.x; x++)
        {
            for (int y = 0; y < mapSize.y; y++)
            {
                allTileCoords.Add(new Coord(x, y));
            }
        }
        shuffledTileCoords = new Queue<Coord>(Utility.ShuffleArray(allTileCoords.ToArray(), seed));


        //Map item Grouping
        string holderName = "Generated Map";
        string obstacleName = "Generated Obstacle";
        if (transform.Find(holderName) || transform.Find(obstacleName))
        {
            DestroyImmediate(transform.Find(holderName).gameObject); //이미 있으면 삭제하고 생성
            DestroyImmediate(transform.Find(obstacleName).gameObject);
            Debug.Log("original Map Destroyed!!");
        }

        Transform mapholder = new GameObject(holderName).transform;
        Transform obstacleholder = new GameObject(obstacleName).transform;
        mapholder.parent = transform;
        obstacleholder.parent = transform;

        //map 생성
        for (int x = 0; x < mapSize.x; x++)
        {
            for (int y = 0; y < mapSize.y; y++)
            {
                Vector3 tilePosition = CoordToPosition(x, y);
                Transform newTile = Instantiate(tilePrefab, tilePosition, Quaternion.Euler(new Vector3(0,0,0))) as Transform;
                newTile.localScale = Vector3.one * (1 - outlinePercent);
                newTile.parent = mapholder;
            }
        }

        bool[,] obstacleMap = new bool[(int)mapSize.x, (int)mapSize.y];

        int obstacleCount = (int) (mapSize.x * mapSize.y * obstaclePercent);
        for (int i = 0; i < obstacleCount; i++)
        {
            Coord randomCoord = GetRandomCoord();
            Vector3 obstaclePosition = CoordToPosition(randomCoord.x, randomCoord.y);
            Transform newObstacle = Instantiate(obstaclePrefab, obstaclePosition + Vector3.up * .8f, Quaternion.identity) as Transform;
            newObstacle.parent = obstacleholder;
        }

    }

    Vector3 CoordToPosition(int x, int y)
    {
        return new Vector3(-mapSize.x / 2 + x, 0, -mapSize.y / 2 + y); //grid 안에 타일이 있게하고싶으면 0.5f를 더할것
    }

    public Coord GetRandomCoord()
    {
        Coord randomCoord = shuffledTileCoords.Dequeue();
        shuffledTileCoords.Enqueue(randomCoord);
        return randomCoord;
    }

    public struct Coord
    {
        public int x;
        public int y;

        public Coord(int _x, int _y)
        {
            x = _x;
            y = _y;
        }
    }
}
