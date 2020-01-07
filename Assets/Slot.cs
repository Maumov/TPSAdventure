using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Slot : MonoBehaviour
{
    public bool Filled;
    public int myValue = -1;

    public Slot Top, Bottom, Left, Right, Front, Back;

    public ProbabilityToBe myProbability;

    public List<GameObject> Visuals;
    private void Start() {
        foreach(GameObject g in Visuals) {
            g.SetActive(false);
        }
    }
    public void MyValueIsSet() {
        if(myValue != -1) {
            Visuals[myValue].SetActive(true);
        }
    }

}

[System.Serializable]
public class ProbabilityToBe
{
    public List<int> value;
    public List<float> probability;
}