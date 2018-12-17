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
        private int machineSpeedMax;

        public OEE(int acceptableProducts, int defectProducts, string timeStampStart, string timeStampEnd, int machineSpeedMax)
        {
            this.acceptableProducts = acceptableProducts;
            this.producedProducts = acceptableProducts + defectProducts;
            this.operatingTime = CalculateOperatingTime(timeStampStart, timeStampEnd);
            this.machineSpeedMax = machineSpeedMax;
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

                q = ((double)acceptableProducts / (double)producedProducts);
            }
            catch (System.DivideByZeroException e)
            {
                Console.WriteLine(e);

            }

            return q;
        }

        private double CalculatePerformance()
        {

            return ((double)producedProducts / ((double) operatingTime/60.0)) / (double)machineSpeedMax;
        }

        public double CalculateOEE()
        {
 
            return (CalculateAvailability() * CalculateQuality() * CalculatePerformance());


        }
    }
}
