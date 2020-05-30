   
    [System.Serializable]
    public class Response
    {
        public int tree_id { get; set; }
        
        public string tree_name { get; set; }
        
        public string tree_object_path { get; set; }
        
        public string tree_model_path { get; set; }
        
        public TreeProduct[] tree_products { get; set; }
    }

    [System.Serializable]
    public class TreeProduct
    {
        public int product_id { get; set; }

        public string product_name { get; set; }

        public int product_order { get; set; }

        public string product_path { get; set; }

        public int product_version { get; set; }
    }

