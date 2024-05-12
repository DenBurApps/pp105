using System;
using System.Collections.Generic;

[Serializable]
public class Root
{
    public bool onBoarding;

    public List<Properties> properties = new List<Properties>();
}

[Serializable]
public class Properties
{
    public string Name;
    public string Photo;
    public string Date;
    public string Location;

    public string Rare;
    public string Description;
    public int ID;
}
