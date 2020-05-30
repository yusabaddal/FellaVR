﻿// Magica Cloth.
// Copyright (c) MagicaSoft, 2020.
// https://magicasoft.jp
using System.Collections.Generic;
using UnityEngine;

namespace MagicaCloth
{
    /// <summary>
    /// ボーンクロスのターゲットトランスフォーム
    /// </summary>
    [System.Serializable]
    public class BoneClothTarget : IDataHash, IBoneReplace
    {
        //　ルートトランスフォーム
        [SerializeField]
        private List<Transform> rootList = new List<Transform>();

        //=========================================================================================
        /// <summary>
        /// ルートの親トランスフォームの登録インデックス
        /// </summary>
        private int[] parentIndexList = null;

        //=========================================================================================
        /// <summary>
        /// データを識別するハッシュコードを作成して返す
        /// </summary>
        /// <returns></returns>
        public int GetDataHash()
        {
            int hash = 0;
            hash += rootList.GetDataHash();
            return hash;
        }

        //=========================================================================================
        /// <summary>
        /// ルートトランスフォームの数
        /// </summary>
        public int RootCount
        {
            get
            {
                return rootList.Count;
            }
        }

        /// <summary>
        /// ルートトランスフォーム取得
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public Transform GetRoot(int index)
        {
            if (index < rootList.Count)
                return rootList[index];

            return null;
        }

        /// <summary>
        /// ルートトランスフォームのインデックスを返す。無い場合は(-1)
        /// </summary>
        /// <param name="root"></param>
        /// <returns></returns>
        public int GetRootIndex(Transform root)
        {
            return rootList.IndexOf(root);
        }

        /// <summary>
        /// ボーン置換
        /// </summary>
        /// <param name="boneReplaceDict"></param>
        public void ReplaceBone(Dictionary<Transform, Transform> boneReplaceDict)
        {
            for (int i = 0; i < rootList.Count; i++)
            {
                rootList[i] = MeshUtility.GetReplaceBone(rootList[i], boneReplaceDict);
            }
        }

        /// <summary>
        /// ルートの親トランスフォームをすべて登録する
        /// </summary>
        public void AddParentTransform()
        {
            if (rootList.Count > 0)
            {
                HashSet<Transform> parentSet = new HashSet<Transform>();
                foreach (var t in rootList)
                {
                    if (t && t.parent)
                        parentSet.Add(t.parent);
                }

                parentIndexList = new int[parentSet.Count];

                int i = 0;
                foreach (var parent in parentSet)
                {
                    int index = -1;
                    if (parent)
                    {
                        index = MagicaPhysicsManager.Instance.Bone.AddBone(parent);
                    }
                    parentIndexList[i] = index;
                    i++;
                }
            }
        }

        /// <summary>
        /// ルートの親トランスフォームをすべて解除する
        /// </summary>
        public void RemoveParentTransform()
        {
            if (MagicaPhysicsManager.IsInstance())
            {
                if (parentIndexList != null && parentIndexList.Length > 0)
                {
                    for (int i = 0; i < parentIndexList.Length; i++)
                    {
                        var index = parentIndexList[i];
                        if (index >= 0)
                        {
                            MagicaPhysicsManager.Instance.Bone.RemoveBone(index);
                        }
                    }
                }
            }

            parentIndexList = null;
        }

        /// <summary>
        /// ルートの親トランスフォームの未来予測をリセットする
        /// </summary>
        public void ResetFuturePredictionParentTransform()
        {
            if (parentIndexList != null && parentIndexList.Length > 0)
            {
                for (int i = 0; i < parentIndexList.Length; i++)
                {
                    var index = parentIndexList[i];
                    if (index >= 0)
                    {
                        MagicaPhysicsManager.Instance.Bone.ResetFuturePrediction(index);
                    }
                }
            }
        }
    }
}
