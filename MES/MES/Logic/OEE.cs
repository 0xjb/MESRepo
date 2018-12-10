using System;
using System.Windows;

namespace MES.Logic
{
    class OEE
    {
        private int acceptableProducts;
        private int producedProducts;
        private int downtime = 0;
        private int operatingTime;
        private int machineSpeed;

        public OEE(int acceptableProducts, int defectProducts, string timeStampStart, string timeStampEnd, int machineSpeed)
        {
            this.acceptableProducts = acceptableProducts;
            this.producedProducts = acceptableProducts + defectProducts;
            this.operatingTime = CalculateOperatingTime(timeStampStart, timeStampEnd);
            this.machineSpeed = machineSpeed;
        }

        private int CalculateOperatingTime(string timestampStart, string timestampEnd)
        {
            DateTime startTime = DateTime.Parse(timestampStart);
            DateTime stopTime = DateTime.Parse(timestampEnd);
            TimeSpan elapsedTime = stopTime - startTime;
            int elapsedTimeInt = (int)elapsedTime.TotalSeconds;
            return elapsedTimeInt;
        }

        private double CalculateAvailability()
        {
            return (((double)operatingTime - (double)downtime) / (double)operatingTime);
        }

        private double CalculateQuality()
        {
            double q = 0;
            try
            {
                //return (acceptableProducts / producedProducts);
                q = ((double)acceptableProducts / (double)producedProducts);
            }
            catch (System.DivideByZeroException e)
            {
                Console.WriteLine(e);

            }
            //return (acceptableProducts / producedProducts);
            return q;
        }

        private double CalculatePerformance()
        {
            return (((double)producedProducts * (60.0 / (double)machineSpeed)) / (double)operatingTime);
        }

        public double CalculateOEE()
        {
            return (CalculateAvailability() * CalculateQuality() * CalculatePerformance());
            //return 0.307;

        }
    }
}
