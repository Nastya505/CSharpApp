using System;

namespace CSharpApp
{
    class Result
    {
        public string Name { get; set; }
        public int Place { get; set; }
        public double Distance { get; set; }

        public Result(string name, int place, double distance)
        {
            if (name == "") throw new ArgumentNullException("Прозвище не может быть пустое"); // Пример обработчика ошибки
            Name = name;
            Place = place;
            if (distance <= 0) throw new ArgumentException("Расстояние не может быть меньше нуля");
            Distance = distance;

        }
    }

    }
