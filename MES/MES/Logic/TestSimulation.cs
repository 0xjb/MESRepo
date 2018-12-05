using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Timers;
using MES.Acquintance;
using MES.Annotations;
using UnifiedAutomation.UaClient;
using Timer = System.Timers.Timer;

namespace MES.Logic
{
    public class TestSimulation : INotifyPropertyChanged

    {
        private OpcClient opc;
        private System.Timers.Timer timerTemp;
        private ILogic iLogic;


        public TestSimulation(OpcClient opcClient, ILogic iLogic)

        {
            this.iLogic = iLogic;
            timerTemp = new Timer();
            timerTemp.Interval = 5000;
            timerTemp.AutoReset = true;
            timerTemp.Enabled = true;
            timerTemp.Elapsed += OnTimedEvent;

            this.opc = opcClient;
            opc.TempCurrent = 35;
            opc.VibrationCurrent = 2;
            opc.HumidityCurrent = 200;


        }

  

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        bool b = false;

        private void OnTimedEvent(Object source, System.Timers.ElapsedEventArgs e)
        {
            if (iLogic.IsSimulationOn)
            {
                Random random = new Random();

                double numberTemp = random.NextDouble() / 8;
                double numberHumid = random.NextDouble() ;
                double numberVibration = random.NextDouble();
                numberTemp = Math.Round(numberTemp, 2);
                numberHumid = Math.Round(numberHumid, 2);
                numberVibration = Math.Round(numberVibration, 2);

                if (b == false)
                {
                    opc.TempCurrent += numberTemp;
                    opc.HumidityCurrent += numberHumid;
                    opc.VibrationCurrent += numberVibration;
                    b = true;
                }
                else
                {
                    opc.TempCurrent -= numberTemp;
                    opc.HumidityCurrent -= numberHumid;
                    opc.VibrationCurrent -= numberVibration;
                    b = false;
                }
            }
            else
            {
                opc.TempCurrent = 0;
                opc.HumidityCurrent = 0;
                opc.VibrationCurrent = 0;
            }
        }
    }
}