using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class exportAssetBundles  {

    [MenuItem("Assets/Build AssetBundle")]
    static void ExportResource()
    {

        Object[] selection = Selection.GetFiltered(typeof(Object), SelectionMode.DeepAssets);
        string filename = (Selection.activeObject.name.Replace("+", ""));
        string path = EditorUtility.SaveFilePanel("Save Resource", "", filename, "unity3d");
        //string path = "E:/YusaBaddal/AssetCreator" + "/AssetBundles/iOS/" + (Selection.activeObject.name.Replace("+",""))+".unity3d";
        if (path.Length != 0)
        {
            BuildPipeline.BuildAssetBundle(Selection.activeObject, selection, path, BuildAssetBundleOptions.CollectDependencies | BuildAssetBundleOptions.CompleteAssets, BuildTarget.StandaloneWindows64);
            //Selection.objects = selection;
        }
    }


    //[MenuItem("Assets/Build AssetBundle")]
    //static void ExportResource()
    //{
    //    string path = EditorUtility.SaveFilePanel("Save Resource", "", "New Resource", "unity3d");

    //    if (path.Length != 0)
    //    {
    //        Object[] selection = Selection.GetFiltered(typeof(Object), SelectionMode.DeepAssets);

    //        BuildPipeline.BuildAssetBundle(Selection.activeObject, selection, path, BuildAssetBundleOptions.CollectDependencies | BuildAssetBundleOptions.CompleteAssets, BuildTarget.Android);

    //        Selection.objects = selection;

    //    }
    //}


}

