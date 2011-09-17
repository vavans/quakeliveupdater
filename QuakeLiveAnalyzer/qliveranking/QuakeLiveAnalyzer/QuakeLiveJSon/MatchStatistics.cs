// JSON C# Class Generator
// http://at-my-window.blogspot.com/?page=json-class-generator

using System;
using Newtonsoft.Json.Linq;
using JsonCSharpClassGenerator;

namespace QuakeLiveAnalyzer.QuakeLiveJSon
{
	internal class MatchStatistics
    {
        public MatchStatistics(string json)
         : this(JObject.Parse(json))
        { }

		public MatchStatistics(JObject obj)
        {
           this.PUBLICID = JsonClassHelper.ReadString(JsonClassHelper.GetJToken<JValue>(obj, "PUBLIC_ID"));
           this.RESTARTED = JsonClassHelper.ReadString(JsonClassHelper.GetJToken<JValue>(obj, "RESTARTED"));
           this.AVGACC = JsonClassHelper.ReadInteger(JsonClassHelper.GetJToken<JValue>(obj, "AVG_ACC"));
           this.TIER = JsonClassHelper.ReadString(JsonClassHelper.GetJToken<JValue>(obj, "TIER"));
           this.RANKED = JsonClassHelper.ReadString(JsonClassHelper.GetJToken<JValue>(obj, "RANKED"));
           this.EXITMSG = JsonClassHelper.ReadString(JsonClassHelper.GetJToken<JValue>(obj, "EXIT_MSG"));
           this.GAMETYPEFULL = JsonClassHelper.ReadString(JsonClassHelper.GetJToken<JValue>(obj, "GAME_TYPE_FULL"));
           this.PLAYERID = JsonClassHelper.ReadString(JsonClassHelper.GetJToken<JValue>(obj, "PLAYER_ID"));
           this.GAMETIMESTAMP = JsonClassHelper.ReadString(JsonClassHelper.GetJToken<JValue>(obj, "GAME_TIMESTAMP"));
           this.TSCORE1 = JsonClassHelper.ReadString(JsonClassHelper.GetJToken<JValue>(obj, "TSCORE1"));
           this.TSCORE0 = JsonClassHelper.ReadString(JsonClassHelper.GetJToken<JValue>(obj, "TSCORE0"));
           this.TRAINING = JsonClassHelper.ReadString(JsonClassHelper.GetJToken<JValue>(obj, "TRAINING"));
           this.MAPNAMESHORT = JsonClassHelper.ReadString(JsonClassHelper.GetJToken<JValue>(obj, "MAP_NAME_SHORT"));
		   this.MOSTDEATHS = JsonClassHelper.ReadStronglyTypedObject<PlayerStat>(JsonClassHelper.GetJToken<JObject>(obj, "MOST_DEATHS"));
           this.GAMETYPE = JsonClassHelper.ReadString(JsonClassHelper.GetJToken<JValue>(obj, "GAME_TYPE"));
		   this.DMGTAKEN = JsonClassHelper.ReadStronglyTypedObject<PlayerStat>(JsonClassHelper.GetJToken<JObject>(obj, "DMG_TAKEN"));
		   this.MOSTACCURATE = JsonClassHelper.ReadStronglyTypedObject<PlayerStat>(JsonClassHelper.GetJToken<JObject>(obj, "MOST_ACCURATE"));
           this.FRAGLIMIT = JsonClassHelper.ReadString(JsonClassHelper.GetJToken<JValue>(obj, "FRAG_LIMIT"));
           this.LASTTEAMSCORER = JsonClassHelper.ReadString(JsonClassHelper.GetJToken<JValue>(obj, "LAST_TEAMSCORER"));
           this.NUMPLAYERS = JsonClassHelper.ReadString(JsonClassHelper.GetJToken<JValue>(obj, "NUM_PLAYERS"));
           this.GAMETYPEID = JsonClassHelper.ReadString(JsonClassHelper.GetJToken<JValue>(obj, "GAME_TYPE_ID"));
           this.GAMELENGTH = JsonClassHelper.ReadString(JsonClassHelper.GetJToken<JValue>(obj, "GAME_LENGTH"));
           this.TIMELIMIT = JsonClassHelper.ReadString(JsonClassHelper.GetJToken<JValue>(obj, "TIME_LIMIT"));
           this.OLDPUBLICID = JsonClassHelper.ReadString(JsonClassHelper.GetJToken<JValue>(obj, "OLD_PUBLIC_ID"));
           this.WINNINGTEAM = JsonClassHelper.ReadString(JsonClassHelper.GetJToken<JValue>(obj, "WINNING_TEAM"));
           this.TOTALROUNDS = JsonClassHelper.ReadInteger(JsonClassHelper.GetJToken<JValue>(obj, "TOTAL_ROUNDS"));
           this.MAPNAME = JsonClassHelper.ReadString(JsonClassHelper.GetJToken<JValue>(obj, "MAP_NAME"));
           this.PREMIUM = JsonClassHelper.ReadString(JsonClassHelper.GetJToken<JValue>(obj, "PREMIUM"));
           this.MAP = JsonClassHelper.ReadString(JsonClassHelper.GetJToken<JValue>(obj, "MAP"));
           this.SERVERID = JsonClassHelper.ReadString(JsonClassHelper.GetJToken<JValue>(obj, "SERVER_ID"));
           this.MAPID = JsonClassHelper.ReadString(JsonClassHelper.GetJToken<JValue>(obj, "MAP_ID"));
           this.SERVERREALM = JsonClassHelper.ReadString(JsonClassHelper.GetJToken<JValue>(obj, "SERVER_REALM"));
		   this.LEASTDEATHS = JsonClassHelper.ReadStronglyTypedObject<PlayerStat>(JsonClassHelper.GetJToken<JObject>(obj, "LEAST_DEATHS"));
           this.QLS = JsonClassHelper.ReadString(JsonClassHelper.GetJToken<JValue>(obj, "QLS"));
           this.FIRSTSCORER = JsonClassHelper.ReadString(JsonClassHelper.GetJToken<JValue>(obj, "FIRST_SCORER"));
           this.SERVERTITLE = JsonClassHelper.ReadString(JsonClassHelper.GetJToken<JValue>(obj, "SERVER_TITLE"));
           this.LASTSCORER = JsonClassHelper.ReadString(JsonClassHelper.GetJToken<JValue>(obj, "LAST_SCORER"));
           this.ABORTED = JsonClassHelper.ReadString(JsonClassHelper.GetJToken<JValue>(obj, "ABORTED"));
           this.TOTALKILLS = JsonClassHelper.ReadInteger(JsonClassHelper.GetJToken<JValue>(obj, "TOTAL_KILLS"));
           this.OWNER = JsonClassHelper.ReadString(JsonClassHelper.GetJToken<JValue>(obj, "OWNER"));
           this.CAPTURELIMIT = JsonClassHelper.ReadString(JsonClassHelper.GetJToken<JValue>(obj, "CAPTURE_LIMIT"));
		   this.DMGDELIVERED = JsonClassHelper.ReadStronglyTypedObject<PlayerStat>(JsonClassHelper.GetJToken<JObject>(obj, "DMG_DELIVERED"));
           this.GAMETIMESTAMPNICE = JsonClassHelper.ReadString(JsonClassHelper.GetJToken<JValue>(obj, "GAME_TIMESTAMP_NICE"));
           this.GAMELENGTHNICE = JsonClassHelper.ReadString(JsonClassHelper.GetJToken<JValue>(obj, "GAME_LENGTH_NICE"));
           this.GAMEEXPIRESFULL = JsonClassHelper.ReadString(JsonClassHelper.GetJToken<JValue>(obj, "GAME_EXPIRES_FULL"));

		   this.TEAMSCOREBOARD = (TeamScoreboard[])JsonClassHelper.ReadArray<TeamScoreboard>(JsonClassHelper.GetJToken<JArray>(obj, "TEAM_SCOREBOARD"), JsonClassHelper.ReadStronglyTypedObject<TeamScoreboard>, typeof(TeamScoreboard[])) ?? new TeamScoreboard[0];

		   this.BLUESCOREBOARD = (Scoreboard[])JsonClassHelper.ReadArray<Scoreboard>(JsonClassHelper.GetJToken<JArray>(obj, "BLUE_SCOREBOARD"), JsonClassHelper.ReadStronglyTypedObject<Scoreboard>, typeof(Scoreboard[])) ?? new Scoreboard[0];
		   this.REDSCOREBOARD = (Scoreboard[])JsonClassHelper.ReadArray<Scoreboard>(JsonClassHelper.GetJToken<JArray>(obj, "RED_SCOREBOARD"), JsonClassHelper.ReadStronglyTypedObject<Scoreboard>, typeof(Scoreboard[])) ?? new Scoreboard[0];
		   this.BLUESCOREBOARDQUITTERS = (Scoreboard[])JsonClassHelper.ReadArray<Scoreboard>(JsonClassHelper.GetJToken<JArray>(obj, "BLUE_SCOREBOARD_QUITTERS"), JsonClassHelper.ReadStronglyTypedObject<Scoreboard>, typeof(Scoreboard[])) ?? new Scoreboard[0];
		   this.REDSCOREBOARDQUITTERS = (Scoreboard[])JsonClassHelper.ReadArray<Scoreboard>(JsonClassHelper.GetJToken<JArray>(obj, "RED_SCOREBOARD_QUITTERS"), JsonClassHelper.ReadStronglyTypedObject<Scoreboard>, typeof(Scoreboard[])) ?? new Scoreboard[0];
		}

