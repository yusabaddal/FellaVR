using System.Collections.Generic;

[System.Serializable]
public class PodiumModel
{
    public List<podiumGender> genderList;
}

[System.Serializable]
public class podiumGender
{
    public int id;
    public string name;
    public List<podiumDress> dressList;

}
[System.Serializable]
public class podiumDress
{
    public int id;
    public string name;
    public List<podiumType> typeList;

}
[System.Serializable]
public class podiumType
{
    public int id;
    public string name;
    public List<podiumSubType> subtypeList;

}
[System.Serializable]
public class podiumSubType
{
    public int id;
    public string name;
}