using Assets.Scripts.GenericRPG.Global;
using GenericRpg.Business.Model.Living;
using GenericRpg.Business.Model.Things;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class StartingSceneGenration : MonoBehaviour
{
    public enum TribesToGenerate
    {
        BattleRoyale = 0,
        TwoTribes = 2,
        ThreeTribes = 3,
        FourTribes = 4,
        FiveTribes = 5,
        SixTribes = 6,
        SevenTribes = 7,
        EightTribes = 8,
        NineTribes = 9,
        TenTribes = 10
    }
    public Color Tribe1Color;
    public Color Tribe2Color;
    public Color Tribe3Color;
    public Color Tribe4Color;
    public Color Tribe5Color;
    public Color Tribe6Color;
    public Color Tribe7Color;
    public Color Tribe8Color;
    public Color Tribe9Color;
    public Color Tribe10Color;

    public GameObject WhatToPlace;
    public int MinimumAmount;
    public int MaximumAmount;
    public int PositionXStart;
    public int PositionXEnd;
    public int PositionYStart;
    public int PositionYEnd;
    public TribesToGenerate HowManyTribes;
    // Use this for initialization
    void Start()
    {
        WorldObjectsReferenceHelper worldObjectsReferenceHelper = WorldObjectsReferenceHelper.Current();
        GenerateTribes();
        int numofTribes = (int)HowManyTribes;
        int humanIteration = 1;

        foreach (Vector3 position in GeneratePositions())
        {
            GameObject theObj = Instantiate(WhatToPlace, position, Quaternion.identity);
            Human humanScript = theObj.GetComponent<Human>();
            if (humanScript != null)
            {
                worldObjectsReferenceHelper.Humans.Add(theObj);

                if (HowManyTribes == 0)
                {
                    humanScript.Tribe = null;
                }
                else
                {
                    humanScript.Tribe = worldObjectsReferenceHelper.Tribes.Find(i => i.Id == humanIteration);
                    humanIteration++;
                    if (humanIteration == (numofTribes + 1))
                    {
                        humanIteration = 1;
                    }
                }
            }
        }
        AddExistingMinerals();
    }

    private void GenerateTribes()
    {
        var Tribes = WorldObjectsReferenceHelper.Current().Tribes;
        Tribes.Add(new Tribe(1, Tribe1Color));
        Tribes.Add(new Tribe(2, Tribe2Color));
        Tribes.Add(new Tribe(3, Tribe3Color));
        Tribes.Add(new Tribe(4, Tribe4Color));
        Tribes.Add(new Tribe(5, Tribe5Color));
        Tribes.Add(new Tribe(6, Tribe6Color));
        Tribes.Add(new Tribe(7, Tribe7Color));
        Tribes.Add(new Tribe(8, Tribe8Color));
        Tribes.Add(new Tribe(9, Tribe9Color));
        Tribes.Add(new Tribe(10, Tribe10Color));
    }

    private void AddExistingMinerals()
    {
        var objects = GameObject.FindObjectsOfType<Mineral>();
        foreach (var element in objects)
        {
            if (element != null)
            {
                WorldObjectsReferenceHelper.Current().Minerals.Add(element.gameObject);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }


    Vector3[] GeneratePositions()
    {
        int num = UnityEngine.Random.Range(MinimumAmount, MaximumAmount);
        Vector3[] array = new Vector3[num];
        for (int i = 0; i < num; i++)
        {
            array[i] = new Vector3(UnityEngine.Random.Range(PositionXStart, PositionXEnd), UnityEngine.Random.Range(PositionYStart, PositionYEnd), 0);
        }

        return array;
    }
}
