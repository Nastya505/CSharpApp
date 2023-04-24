using System;
using System.Linq.Expressions;
using System.Transactions;

namespace CSharpApp
{

    public class Auto : Transport
    {
        public int FuelTank { get; init; } // Объём бака (литры)
        public int FuelType { get; init; } // Тип топлива (bens = 1, gas = 2, disel = 3)
        public double Fuel { get; set; } // Текущее кол-во топлива в баке на данный момент
        public double Rate { get; init; } // Расход топлива (считается в конструкторе и зависит от типа)

        public Auto(int mass, string name, int fuelTank, int fuelType, double fuel) : base(mass, name)
        {
            const int REDUCING_FACTOR = 1000;
            const double BENZ_COEF_OF_LOSING_FUEL = 0.7;
            const double GAZ_COEF_OF_LOSING_FUEL = 0.5;
            const double DISEL_COEF_OF_LOSING_FUEL = 1.0;

            if (name == "") throw new ArgumentNullException("Прозвище не может быть пустое");
            Name = name;

            if (mass <= 0) // Проверка массы
            {
                throw new ArgumentException("Масса не может быть меньше нуля");
            }
            Mass = mass;

            if (fuelTank <= 0) // Проверка объема бака
            {
                throw new ArgumentException("Объём топлива не может быть меньше нуля");
            }
            FuelTank = fuelTank;

            if (fuel < 0) // Проверка топлива в баке на данный момент
            {
                throw new ArgumentException("Топлива не может быть меньше нуля");
            }
            Fuel = fuel;

            double fuelFactorCache; // Временная переменная для кэширования коэф. расхода топлива (зависит от типа)
            switch(fuelType)
            {
                case 1:
                    fuelFactorCache = BENZ_COEF_OF_LOSING_FUEL; break;
                case 2:
                    fuelFactorCache = GAZ_COEF_OF_LOSING_FUEL; break;
                case 3:
                    fuelFactorCache = DISEL_COEF_OF_LOSING_FUEL; break;
                default: throw new ArgumentException("Некорректный тип топлива"); // Проверка типа топлива
            }
            FuelType = fuelType;
            Rate = (double)mass / REDUCING_FACTOR * fuelFactorCache;
        }

        public double ActualDistance() // Считает сколько машина проедет (пока не кончится топливо)
        {
            if (Fuel <= 0) throw new ArgumentException("У вас бензина нет");

            int actualDistanceKM = 0;
            while (Fuel > 0)
            {
                actualDistanceKM += 1;
                Fuel -= Rate; 
            }

            if (Fuel < 0) Fuel = 0;

            return actualDistanceKM;
        }
    }

}