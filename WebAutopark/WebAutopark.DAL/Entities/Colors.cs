using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace WebAutopark.DAL.Entities
{
    public enum Colors
    {
        [Description("Default")]
        DEFAULT,

        [Description("Blue")]
        BLUE,

        [Description("Green")]
        GREEN,

        [Description("Red")]
        RED,

        [Description("White")]
        WHITE,

        [Description("Gray")]
        GRAY,

        [Description("Yellow")]
        YELLOW
    }
}
