// JSON C# Class Generator
// http://at-my-window.blogspot.com/?page=json-class-generator

using System;
using Newtonsoft.Json.Linq;
using JsonCSharpClassGenerator;

namespace QuakeLiveAnalyzer.QuakeLiveJSon
{
    internal class PlayerStat
    {
        public PlayerStat(JObject obj)
        {
           this.PLAYERTEAM = JsonClassHelper.ReadString(JsonClassHelper.GetJToken<JValue>(obj, "PLAYER_TEAM"));
           this.PLAYERNICK = JsonClassHelper.ReadString(JsonClassHelper.GetJToken<JValue>(obj, "PLAYER_NICK"));
           this.PLAYERCOUNTRY = JsonClassHelper.ReadString(JsonClassHelper.GetJToken<JValue>(obj, "PLAYER_COUNTRY"));
           this.NUM = JsonClassHelper.ReadString(JsonClassHelper.GetJToken<JValue>(obj, "NUM").ToString());
           this.PLAYERMODEL = JsonClassHelper.ReadString(JsonClassHelper.GetJToken<JValue>(obj, "PLAYER_MODEL"));
           this.PLAYERID = JsonClassHelper.ReadInteger(JsonClassHelper.GetJToken<JValue>(obj, "PLAYER_ID"));
        }

        public readonly string PLAYERTEAM;
        public readonly string PLAYERNICK;
        public readonly string PLAYERCOUNTRY;
        public readonly string NUM;
        public readonly string PLAYERMODEL;
        public readonly int PLAYERID;
    }
}
