using System;
using System.Collections.Generic;
using static SuitColorSync;

[Serializable]
public struct PlayerData
{
    public string id;
    public string name;
    public ColorEnum suitColor;
    public List<int> myFaces;
}