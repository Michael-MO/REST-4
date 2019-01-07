using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public class Meassurement
    {
        private int _id;
        private double _pressure;
        private double _humidity;
        private double _temperature;
        private DateTime _timeOfEntry;

        public Meassurement(int id, double pressure, double humidity, double temperature, DateTime timeOfEntry)
        {
            _id = id;
            _pressure = pressure;
            _humidity = humidity;
            _temperature = temperature;
            _timeOfEntry = timeOfEntry;
        }

        public Meassurement() { }

        public int ID
        {
            get => _id;
            set => _id = value;
        }

        public double Pressure
        {
            get => _pressure;
            set => _pressure = value;
        }

        public double Humidity
        {
            get => _humidity;
            set => _humidity = value;
        }

        public double Temperature
        {
            get => _temperature;
            set => _temperature = value;
        }

        public DateTime TimeOfEntry
        {
            get => _timeOfEntry;
            set => _timeOfEntry = value;
        }
    }
}
