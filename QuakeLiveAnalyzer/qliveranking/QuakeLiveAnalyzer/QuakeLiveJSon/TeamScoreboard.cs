// JSON C# Class Generator
// http://at-my-window.blogspot.com/?page=json-class-generator

using System;
using Newtonsoft.Json.Linq;
using JsonCSharpClassGenerator;

namespace QuakeLiveAnalyzer.QuakeLiveJSon
{
    internal class TeamScoreboard
    {
        public TeamScoreboard(JObject obj)
        {
           this.MIN = JsonClassHelper.ReadFloat(JsonClassHelper.GetJToken<JValue>(obj, "MIN"));
           this.KILLS = JsonClassHelper.ReadInteger(JsonClassHelper.GetJToken<JValue>(obj, "KILLS"));
           this.BFGA = JsonClassHelper.ReadInteger(JsonClassHelper.GetJToken<JValue>(obj, "BFG_A"));
           this.CGA = JsonClassHelper.ReadInteger(JsonClassHelper.GetJToken<JValue>(obj, "CG_A"));
           this.LG = JsonClassHelper.ReadInteger(JsonClassHelper.GetJToken<JValue>(obj, "LG"));
           this.LGA = JsonClassHelper.ReadInteger(JsonClassHelper.GetJToken<JValue>(obj, "LG_A"));
           this.NG = JsonClassHelper.ReadInteger(JsonClassHelper.GetJToken<JValue>(obj, "NG"));
           this.RG = JsonClassHelper.ReadInteger(JsonClassHelper.GetJToken<JValue>(obj, "RG"));
           this.PG = JsonClassHelper.ReadInteger(JsonClassHelper.GetJToken<JValue>(obj, "PG"));
           this.TEAM = JsonClassHelper.ReadString(JsonClassHelper.GetJToken<JValue>(obj, "TEAM"));
           this.RL = JsonClassHelper.ReadInteger(JsonClassHelper.GetJToken<JValue>(obj, "RL"));
           this.PMA = JsonClassHelper.ReadInteger(JsonClassHelper.GetJToken<JValue>(obj, "PM_A"));
           this.PM = JsonClassHelper.ReadInteger(JsonClassHelper.GetJToken<JValue>(obj, "PM"));
           this.GT = JsonClassHelper.ReadInteger(JsonClassHelper.GetJToken<JValue>(obj, "GT"));
           this.BFG = JsonClassHelper.ReadInteger(JsonClassHelper.GetJToken<JValue>(obj, "BFG"));
           this.DEATHS = JsonClassHelper.ReadInteger(JsonClassHelper.GetJToken<JValue>(obj, "DEATHS"));
           this.CG = JsonClassHelper.ReadInteger(JsonClassHelper.GetJToken<JValue>(obj, "CG"));
           this.NGA = JsonClassHelper.ReadInteger(JsonClassHelper.GetJToken<JValue>(obj, "NG_A"));
           this.GTA = JsonClassHelper.ReadInteger(JsonClassHelper.GetJToken<JValue>(obj, "GT_A"));
           this.RLA = JsonClassHelper.ReadInteger(JsonClassHelper.GetJToken<JValue>(obj, "RL_A"));
           this.GL = JsonClassHelper.ReadInteger(JsonClassHelper.GetJToken<JValue>(obj, "GL"));
           this.RGA = JsonClassHelper.ReadInteger(JsonClassHelper.GetJToken<JValue>(obj, "RG_A"));
           this.MG = JsonClassHelper.ReadInteger(JsonClassHelper.GetJToken<JValue>(obj, "MG"));
           this.IMPRESSIVE = JsonClassHelper.ReadInteger(JsonClassHelper.GetJToken<JValue>(obj, "IMPRESSIVE"));
           this.HUMILIATION = JsonClassHelper.ReadInteger(JsonClassHelper.GetJToken<JValue>(obj, "HUMILIATION"));
           this.SGA = JsonClassHelper.ReadInteger(JsonClassHelper.GetJToken<JValue>(obj, "SG_A"));
           this.PGA = JsonClassHelper.ReadInteger(JsonClassHelper.GetJToken<JValue>(obj, "PG_A"));
           this.ROUNDSWON = JsonClassHelper.ReadString(JsonClassHelper.GetJToken<JValue>(obj, "ROUNDS_WON"));
           this.EXCELLENT = JsonClassHelper.ReadInteger(JsonClassHelper.GetJToken<JValue>(obj, "EXCELLENT"));
           this.MGA = JsonClassHelper.ReadInteger(JsonClassHelper.GetJToken<JValue>(obj, "MG_A"));
           this.GLA = JsonClassHelper.ReadInteger(JsonClassHelper.GetJToken<JValue>(obj, "GL_A"));
           this.ACCURACY = JsonClassHelper.ReadInteger(JsonClassHelper.GetJToken<JValue>(obj, "ACCURACY"));
           this.SG = JsonClassHelper.ReadInteger(JsonClassHelper.GetJToken<JValue>(obj, "SG"));
        }

        public readonly double MIN;
        public readonly int KILLS;
        public readonly int BFGA;
        public readonly int CGA;
        public readonly int LG;
        public readonly int LGA;
        public readonly int NG;
        public readonly int RG;
        public readonly int PG;
        public readonly string TEAM;
        public readonly int RL;
        public readonly int PMA;
        public readonly int PM;
        public readonly int GT;
        public readonly int BFG;
        public readonly int DEATHS;
        public readonly int CG;
        public readonly int NGA;
        public readonly int GTA;
        public readonly int RLA;
        public readonly int GL;
        public readonly int RGA;
        public readonly int MG;
        public readonly int IMPRESSIVE;
        public readonly int HUMILIATION;
        public readonly int SGA;
        public readonly int PGA;
        public readonly string ROUNDSWON;
        public readonly int EXCELLENT;
        public readonly int MGA;
        public readonly int GLA;
        public readonly int ACCURACY;
        public readonly int SG;
    }
}
