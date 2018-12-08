﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        private int CalculateOperatingTime( string timestampStart, string timestampEnd)
        {
            DateTime startTime = DateTime.Parse(timestampStart);
            DateTime stopTime = DateTime.Parse(timestampEnd);
            TimeSpan elapsedTime = stopTime - startTime;
            int elapsedTimeInt = (int)elapsedTime.TotalSeconds;
            return elapsedTimeInt;
        }

        private double CalculateAvailability()
        {
            return ((operatingTime - downtime) / operatingTime);
        }

        private double CalculateQuality()
        {
            double q=0;
            try
            {
                //return (acceptableProducts / producedProducts);
                q= (acceptableProducts / producedProducts);
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
            return ((producedProducts * (60 / machineSpeed)) / operatingTime);
        }

        public double CalculateOEE()
        {
            return (CalculateAvailability() * CalculateQuality() * CalculatePerformance());
            //return 0.307;

        }
    }
}