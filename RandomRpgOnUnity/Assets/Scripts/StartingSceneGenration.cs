using Assets.Scripts.GenericRPG.Global;
using GenericRpg.Business.Model.Living;
using GenericRpg.Business.Model.Things;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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


    public GameObject WhatToPlace;
    public int MinimumAmount;
    public int MaximumAmount;
    public int PositionXStart;
    public int PositionXEnd;
    public int PositionYStart;
    public int PositionYEnd;
    public TribesToGenerate HowManyTrybes;
    // Use this for initialization
    void Start()
    {
        int numofTribes = (int)HowManyTrybes;
        int humanIteration = 0;
        WorldObjectsReferenceHelper worldObjectsReferenceHelper = WorldObjectsReferenceHelper.Current();
        foreach (Vector3 position in GeneratePositions())
        {
            MonoBehaviour toPlaceScript = WhatToPlace.GetComponent<MonoBehaviour>();
            Human humanScript = toPlaceScript as Human;
            if (humanScript != null)
            {
                if (HowManyTrybes == TribesToGenerate.BattleRoyale)
                {
                    humanScript.Tribe = Tribe.NoTribe;
                }
                else
                {
                    if (humanIteration == numofTribes)
                    {
                        humanIteration = 0;
                    }

                    switch (humanIteration)
                    {
                        case 0: humanScript.Tribe = Tribe.Tribe1; break;
                        case 1: humanScript.Tribe = Tribe.Tribe2; break;
                        case 2: humanScript.Tribe = Tribe.Tribe3; break;
                        case 3: humanScript.Tribe = Tribe.Tribe4; break;
                        case 4: humanScript.Tribe = Tribe.Tribe5; break;
                        case 5: humanScript.Tribe = Tribe.Tribe6; break;
                        case 6: humanScript.Tribe = Tribe.Tribe7; break;
                        case 7: humanScript.Tribe = Tribe.Tribe8; break;
                        case 8: humanScript.Tribe = Tribe.Tribe9; break;
                        case 9: humanScript.Tribe = Tribe.Tribe10; break;
                        default: throw new System.Exception("Somethhing went wrong");
                    }
                    humanIteration++;
                }


            }

            GameObject theObj = Instantiate(WhatToPlace, position, Quaternion.identity);
            if (theObj.GetComponent<Human>() != null)
            {
                worldObjectsReferenceHelper.Humans.Add(theObj);
            }



        }

        AddExistingMinerals();
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
