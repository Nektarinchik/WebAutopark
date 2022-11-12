using System.ComponentModel;

namespace WebAutopark.DAL.Entities
{
    public enum SortState
    {
        [Description("Default")]
        DEFAULT,

        [Description("Model")]
        MODEL,

        [Description("Vehicle Type")]
        VEHICLETYPE,

        [Description("Mileage")]
        MILEAGE
    }
}
