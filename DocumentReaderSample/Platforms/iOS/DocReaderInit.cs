﻿using System;
using DocReaderApi.iOS;
using Foundation;

namespace DocumentReaderSample.Platforms.iOS
{
    public class DocReaderInitEvent : EventArgs, IDocReaderInitEvent
    {
        public IList<Scenario> Scenarios { get; set; }

        public bool IsSuccess { get; set; }

        public bool IsRfidAvailable { get; set; }
    }

    public class DocReaderInit : IDocReaderInit
    {
        public DocReaderInit()
        {
        }

        public event EventHandler<IDocReaderInitEvent> ScenariosObtained;

        public void InitDocReader()
        {
            Console.WriteLine("Native iOS");
            InitReader();
        }

        protected void InitReader()
        {
            var licenseData = NSData.FromFile(NSBundle.MainBundle.PathForResource("regula.license", null));
            if (licenseData == null)
            {
                return;
            }

            RGLConfig config = new(licenseData)
            {
                DelayedNNLoadEnabled = true
            };
            RGLDocReader.Shared.InitializeReaderWithConfig(config, (bool success, NSError error) =>
            {
                DocReaderInitEvent readerInitEvent = new() { IsSuccess = success };
                if (success)
                {
                    Console.WriteLine("Initialized sucessfully");


                    IList<Scenario> data = new List<Scenario>();
                    foreach (RGLScenario scenario in RGLDocReader.Shared.AvailableScenarios)
                    {
                        var scenarion = new Scenario() { Name = scenario.Identifier, Description = scenario.Description };
                        data.Add(scenarion);
                    }
                    readerInitEvent.Scenarios = data;
                    readerInitEvent.IsRfidAvailable = RGLDocReader.Shared.RfidAvailable;
                }
                else
                {
                    Console.WriteLine("Initialization failed:" + error);
                }

                ScenariosObtained(this, readerInitEvent);
            });
        }

    }
}