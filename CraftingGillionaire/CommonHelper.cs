using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace CraftingGillionaire
{
	internal static class CommonHelper
	{
		internal static string GetDatacenterByServerName(string serverName)
		{
			switch (serverName)
			{
				case "Cerberus":
				case "Louisoix":
				case "Moogle":
				case "Omega":
				case "Phantom":
				case "Ragnarok":
				case "Sagittarius":
				case "Spriggan":
					return "Chaos";

				case "Adamantoise":
				case "Cactuar":
				case "Faerie":
				case "Gilgamesh":
				case "Jenova":
				case "Midgardsormr":
				case "Sargatanas":
				case "Siren":
					return "Aether";

				case "Balmung":
				case "Brynhildr":
				case "Coeurl":
				case "Diabolos":
				case "Goblin":
				case "Malboro":
				case "Mateus":
				case "Zalera":
					return "Crystal";

				case "Halicarnassus":
				case "Maduin":
				case "Marilith":
				case "Seraph":
					return "Dynamis";

				case "Behemoth":
				case "Excalibur":
				case "Exodus":
				case "Famfrit":
				case "Hyperion":
				case "Lamia":
				case "Leviathan":
				case "Ultros":
					return "Primal";

				case "Alpha":
				case "Lich":
				case "Odin":
				case "Phoenix":
				case "Raiden":
				case "Shiva":
				case "Twintania":
				case "Zodiark":
					return "Light";

				case "Bismarck":
				case "Ravana":
				case "Sephirot":
				case "Sophia":
				case "Zurvan":
					return "Materia";

				case "Aegis":
				case "Atomos":
				case "Carbuncle":
				case "Garuda":
				case "Gungnir":
				case "Kujata":
				case "Tonberry":
				case "Typhon":
					return "Elemental";

				case "Alexander":
				case "Bahamut":
				case "Durandal":
				case "Fenrir":
				case "Ifrit":
				case "Ridill":
				case "Tiamat":
				case "Ultima":
					return "Gaia";

				case "Anima":
				case "Asura":
				case "Chocobo":
				case "Hades":
				case "Ixion":
				case "Masamune":
				case "Pandaemonium":
				case "Titan":
					return "Mana";

				case "Belias":
				case "Mandragora":
				case "Ramuh":
				case "Shinryu":
				case "Unicorn":
				case "Valefor":
				case "Yojimbo":
				case "Zeromus":
					return "Meteor";

				default:
					throw new Exception("Server name is not supported");
			}
		}

		internal static string GetJobNameByID(int jobID)
		{
			switch (jobID)
			{
				case 0:
					return "Adventurer";
				case 1:
					return "Gladiator";
				case 2:
					return "Pugilist";
				case 3:
					return "Marauder";
				case 4:
					return "Lancer";
				case 5:
					return "Archer";
				case 6:
					return "Conjurer";
				case 7:
					return "Thaumaturge";
				case 8:
					return "Carpenter";
				case 9:
					return "Blacksmith";
				case 10:
					return "Armorer";
				case 11:
					return "Goldsmith";
				case 12:
					return "Leatherworker";
				case 13:
					return "Weaver";
				case 14:
					return "Alchemist";
				case 15:
					return "Culinarian";
				case 16:
					return "Miner";
				case 17:
					return "Botanist";
				case 18:
					return "Fisher";
				case 19:
					return "Paladin";
				case 20:
					return "Monk";
				case 21:
					return "Warrior";
				case 22:
					return "Dragoon";
				case 23:
					return "Bard";
				case 24:
					return "White Mage";
				case 25:
					return "Black Mage";
				case 26:
					return "Arcanist";
				case 27:
					return "Summoner";
				case 28:
					return "Scholar";
				case 29:
					return "Rogue";
				case 30:
					return "Ninja";
				case 31:
					return "Machinist";
				case 32:
					return "Dark Knight";
				case 33:
					return "Astrologian";
				case 34:
					return "Samurai";
				case 35:
					return "Red Mage";
				case 36:
					return "Blue Mage";
				case 37:
					return "Gunbreaker";
				case 38:
					return "Dancer";
				case 39:
					return "Reaper";
				case 40:
					return "Sage";
				default:
					throw new Exception($"Unsupported jobID: {jobID}.");
			}
		}

        internal static void OpenLink(string url)
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                url = url.Replace("&", "^&");
                Process.Start(new ProcessStartInfo("cmd", $"/c start {url}") { CreateNoWindow = true });
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                Process.Start("xdg-open", url);
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            {
                Process.Start("open", url);
            }
        }
    }
}
