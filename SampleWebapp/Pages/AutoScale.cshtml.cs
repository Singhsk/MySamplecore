using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Diagnostics;
using System.Threading;

namespace SampleWebapp.Pages
{
    public class AutoScaleModel : PageModel
    {
        public void OnGet()
        {
            // Start auto scaling.
            this.SimulateAutoScale();
        }

        private void SimulateAutoScale()
        {
            int percentage = 80;
            Stopwatch timeToRun = new Stopwatch();
            timeToRun.Start();

            for (int i = 0; i < Environment.ProcessorCount; i++)
            {
                (new Thread(() =>
                {
                    Stopwatch watch = new Stopwatch();
                    watch.Start();

                    // Run for 10 minutes and then stop.
                    while (timeToRun.ElapsedMilliseconds <= 600000)
                    {
                        // Make the loop go on for "percentage" milliseconds then sleep the 
                        // remaining percentage milliseconds. So 80% utilization means work 80ms and sleep 20ms
                        if (watch.ElapsedMilliseconds > percentage)
                        {
                            Thread.Sleep(100 - percentage);
                            watch.Reset();
                            watch.Start();
                        }
                    }
                })).Start();
            }
        }
    }
}