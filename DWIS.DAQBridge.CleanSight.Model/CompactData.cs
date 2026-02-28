using DWIS.RigOS.Common.Worker;
using OSDC.DotnetLibraries.Drilling.DrillingProperties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DWIS.DAQBridge.CleanSight.Model
{
    public class CompactData
    {
        public DateTimeOffset? TimeStampUTC { get; set; } = null;
        public double? ShakerLoadEstimatesA { get; set; } = null;
        public double? ShakerLoadEstimatesB { get; set; } = null;

        public double? AverageShakerLoadEstimate { get; set; } = null;

        public double? CuttingsRecoveryRatesA { get; set; } = null;
        public double? CuttingsRecoveryRatesB { get; set; } = null;

        public double? AccumulatedCuttingsRecoveryA { get; set; } = null;
        public double? AccumulatedCuttingsRecoveryB { get; set; } = null;

        public double? OverallCuttingsRecovery { get; set; } = null;

        public double? TotalTheoreticalCuttingsVolume { get; set; } = null;

        public double? MaxCavingSizes { get; set; } = null;

        public double? OverallMaxCavingSize { get; set; } = null;

        public double? OverallAverageCuttingSize { get; set; } = null;

        public double? ShakerLoadDistribution01A { get; set; } = null;

        public double? ShakerLoadDistribution02A { get; set; } = null;

        public double? ShakerLoadDistribution03A { get; set; } = null;

        public double? ShakerLoadDistribution04A { get; set; } = null;

        public double? ShakerLoadDistribution05A { get; set; } = null;

        public double? ShakerLoadDistribution06A { get; set; } = null;

        public double? ShakerLoadDistribution07A { get; set; } = null;

        public double? ShakerLoadDistribution08A { get; set; } = null;

        public double? ShakerLoadDistribution09A { get; set; } = null;

        public double? ShakerLoadDistribution10A { get; set; } = null;

        public double? ShakerLoadDistribution01B { get; set; } = null;

        public double? ShakerLoadDistribution02B { get; set; } = null;

        public double? ShakerLoadDistribution03B { get; set; } = null;

        public double? ShakerLoadDistribution04B { get; set; } = null;

        public double? ShakerLoadDistribution05B { get; set; } = null;

        public double? ShakerLoadDistribution06B { get; set; } = null;

        public double? ShakerLoadDistribution07B { get; set; } = null;

        public double? ShakerLoadDistribution08B { get; set; } = null;

        public double? ShakerLoadDistribution09B { get; set; } = null;

        public double? ShakerLoadDistribution10B { get; set; } = null;

        public void Transfer(CleanSightRawData rawData, DateTimeOffset timeStamp)
        {
            TimeStampUTC = timeStamp;
            if (rawData.ShakerLoadEstimatesA is not null)
            {
                ShakerLoadEstimatesA = rawData.ShakerLoadEstimatesA.Value;
            }
            if (rawData.ShakerLoadEstimatesB is not null)
            {
                ShakerLoadEstimatesB = rawData.ShakerLoadEstimatesB.Value;
            }
            if (rawData.AverageShakerLoadEstimate is not null)
            {
                AverageShakerLoadEstimate = rawData.AverageShakerLoadEstimate.Value;
            }

            if (rawData.CuttingsRecoveryRatesA is not null)
            {
                CuttingsRecoveryRatesA = rawData.CuttingsRecoveryRatesA.Value;
            }
            if (rawData.CuttingsRecoveryRatesB is not null)
            {
                CuttingsRecoveryRatesB = rawData.CuttingsRecoveryRatesB.Value;
            }

            if (rawData.AccumulatedCuttingsRecoveryA is not null)
            {
                AccumulatedCuttingsRecoveryA = rawData.AccumulatedCuttingsRecoveryA.Value;
            }
            if (rawData.AccumulatedCuttingsRecoveryB is not null)
            {
                AccumulatedCuttingsRecoveryB = rawData.AccumulatedCuttingsRecoveryB.Value;
            }

            if (rawData.OverallCuttingsRecovery is not null)
            {
                OverallCuttingsRecovery = rawData.OverallCuttingsRecovery.Value;
            }

            if (rawData.TotalTheoreticalCuttingsVolume is not null)
            {
                TotalTheoreticalCuttingsVolume = rawData.TotalTheoreticalCuttingsVolume.Value;
            }

            if (rawData.MaxCavingSizes is not null)
            {
                MaxCavingSizes = rawData.MaxCavingSizes.Value;
            }

            if (rawData.OverallMaxCavingSize is not null)
            {
                OverallMaxCavingSize = rawData.OverallMaxCavingSize.Value;
            }

            if (rawData.OverallAverageCuttingSize is not null)
            {
                OverallAverageCuttingSize = rawData.OverallAverageCuttingSize.Value;
            }

            if (rawData.ShakerLoadDistribution01A is not null)
            {
                ShakerLoadDistribution01A = rawData.ShakerLoadDistribution01A.Value;
            }

            if (rawData.ShakerLoadDistribution02A is not null)
            {
                ShakerLoadDistribution02A = rawData.ShakerLoadDistribution02A.Value;
            }

            if (rawData.ShakerLoadDistribution03A is not null)
            {
                ShakerLoadDistribution03A = rawData.ShakerLoadDistribution03A.Value;
            }

            if (rawData.ShakerLoadDistribution04A is not null)
            {
                ShakerLoadDistribution04A = rawData.ShakerLoadDistribution04A.Value;
            }

            if (rawData.ShakerLoadDistribution05A is not null)
            {
                ShakerLoadDistribution05A = rawData.ShakerLoadDistribution05A.Value;
            }

            if (rawData.ShakerLoadDistribution06A is not null)
            {
                ShakerLoadDistribution06A = rawData.ShakerLoadDistribution06A.Value;
            }

            if (rawData.ShakerLoadDistribution07A is not null)
            {
                ShakerLoadDistribution07A = rawData.ShakerLoadDistribution07A.Value;
            }

            if (rawData.ShakerLoadDistribution08A is not null)
            {
                ShakerLoadDistribution08A = rawData.ShakerLoadDistribution08A.Value;
            }

            if (rawData.ShakerLoadDistribution09A is not null)
            {
                ShakerLoadDistribution09A = rawData.ShakerLoadDistribution09A.Value;
            }

            if (rawData.ShakerLoadDistribution10A is not null)
            {
                ShakerLoadDistribution10A = rawData.ShakerLoadDistribution10A.Value;
            }

            if (rawData.ShakerLoadDistribution01B is not null)
            {
                ShakerLoadDistribution01B = rawData.ShakerLoadDistribution01B.Value;
            }

            if (rawData.ShakerLoadDistribution02B is not null)
            {
                ShakerLoadDistribution02B = rawData.ShakerLoadDistribution02B.Value;
            }

            if (rawData.ShakerLoadDistribution03B is not null)
            {
                ShakerLoadDistribution03B = rawData.ShakerLoadDistribution03B.Value;
            }

            if (rawData.ShakerLoadDistribution04B is not null)
            {
                ShakerLoadDistribution04B = rawData.ShakerLoadDistribution04B.Value;
            }

            if (rawData.ShakerLoadDistribution05B is not null)
            {
                ShakerLoadDistribution05B = rawData.ShakerLoadDistribution05B.Value;
            }

            if (rawData.ShakerLoadDistribution06B is not null)
            {
                ShakerLoadDistribution06B = rawData.ShakerLoadDistribution06B.Value;
            }

            if (rawData.ShakerLoadDistribution07B is not null)
            {
                ShakerLoadDistribution07B = rawData.ShakerLoadDistribution07B.Value;
            }

            if (rawData.ShakerLoadDistribution08B is not null)
            {
                ShakerLoadDistribution08B = rawData.ShakerLoadDistribution08B.Value;
            }

            if (rawData.ShakerLoadDistribution09B is not null)
            {
                ShakerLoadDistribution09B = rawData.ShakerLoadDistribution09B.Value;
            }

            if (rawData.ShakerLoadDistribution10B is not null)
            {
                ShakerLoadDistribution10B = rawData.ShakerLoadDistribution10B.Value;
            }

        }
    }
}
