﻿using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework.Graphics;

using StardewValley;

namespace Entoarox.XnbLoader
{
    public class XnbLoaderMod : StardewModdingAPI.Mod
    {
        public override void Entry(StardewModdingAPI.IModHelper helper)
        {
            if (Framework.EntoFramework.Version < new Version(1, 6, 6))
                throw new DllNotFoundException("A newer version of EntoaroxFramework.dll is required as the currently installed one is to old for XnbLoader to use.");
            Framework.EntoFramework.VersionRequired("XnbLoader", new Version(1, 6, 6));
            StardewModdingAPI.Events.GameEvents.FirstUpdateTick += GameEvents_FirstUpdateTick;
            Framework.VersionChecker.AddCheck("XnbLoader", new System.Version(1, 0, 6), "https://raw.githubusercontent.com/Entoarox/StardewMods/master/VersionChecker/XnbLoader.json");
        }
        private string _Path;
        private int Files = 0;
        private void GameEvents_FirstUpdateTick(object s, object e)
        {
            StardewModdingAPI.Events.GameEvents.UpdateTick += GameEvents_UpdateTick;
        }
        private void GameEvents_UpdateTick(object s, object e)
        {
            StardewModdingAPI.Events.GameEvents.UpdateTick -= GameEvents_UpdateTick;
            _Path = System.IO.Path.Combine(Helper.DirectoryPath, "ModContent","");
            System.IO.Directory.CreateDirectory(_Path);
            Monitor.Log("Parsing `ModContent` for files to redirect the content manager to...", StardewModdingAPI.LogLevel.Info);
            ParseDir(_Path);
            Monitor.Log("Reloading static content references...", StardewModdingAPI.LogLevel.Trace);
            Framework.EntoFramework.GetContentRegistry().ReloadStaticReferences();
            Monitor.Log($"Parsing complete, found and redirected [{Files}] files", StardewModdingAPI.LogLevel.Info);
        }
        private void ParseDir(string path)
        {
            Monitor.Log("Scanning for files and directories in " + path.Replace(_Path + System.IO.Path.DirectorySeparatorChar, System.IO.Path.DirectorySeparatorChar.ToString()).Replace(_Path, System.IO.Path.DirectorySeparatorChar.ToString()), StardewModdingAPI.LogLevel.Trace);
            foreach (string dir in System.IO.Directory.EnumerateDirectories(path))
                ParseDir(System.IO.Path.Combine(path, dir));
            foreach (string file in System.IO.Directory.EnumerateFiles(path))
            {
                string filePath = System.IO.Path.Combine(path, System.IO.Path.GetDirectoryName(file), System.IO.Path.GetFileNameWithoutExtension(file));
                string from = filePath.Replace(_Path+System.IO.Path.DirectorySeparatorChar, "");
                Monitor.Log($"Redirecting: {from} ~> {filePath}.xnb", StardewModdingAPI.LogLevel.Trace);
                Files++;
                Framework.EntoFramework.GetContentRegistry().RegisterXnb(from, filePath);
            }
        }
    }
}