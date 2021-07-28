using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map {

    private Tile[][] data;
    private List<Tile> playTiles = new List<Tile>();
    private CoordinateValidator validator;

    public Map(IMapDataGenerator dataGenerator)
    {
        data = dataGenerator.generateMapData();
        validator = new CoordinateValidator(data[0].Length, data.Length,0);
        buildMap();

        //Creating world space UI renderer
        float mapWidth = data[0].Length * Assets.tilePrefabWidth;
        float mapHeight = data.Length * Assets.tilePrefabWidth;
        RectTransform rect = Assets.worldCanvas.GetComponent<RectTransform>();
        rect.position = new Vector2(mapWidth / 2, mapHeight / 2);
        rect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, mapWidth);
        rect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, mapHeight);
    }

    public Tile getTile(int x,int y) { return data[y][x]; }

    //Renders map, links tiles and adds relevant tiles to playTiles
    private void buildMap()
    {
        for (int y = 0; y < data.Length; y++)
        {
            for (int x = 0; x < data[0].Length; x++)
            {
                Tile currentTile = getTile(x, y);
                if (currentTile != null)
                {
                    if (validator.valid(x - 1, y) && getTile(x - 1, y) != null) currentTile.left = getTile(x - 1, y);
                    if (validator.valid(x + 1, y) && getTile(x + 1, y) != null) currentTile.right = getTile(x + 1, y);
                    if (validator.valid(x, y - 1) && getTile(x, y - 1) != null) currentTile.down = getTile(x, y - 1);
                    if (validator.valid(x, y + 1) && getTile(x, y + 1) != null) currentTile.up = getTile(x, y + 1);
                    currentTile.render();
                    currentTile.renderFeature();
                    playTiles.Add(currentTile);
                }
            }
        }
    }

    public Tile tileWithMouseInside()
    {
        for(int i = 0;i < playTiles.Count; i++)
        {
            if (playTiles[i].pointInside(InputAdapter.position)) return playTiles[i];
        }
        return null;
    }

    /*public TransportHubFeature[] getHubs()
    {
        List<TransportHubFeature> hubs = new List<TransportHubFeature>();
        for (int i = 0;i < data.Length; i++)
        {
            for(int j = 0;j < data[0].Length; j++)
            {
                if (data[i][j] != null)
                {
                    Feature feature = data[i][j].getFeature(false);
                    if (feature != null && feature.isHub) hubs.Add((TransportHubFeature)feature);
                }
            }
        }
        return hubs.ToArray();
    }*/
}
