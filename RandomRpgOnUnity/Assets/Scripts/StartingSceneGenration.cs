using Assets.Scripts.GenericRPG.Global;
using GenericRpg.Business.Model.Living;
using GenericRpg.Business.Model.Things;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Assets.Scripts.GenericRPG.Model.Things.Buildings;

public enum GenerationMode
{
    RandomPlacing,
    TeamBasedPlacing,
    TeamBasedWithOnlyBuilding
}

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

    // Colors
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
    public GameObject BaseBuliding;
    public int MinimumAmount;
    public int MaximumAmount;
    public int PositionXStart;
    public int PositionXEnd;
    public int PositionYStart;
    public int PositionYEnd;
    public TribesToGenerate HowManyTribes;
    public GenerationMode GenerationMode;
    private int numberOfHumans;
    private int numofTribes;
    // Use this for initialization
    public long MaxAmountOfWorkers;


    void Start()
    {
        numofTribes = (int)HowManyTribes;
        numberOfHumans = UnityEngine.Random.Range(MinimumAmount, MaximumAmount);

        GenerateTribes();

        GenerateStuff();

        AddExistingMinerals();
    }


    private void GenerateStuff()
    {
        switch (GenerationMode)
        {
            case GenerationMode.RandomPlacing: GenerateHumansRandomly(); break;
            case GenerationMode.TeamBasedPlacing:
                {
                    if (HowManyTribes == TribesToGenerate.BattleRoyale)
                    {
                        GenerateHumansRandomly();
                    }
                    else
                    {
                        GenerateHumansCloseInTribe();
                    }
                }
                break;
            case GenerationMode.TeamBasedWithOnlyBuilding:

                GenerateBuildingInCorners();

                break;
            default: throw new NotImplementedException();

        }
    }

    private void GenerateBuildingInCorners()
    {
        WorldObjectsReferenceHelper worldObjectsReferenceHelper = WorldObjectsReferenceHelper.Current();
            Tribe tribe = null;
        for (int i = 0; i < numofTribes; i++)
        {
            tribe = worldObjectsReferenceHelper.Tribes.Find(ji => ji.Id == (i % numofTribes) + 1);
            GameObject theObj = Instantiate(BaseBuliding, tribe.TeamPosition, Quaternion.identity);
            BaseBulding humanScript = theObj.GetComponent<BaseBulding>();
            if (humanScript != null)
            {
                humanScript.Tribe = tribe;
                worldObjectsReferenceHelper.Humans.Add(theObj);
            }
        }
        
    }

    private void GenerateHumansCloseInTribe()
    {

        WorldObjectsReferenceHelper worldObjectsReferenceHelper = WorldObjectsReferenceHelper.Current();
        for (int i = 0; i < numberOfHumans; i++)
        {
            Tribe tribe = null;
            tribe = worldObjectsReferenceHelper.Tribes.Find(ji => ji.Id == (i % numofTribes) + 1);

            GameObject theObj = Instantiate(WhatToPlace, tribe.TeamPosition, Quaternion.identity);
            Human humanScript = theObj.GetComponent<Human>();
            if (humanScript != null)
            {
                humanScript.Tribe = tribe;
                worldObjectsReferenceHelper.Humans.Add(theObj);
            }
        }
    }

    private void GenerateHumansRandomly()
    {
        WorldObjectsReferenceHelper worldObjectsReferenceHelper = WorldObjectsReferenceHelper.Current();
        for (int i = 0; i < numberOfHumans; i++)
        {
            GameObject theObj = Instantiate(WhatToPlace, GenerateRandomPosition(), Quaternion.identity);
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
                    humanScript.Tribe = worldObjectsReferenceHelper.Tribes.Find(ji => ji.Id == (i % numofTribes) + 1);
                }
            }
        }
    }

    private void GenerateTribes()
    {
        var Tribes = WorldObjectsReferenceHelper.Current().Tribes;
        Tribes.Add(new Tribe(1, Tribe1Color, new Vector3(PositionXStart, PositionYStart, 0), MaxAmountOfWorkers));
        Tribes.Add(new Tribe(2, Tribe2Color, new Vector3(PositionXStart, PositionYEnd, 0), MaxAmountOfWorkers));
        Tribes.Add(new Tribe(3, Tribe3Color, new Vector3(PositionXEnd, PositionYStart, 0), MaxAmountOfWorkers));
        Tribes.Add(new Tribe(4, Tribe4Color, new Vector3(PositionXEnd, PositionYEnd, 0), MaxAmountOfWorkers));
        Tribes.Add(new Tribe(5, Tribe5Color, new Vector3(PositionXStart, PositionYStart, 0), MaxAmountOfWorkers));
        Tribes.Add(new Tribe(6, Tribe6Color, new Vector3(PositionXStart, PositionYEnd, 0), MaxAmountOfWorkers));
        Tribes.Add(new Tribe(7, Tribe7Color, new Vector3(PositionXStart, PositionYStart, 0), MaxAmountOfWorkers));
        Tribes.Add(new Tribe(8, Tribe8Color, new Vector3(PositionXStart, PositionYStart, 0), MaxAmountOfWorkers));
        Tribes.Add(new Tribe(9, Tribe9Color, new Vector3(PositionXStart, PositionYStart, 0), MaxAmountOfWorkers));
        Tribes.Add(new Tribe(10, Tribe10Color, new Vector3(PositionXStart, PositionYEnd, 0), MaxAmountOfWorkers));

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


    Vector3 GenerateRandomPosition()
    {
        return new Vector3(UnityEngine.Random.Range(PositionXStart, PositionXEnd), UnityEngine.Random.Range(PositionYStart, PositionYEnd), 0);
    }
}
