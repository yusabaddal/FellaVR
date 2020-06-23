using System.Collections.Generic;

[System.Serializable]
public class PodiumModel
{
    public List<podiumGender> genderList;
}

[System.Serializable]
public class podiumGender
{
    public int gender_id { get; set; }
    public string gender_name { get; set; }
    public string gender_icon { get; set; }
    public List<GenderDress> gender_dress { get; set; }

}
[System.Serializable]
public class TypeSubtype
{
    public int subtype_id { get; set; }
    public string subtype_name { get; set; }
    public string subtype_icon { get; set; }
}

[System.Serializable]
public class DressType
{
    public int type_id { get; set; }
    public string type_name { get; set; }
    public string type_icon { get; set; }
    public List<TypeSubtype> type_subtypes { get; set; }
}

[System.Serializable]
public class GenderDress
{
    public int dress_id { get; set; }
    public string dress_name { get; set; }
    public string dress_icon { get; set; }
    public List<DressType> dress_types { get; set; }
}