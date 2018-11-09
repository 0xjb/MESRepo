using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MES.Acquintance
{
    public interface IBatchValueSet
    {
        float GetTemperature();

        float GetHumidity();

        float GetVibration();

        string GetTimeStamp();
    }
}
