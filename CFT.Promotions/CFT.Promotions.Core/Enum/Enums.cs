using System.ComponentModel.DataAnnotations;

namespace CFT.Promotions.Core.Enum
{
    public enum Rating
    {
        [Display(Description = "Poor")]
        Poor,

        [Display(Description = "Below Average")]
        BelowAverage,

        [Display(Description = "Average")]
        Average,

        [Display(Description = "Above Average")]
        AboveAverage,

        [Display(Description = "Excellent")]
        Excellent
    }
}