using ReactiveUI;
using System;
using System.Text.Json.Serialization;

namespace CraftingGillionaire.Models.User
{
    public class UserInfo : ReactiveObject
    {
        public UserInfo()
		{
			this.CarpenterLevel = 0;
			this.ArmorerLevel = 0;
			this.BlacksmithLevel = 0;
			this.GoldsmithLevel = 0;
			this.LeatherworkerLevel = 0;
			this.WeaverLevel = 0;
			this.AlchemistLevel = 0;
			this.CulinarianLevel = 0;
        }

		public int CarpenterLevel { get; set; } = 0;

		[JsonIgnore]
        public string CarpenterLevelString
        {
            get => this.CarpenterLevel.ToString();
            set
            {
				if (Int32.TryParse(value, out int carpenterLevel) && carpenterLevel >= 0 && carpenterLevel <= 90)
				{
					this.CarpenterLevel = carpenterLevel;
					this.RaisePropertyChanged(nameof(this.CarpenterLevelString));
				}
			}
        }

		public int BlacksmithLevel { get; set; } = 0;

		[JsonIgnore]
		public string BlacksmithLevelString
		{
			get => this.BlacksmithLevel.ToString();
			set
			{

				string valueString = String.Empty;
				if (String.IsNullOrEmpty(value))
				{
					this.BlacksmithLevel = 1;
					this.RaisePropertyChanged(nameof(this.BlacksmithLevelString));
				}
				else if (value.Length > 2)
				{ 
					valueString = value.Substring(0, 2);
				}
				else
				{
					valueString = value;
				}

				if (!String.IsNullOrEmpty(valueString))
				{
					if (Int32.TryParse(valueString, out int blacksmithLevel) && blacksmithLevel >= 0 && blacksmithLevel <= 90)
					{
						this.BlacksmithLevel = blacksmithLevel;
						this.RaisePropertyChanged(nameof(this.BlacksmithLevelString));
					}
				}
			}
		}

		public int ArmorerLevel { get; set; } = 0;

		[JsonIgnore]
		public string ArmorerLevelString
		{
			get => this.ArmorerLevel.ToString();
			set
			{
				if (Int32.TryParse(value, out int armorerLevel) && armorerLevel >= 0 && armorerLevel <= 90)
				{
					this.ArmorerLevel = armorerLevel;
					this.RaisePropertyChanged(nameof(this.ArmorerLevelString));
				}
			}
		}

		public int GoldsmithLevel { get; set; } = 0;

		[JsonIgnore]
		public string GoldsmithLevelString
		{
			get => this.GoldsmithLevel.ToString();
			set
			{
				if (Int32.TryParse(value, out int goldsmithLevel) && goldsmithLevel >= 0 && goldsmithLevel <= 90)
				{
					this.GoldsmithLevel = goldsmithLevel;
					this.RaisePropertyChanged(nameof(this.GoldsmithLevelString));
				}
			}
		}

		public int LeatherworkerLevel { get; set; } = 0;

		[JsonIgnore]
		public string LeatherworkerLevelString
		{
			get => this.LeatherworkerLevel.ToString();
			set
			{
				if (Int32.TryParse(value, out int leatherworkerLevel) && leatherworkerLevel >= 0 && leatherworkerLevel <= 90)
				{
					this.LeatherworkerLevel = leatherworkerLevel;
					this.RaisePropertyChanged(nameof(this.LeatherworkerLevelString));
				}
			}
		}

		public int WeaverLevel { get; set; } = 0;

		[JsonIgnore]
		public string WeaverLevelString
		{
			get => this.WeaverLevel.ToString();
			set
			{
				if (Int32.TryParse(value, out int weaverLevel) && weaverLevel >= 0 && weaverLevel <= 90)
				{
					this.WeaverLevel = weaverLevel;
					this.RaisePropertyChanged(nameof(this.WeaverLevelString));
				}
			}
		}

		public int AlchemistLevel { get; set; } = 0;

		[JsonIgnore]
		public string AlchemistLevelString
		{
			get => this.AlchemistLevel.ToString();
			set
			{
				if (Int32.TryParse(value, out int alchemistLevel) && alchemistLevel >= 0 && alchemistLevel <= 90)
				{
					this.AlchemistLevel = alchemistLevel;
					this.RaisePropertyChanged(nameof(this.AlchemistLevelString));
				}
			}
		}

		public int CulinarianLevel { get; set; } = 0;

		[JsonIgnore]
		public string CulinarianLevelString
		{
			get => this.CulinarianLevel.ToString();
			set
			{
				if (Int32.TryParse(value, out int culinarianLevel) && culinarianLevel >= 0 && culinarianLevel <= 90)
				{
					this.CulinarianLevel = culinarianLevel;
					this.RaisePropertyChanged(nameof(this.CulinarianLevelString));
				}
			}
		}

		public int GetLevelByJobID(int jobID)
        {
            switch (jobID)
            {
                case 8:
                    return this.CarpenterLevel;
                case 9:
                    return this.BlacksmithLevel;
                case 10:
                    return this.ArmorerLevel;
				case 11:
					return this.GoldsmithLevel;
				case 12:
					return this.LeatherworkerLevel;
				case 13:
					return this.WeaverLevel;
				case 14:
					return this.AlchemistLevel;
				case 15:
					return this.CulinarianLevel;
				default:
                    throw new Exception($"Job with ID = {jobID} is not a crafting job.");
            }
        }

        public void SetLevelByJobID(int jobID, int level)
        {
            switch (jobID)
            {
                case 8:
                    this.CarpenterLevel = level;
                    break;
                case 9:
                    this.BlacksmithLevel = level;
                    break;
                case 10:
                    this.ArmorerLevel = level;
                    break;
				case 11:
					this.GoldsmithLevel = level;
					break;
				case 12:
					this.LeatherworkerLevel = level;
					break;
				case 13:
					this.WeaverLevel = level;
					break;
				case 14:
					this.AlchemistLevel = level;
					break;
				case 15:
					this.CulinarianLevel = level;
					break;
                default:
					throw new Exception($"Job with ID = {jobID} is not a crafting job.");
			}
        }
    }
}
