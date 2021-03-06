namespace Thermometers.Tests
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using FluentAssertions;
    using Starkie.RaspieThermometer.Thermometers;
    using Starkie.RaspieThermometer.Thermometers.Contracts;
    using Xunit;

    /// <summary> Tests for the DS18B20 Thermometer sensor. </summary>
    public class DS18B20ThermometerTests
    {
        private static readonly string DevicesDirectory = Path.GetFullPath("../../../Resources");

        /// <summary>
        ///     The Temperature property. When queried on a properly registered sensor, it should
        ///     return the current temperature.
        /// </summary>
        [Fact]
        public void Temperature_SensorReadsTemperatureCorrectly_ShouldReturnExistingTemperature()
        {
            // Arrange.
            IEnumerable<IThermometer> thermometers = DS18B20ThermometerLoader.GetThermometers(DevicesDirectory);

            thermometers.Should().HaveCount(1);

            // Act.
            IThermometer thermometer = thermometers.First();

            TemperatureMeasurement temperatureMeasurement = thermometer.Temperature;

            // Assert.
            TemperatureMeasurement expectedMeasurement = new TemperatureMeasurement("28-3c01a816c730", MeasurementStatus.Ok, DateTime.Now, 30.062);

            temperatureMeasurement.SensorId.Should().Be(expectedMeasurement.SensorId);
            temperatureMeasurement.Status.Should().Be(expectedMeasurement.Status);
            temperatureMeasurement.Temperature.Should().Be(expectedMeasurement.Temperature);
        }

        /// <summary>
        ///     The Temperature property. When queried on a properly registered sensor that writes
        ///     temperatures on an unexpected format, should return an empty measurement with
        ///     the <see cref="MeasurementStatus.Failed"/> status.
        /// </summary>
        [Fact]
        public void Temperature_CannotParseTemperature_ShouldReturnExistingTemperature()
        {
            // Arrange.
            IEnumerable<IThermometer> thermometers = DS18B20ThermometerLoader.GetThermometers(DevicesDirectory);

            thermometers.Should().HaveCount(1);

            // Act.
            IThermometer thermometer = thermometers.First();

            TemperatureMeasurement temperatureMeasurement = thermometer.Temperature;

            // Assert.
            TemperatureMeasurement expectedMeasurement = new TemperatureMeasurement("28-3c01a816c731", MeasurementStatus.Failed, DateTime.Now, default);

            temperatureMeasurement.SensorId.Should().Be(expectedMeasurement.SensorId);
            temperatureMeasurement.Status.Should().Be(expectedMeasurement.Status);
            temperatureMeasurement.Temperature.Should().Be(expectedMeasurement.Temperature);
        }
    }
}