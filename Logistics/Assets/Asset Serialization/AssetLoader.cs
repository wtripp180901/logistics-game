using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssetLoader : MonoBehaviour {

    private void Awake()
    {
        /*Assets.allsidesPrefab = _allsidesPrefab;
        Assets.
        Assets.railOnesidePrefab = _railOnesidePrefab;
        Assets.railyardPrefab = _railyardPrefab;
        Assets.trainPrefab = _trainPrefab;
        Assets.openTransportButtonPrefab = _openTransportButtonPrefab;
        Assets.openVehicleButtonPrefab = _openVehicleButtonPrefab;
        Assets.canvas = _canvas;
        Assets.vehicleBarPrefab = _vehicleBarPrefab;
        Assets.transportBarPrefab = _transportBarPrefab;
        Assets.confirmButtonPrefab = _confirmButtonPrefab;
        Assets.cancelButtonPrefab = _cancelButtonPrefab;
        Assets.stopNumberPrefab = _stopNumberPrefab;
        Assets.worldCanvas = _worldCanvas;
        Assets.factoryUIPrefab = _factoryUIPrefab;
        Destroy(this);*/
        tilePrefabWidth = allsidesPrefab.GetComponent<SpriteRenderer>().bounds.extents.x * 2;
        Assets.assets = this;

    }

    public float tilePrefabWidth;
    public GameObject allsidesPrefab;
    public GameObject railOnesidePrefab;
    public GameObject railyardPrefab;
    public GameObject trainPrefab;
    public GameObject openTransportButtonPrefab;
    public GameObject openVehicleButtonPrefab;
    public GameObject vehicleBarPrefab;
    public Transform canvas;
    public GameObject transportBarPrefab;
    public GameObject confirmButtonPrefab;
    public GameObject cancelButtonPrefab;
    public GameObject stopNumberPrefab;
    public Transform worldCanvas;
    public GameObject factoryUIPrefab;
    public GameObject goalBarPrefab;
}
