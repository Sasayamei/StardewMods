﻿using System;
using System.Collections.Generic;

namespace Entoarox.Framework.ContentManager
{
    public class XnbContentInjector : ContentInjector
    {
        private static Dictionary<string, string> Mapping = new Dictionary<string, string>();
        public static void Register(string assetName, string filePath)
        {
            Mapping.Add(GetPlatformSafePath(assetName), GetModsRelativePath(GetPlatformSafePath(filePath)));
        }
        public override bool CanLoad<T>(string assetName)
        {
            return Mapping.ContainsKey(assetName);
        }
        public override T Load<T>(string assetName, Func<string, T> loadBase)
        {
            return ModManager.Load<T>(Mapping[GetPlatformSafePath(assetName)]);
        }
        public override void Inject<T>(string assetName, ref T asset)
        {
            throw new NotImplementedException();
        }
    }
}