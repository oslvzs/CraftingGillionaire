using DynamicData;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace CraftingGillionaire.Models.CraftingAnalyzer
{
    public class CraftingInfo
    {
        public CraftingInfo(int craftingAmount, int craftingCosts, List<CraftingPart> craftingParts, CraftingJobInfo craftingJobInfo)
        {
            CraftingAmount = craftingAmount;
            CraftingCostsPerUnit = craftingCosts;
            CraftingTotalCosts = craftingAmount * craftingCosts;
            CraftingParts = new ObservableCollection<CraftingPart>();
            CraftingParts.AddRange(craftingParts);
            CraftingJobInfo = craftingJobInfo;
        }

        public int CraftingAmount { get; }

        public int CraftingCostsPerUnit { get; }

        public int CraftingTotalCosts { get; }

        public ObservableCollection<CraftingPart> CraftingParts { get; set; }

        public CraftingJobInfo CraftingJobInfo { get; }
    }
}
