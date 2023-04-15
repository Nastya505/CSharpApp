using System;

namespace CSharpApp
{
    public class Carriage
    {
        public string Name { get; init; } // Прозвище участника
        public int MassCarriage { get; init; } // Масса кареты
        public List<double> HorseFatigue { get; init; } // Список усталости коней

        public Carriage(string name, int massCarriage)
        {
            if (name == null) throw new ArgumentNullException("Прозвище не может быть пустое");
            Name = name;

            if (massCarriage < 0)
            {
                throw new ArgumentException("масса не может быть меньше нуля");
            }
            MassCarriage = massCarriage;
            HorseFatigue = new List<double>(); // Создаём пустой список коней (добавляем потом через функцию)
        }


        public void AddHorse(double fatigue) // Добавить коня
        {
            HorseFatigue.Add(fatigue);
        }

        public int GetHorseCount() // Получаем кол-во коней
        {
            return HorseFatigue.Count;
        }

        public double GetHorseFatigue(int index) // Получить коня по индексу
        {
            return HorseFatigue[index];
        }

        public void RemoveHorse(int index) // удалить коня по индексу
        {
            HorseFatigue.RemoveAt(index);
        }
        public int FindIndexWeakHorse(){// находим самого слабого коня
            double minTiredness = GetHorseFatigue(0);
            int minIndex = 0;

            for (int i = 1; i < GetHorseCount(); i++) {
                double tiredness = GetHorseFatigue(i);
                if (tiredness < minTiredness) {
                    minTiredness = tiredness;
                    minIndex = i;
                }
            }
            return minIndex;
        }
      public  double ActualDistance()//пройденое расстояние
        {
            int actualDistance = 0;
            int indexWeakHorse = FindIndexWeakHorse();

            if (GetHorseFatigue(indexWeakHorse) <= 0) throw new ArgumentException("У вас конь устал");


            while (GetHorseFatigue(indexWeakHorse) > 0)
            {
                actualDistance += 1;
                
                for (int i = 0;i < GetHorseCount();i++)
                {
                    HorseFatigue[i] -= 0.5;
                }
            }
            
            return actualDistance;
        }
    }
}