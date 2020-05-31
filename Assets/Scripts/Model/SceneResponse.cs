
using System.Collections.Generic;

[System.Serializable]
public partial class Response
{
    public int tree_id { get; set; }
    public string tree_name { get; set; }
    public int tree_location { get; set; }
    public List<TreeProduct> tree_product { get; set; }
}

[System.Serializable]
public class ColorVaryant
{
    public int varyant_id { get; set; }
    public string varyant_name { get; set; }
    public string varyant_object { get; set; }
    public string varyant_model_object { get; set; }
    public int varyant_version { get; set; }
    public int varyant_gender_id { get; set; }
    public string varyant_gender_name { get; set; }
    public int varyant_dress_id { get; set; }
    public string varyant_dress_name { get; set; }
    public int varyant_type_id { get; set; }
    public string varyant_type_name { get; set; }
    public int varyant_subtype_id { get; set; }
    public string varyant_subtype_name { get; set; }
}

[System.Serializable]
public class ProductColor
{
    public int color_id { get; set; }
    public string color_name { get; set; }
    public string color_code { get; set; }
    public List<ColorVaryant> color_varyants { get; set; }
}

[System.Serializable]
public class TreeProduct
{
    public int product_id { get; set; }
    public string product_name { get; set; }
    public List<ProductColor> product_colors { get; set; }
}
