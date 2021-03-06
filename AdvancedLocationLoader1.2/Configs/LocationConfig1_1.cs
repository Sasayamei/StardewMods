﻿using System;
using System.Collections.Generic;

using StardewModdingAPI;

namespace Entoarox.AdvancedLocationLoader.Configs
{
    public class LocationConfig1_1
    {
        public string loaderVersion;
        public Dictionary<string, string> about;
        public List<Dictionary<string, string>> locations;
        public List<Dictionary<string, string>> overrides;
        public Dictionary<string, Dictionary<string, List<string>>> minecarts;
        public List<Dictionary<string, string>> tilesheets;
        public List<Dictionary<string, string>> tiles;
        public List<List<string>> properties;
        public List<List<string>> warps;
        public Dictionary<string, Dictionary<string, string>> conditions;
        public List<string> shops;
    }
}
