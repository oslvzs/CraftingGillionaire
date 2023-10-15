using System;

namespace CraftingGillionaire
{
	internal static class CommonInfoHelper
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

		internal static int ConvertFilterNameToID(string filterName)
		{
			switch (filterName)
			{
				case "Everything":
					return 0;
				case "Purchasable from NPC Vendor":
					return -1;
				case "Furniture Purchasable from NPC Vendor":
					return -4;
				case "Supply and Provisioning Mission Quest Items":
					return -2;
				case "Crafter Class Quest Items":
					return -3;
				case "Exclude Crafted Gear":
					return -5;
				case "Arms":
					return 1;
				case "-- Archanist's Arms":
					return 16;
				case "-- Archer's Arms":
					return 12;
				case "-- Astrologian's Arms":
					return 78;
				case "-- Blue Mage's Arms":
					return 105;
				case "-- Conjurer's Arms":
					return 15;
				case "-- Dancer's Arms":
					return 87;
				case "-- Dark Knight's Arms":
					return 76;
				case "-- Gunbreaker's Arms":
					return 86;
				case "-- Lancer's Arms":
					return 13;
				case "-- Machinist's Arms":
					return 77;
				case "-- Marauder's Arms":
					return 11;
				case "-- Pugilist's Arms":
					return 9;
				case "-- Red Mage's Arms":
					return 84;
				case "-- Rogue's Arms":
					return 73;
				case "-- Reaper's Arms":
					return 88;
				case "-- Samurai's Arms":
					return 83;
				case "-- Scholar's Arms":
					return 85;
				case "-- Sage's Arms":
					return 89;
				case "-- Thaumaturge's Arms":
					return 14;
				case "Tools":
					return 2;
				case "-- Alchemist's Tools":
					return 25;
				case "-- Armorer's Tools":
					return 21;
				case "-- Blacksmith's Tools":
					return 20;
				case "-- Botanist's Tools":
					return 28;
				case "-- Carpenter's Tools":
					return 19;
				case "-- Culinarian's Tools":
					return 26;
				case "-- Fisher's Tackle":
					return 30;
				case "-- Fisher's Tools":
					return 29;
				case "Goldsmith's Tools":
					return 22;
				case "-- Leatherworker's Tools":
					return 23;
				case "-- Miner's Tools":
					return 27;
				case "-- Weaver's Tools":
					return 24;
				case "Armor":
					return 3;
				case "-- Shields":
					return 17;
				case "-- Head":
					return 31;
				case "-- Body":
					return 33;
				case "-- Legs":
					return 35;
				case "-- Hands":
					return 36;
				case "-- Feet":
					return 37;
				case "Accessories":
					return 4;
				case "-- Necklaces":
					return 39;
				case "-- Earrings":
					return 40;
				case "-- Bracelets":
					return 41;
				case "-- Rings":
					return 42;
				case "Medicines & Meals":
					return 5;
				case "-- Medicine":
					return 43;
				case "-- Ingredients":
					return 44;
				case "-- Meals":
					return 45;
				case "-- Seafood":
					return 46;
				case "Materials":
					return 6;
				case "-- Stone":
					return 47;
				case "-- Metal":
					return 48;
				case "-- Lumber":
					return 49;
				case "-- Cloth":
					return 50;
				case "-- Leather":
					return 51;
				case "-- Bone":
					return 52;
				case "-- Reagents":
					return 53;
				case "-- Dyes":
					return 54;
				case "-- Weapon Parts":
					return 55;
				case "Other":
					return 7;
				case "-- Furnishings":
					return 56;
				case "-- Materia":
					return 57;
				case "-- Crystals":
					return 58;
				case "-- Catalysts":
					return 59;
				case "-- Miscellany":
					return 60;
				case "-- Exterior Fixtures":
					return 65;
				case "-- Interior Fixtures":
					return 66;
				case "-- Outdoor Furnishings":
					return 67;
				case "-- Chairs and Beds":
					return 68;
				case "-- Tables":
					return 69;
				case "-- Tabletop":
					return 70;
				case "-- Wall-mounted":
					return 71;
				case "-- Rugs":
					return 72;
				case "-- Seasonal Miscellany":
					return 74;
				case "-- Minions":
					return 75;
				case "-- Airship/Submersible Components":
					return 79;
				case "-- Orchestration Components":
					return 80;
				case "-- Gardening Items":
					return 81;
				case "-- Paintings":
					return 82;
				case "-- Registrable Miscellany":
					return 90;
				default:
					throw new Exception($"Unsupported filter name: {filterName}");
			}
		}
	}
}
