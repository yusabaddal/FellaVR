
using System.Collections.Generic;

[System.Serializable]
public partial class Response
{
    public string scenario_name { get; set; }
    public List<ScenarioTree> scenario_trees { get; set; }
}
[System.Serializable]
public class ScenarioTree
{
    public int tree_id { get; set; }
    public string tree_name { get; set; }
    public int tree_location { get; set; }
    public List<TreeProduct> tree_products { get; set; }
}

[System.Serializable]
public class ColorVaryant
{
    public int varyant_id { get; set; }
    public string varyant_name { get; set; }
    public string varyant_stock_code { get; set; }
    public string varyant_model { get; set; }
    public string varyant_manken_model { get; set; }
    public int varyant_version { get; set; }
    public string varyant_size { get; set; }
    public int varyant_gender_id { get; set; }
    public string varyant_gender_name { get; set; }
    public string varyant_gender_icon { get; set; }
    public int varyant_dress_id { get; set; }
    public string varyant_dress_name { get; set; }
    public string varyant_dress_icon { get; set; }
    public int varyant_type_id { get; set; }
    public string varyant_type_name { get; set; }
    public string varyant_type_icon { get; set; }
    public int varyant_subtype_id { get; set; }
    public string varyant_subtype_name { get; set; }
    public string varyant_subtype_icon { get; set; }
}

[System.Serializable]
public class ProductColor
{
    public int color_id { get; set; }
    public string color_name { get; set; }
    public string color_code { get; set; }
    public List<ColorVaryant> color_varyant { get; set; }
}

[System.Serializable]
public class TreeProduct
{
    public int product_id { get; set; }
    public string product_name { get; set; }
    public List<ProductColor> product_colors { get; set; }
}
