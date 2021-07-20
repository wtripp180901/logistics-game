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
        Assets.openTransportButtonPrefab = _openTransportButtonPrefab;
        Assets.openVehicleButtonPrefab = _openVehicleButtonPrefab;
        Assets.canvas = _canvas;
        Assets.vehicleBarPrefab = _vehicleBarPrefab;
        Assets.transportBarPrefab = _transportBarPrefab;
    }

    public GameObject _allsidesPrefab;
    public GameObject _railOnesidePrefab;
    public GameObject _railyardPrefab;
    public GameObject _trainPrefab;
    public GameObject _openTransportButtonPrefab;
    public GameObject _openVehicleButtonPrefab;
    public GameObject _vehicleBarPrefab;
    public Transform _canvas;
    public GameObject _transportBarPrefab;
}
