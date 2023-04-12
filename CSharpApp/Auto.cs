using System;
using System.Linq.Expressions;

namespace CSharpApp
{

    public class Auto 
    {
        public int Mass { get; init; } // Масса машины
        public string Name { get; init; } // Масса машины
        public int FuelTank { get; init; } // Объём бака
        public int FuelType { get; init; } // Тип топлива (bens = 1, gas = 2, disel = 3)
        public double Fuel { get; set; } // Текущее кол-во топлива в баке на данный момент
        public double Rate { get; init; } // Расход топлива (считается в конструкторе и зависит от типа)

        public Auto(string name, int mass, int fuelTank, int fuelType, double fuel)
        {
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

            double fuelTypeRate; // Временная переменная для кэширования коэф. расхода топлива (зависит от типа)
            switch(fuelType)
            {
                case 1:
                    fuelTypeRate = 0.7; break;
                case 2:
                    fuelTypeRate = 0.5; break;
                case 3:
                    fuelTypeRate = 1; break;
                default: throw new ArgumentException("Некорректный тип топлива"); // Проверка типа топлива
            }
            FuelType = fuelType;
            Rate = (double)mass / 1000 * fuelTypeRate;
        }

        public double Move() // Считает сколько машина проедет (пока не кончится топливо)
        {
            int actualDistance = 0;
            while (Fuel > 0)
            {
                actualDistance += 1;
                Fuel -= Rate;
            }
            return actualDistance;
        }
    }

}