		public readonly Scoreboard[] BLUESCOREBOARD;
		public readonly Scoreboard[] REDSCOREBOARD;
		public readonly Scoreboard[] BLUESCOREBOARDQUITTERS;
		public readonly Scoreboard[] REDSCOREBOARDQUITTERS;

        public readonly TeamScoreboard[] TEAMSCOREBOARD;
        public readonly string PUBLICID;
        public readonly string RESTARTED;
        public readonly int AVGACC;
        public readonly string TIER;
        public readonly string RANKED;
        public readonly string EXITMSG;
        public readonly string GAMETYPEFULL;
        public readonly string PLAYERID;
        public readonly string GAMETIMESTAMP;
        public readonly string TSCORE1;
        public readonly string TSCORE0;
        public readonly string TRAINING;
        public readonly string MAPNAMESHORT;
        public readonly PlayerStat MOSTDEATHS;
        public readonly string GAMETYPE;
		public readonly PlayerStat DMGTAKEN;
		public readonly PlayerStat MOSTACCURATE;
        public readonly string FRAGLIMIT;
        public readonly string LASTTEAMSCORER;
        public readonly string NUMPLAYERS;
        public readonly string GAMETYPEID;
        public readonly string GAMELENGTH;
        public readonly string TIMELIMIT;
        public readonly string OLDPUBLICID;
        public readonly string WINNINGTEAM;
        public readonly int TOTALROUNDS;
        public readonly string MAPNAME;
        public readonly string PREMIUM;
		public readonly string MAP;
        public readonly string SERVERID;
        public readonly string MAPID;
        public readonly string SERVERREALM;
		public readonly PlayerStat LEASTDEATHS;
        public readonly string QLS;
        public readonly string FIRSTSCORER;
        public readonly string SERVERTITLE;
        public readonly string LASTSCORER;
        public readonly string ABORTED;
        public readonly int TOTALKILLS;
        public readonly string OWNER;
        public readonly string CAPTURELIMIT;
		public readonly PlayerStat DMGDELIVERED;
        public readonly string GAMETIMESTAMPNICE;
        public readonly string GAMELENGTHNICE;
        public readonly string GAMEEXPIRESFULL;
    }
}
