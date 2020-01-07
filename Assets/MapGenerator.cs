using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public int viewSize;
    [Header ("Sample")]
    public List<Part> PartsLibrary;

    [Header("Generation")]
    public int sizeX;
    public int sizeY;
    public int sizeZ;
    public GameObject SlotPrefab;
    public List<Part> AllSlotsCreated;
    public int randomSeed;
    public void Start() {
        AllSlotsCreated = new List<Part>();
        StartCoroutine(GenerateSlots());
       
    }

    IEnumerator GenerateSlots() {
        for(int i = 0; i < sizeX; i++) {
            for(int j = 0; j < sizeY; j++) {
                for(int k = 0; k < sizeZ; k++) {
                    Part p = new Part();
                    AllSlotsCreated.Add(p);
                    yield return null;
                }
            }
        }
        StartCoroutine(TheStart());
    }

    IEnumerator TheStart() {
        Random.InitState(randomSeed);
        
        yield return null;


    }
}

[System.Serializable]
public class Part
{
    public string Name;
    public int partNumber;
    //What can be connected with me;
    public int connectionLeft;
    public int connectionRight;
    public int connectionFront;
    public int connectionBack;

    float entropy;

    public void CalculateEntropy() {
        
    }
}


