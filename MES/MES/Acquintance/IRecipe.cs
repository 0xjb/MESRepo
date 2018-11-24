using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MES.Acquintance
{
    public interface IRecipe
    {
        float BeerId { get; set; }

        float MaxSpeed { get; set; }

        string Name { get; set; }

        float Barley { get; set; }

        float Hops { get; set; }

        float Malt { get; set; }

        float Wheat { get; set; }

        float Yeast { get; set; }
    }
}