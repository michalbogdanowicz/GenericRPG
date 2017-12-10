using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartingSceneGenration : MonoBehaviour
{


    public GameObject WhatToPlace;
    public int MinimumAmount;
    public int MaximumAmount;
    public int PositionXStart;
    public int PositionXEnd;
    public int PositionYStart;
    public int PositionYEnd;
    // Use this for initialization
    void Start()
    {
       foreach ( Vector3 elemet in  GetPositions())
        { 
                Instantiate(WhatToPlace, elemet, Quaternion.identity);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }


    Vector3 [] GetPositions()
    {
        int num = Random.Range(MinimumAmount, MaximumAmount);
    Vector3[] array  = new Vector3 [num];
        for (int i = 0; i < num; i++)
        {
            array[i] = new  Vector3(Random.Range(PositionXStart,PositionXEnd), Random.Range(PositionYStart, PositionYEnd), 0);
        }
      
        return array;
    }
}
