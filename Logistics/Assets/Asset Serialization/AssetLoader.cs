﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssetLoader : MonoBehaviour {

    private void Awake()
    {
        Assets.allsidesPrefab = _allsidesPrefab;
        Assets.tilePrefabWidth = _allsidesPrefab.GetComponent<SpriteRenderer>().bounds.extents.x * 2;
        Assets.railOnesidePrefab = _railOnesidePrefab;
        Assets.railyardPrefab = _railyardPrefab;
        Assets.trainPrefab = _trainPrefab;
    }

    public GameObject _allsidesPrefab;
    public GameObject _railOnesidePrefab;
    public GameObject _railyardPrefab;
    public GameObject _trainPrefab;
}