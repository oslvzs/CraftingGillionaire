using System.ComponentModel.DataAnnotations;

namespace CraftingGillionaire.API.Saddlebag
{
    public enum SortBy
    {
        [Display(Name = "Average price")]
        AveragePrice,
        [Display(Name = "Median price")]
        MedianPrice,
        [Display(Name = "Market value")]
        MarketValue,
        [Display(Name = "Purcase amount")]
        PurchaseAmount,
        [Display(Name = "Quantity sold")]
        QuantitySold
    }
}
