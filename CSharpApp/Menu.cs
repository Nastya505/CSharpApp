namespace CSharpApp
{
    class Program
    {

        static void Main()
        {
            Menu menu = new Menu();
            menu.Start();
        }
    }
    public class Menu
    {
        private string err = "";
        public List<Auto> Autos { get; init; }
        public List<Carriage> Carriages { get; init; }

        public Menu()
        {
            Autos = new List<Auto>();
            Carriages = new List<Carriage>();
        }

        public void Start()
        {
            while (true)
            {
                Console.Clear();

                if (err != "") // Вывести ошибку, если она была
                {
                    Console.WriteLine("!ERROR " + err);
                    err = ""; // Обнуляем ошибку после вывода
                }

                Console.WriteLine("=== Меню соревнования ===");
                Console.WriteLine("1 - Добавить машину");
                Console.WriteLine("2 - Добавить повозку");
                Console.WriteLine("3 - Посмотреть участников");
                Console.WriteLine("4 - Начать соревнование");

                try
                {
                    DefineBehavior(GetConsoleString());
                } catch (Exception ex)
                {
                    err = ex.Message;
                }
            }
        }

        private void DefineBehavior(string value)
        {
            switch (value)
            {
                case "1":
                    AddCar();
                    break;
                case "2":
                    AddCarriage();
                    break;
                case "3":
                    CheckParcipiant();
                    break;
                case "4":
                    StartCompetition();
                    break;
            }
        }

        private void AddCar()
        {
            Console.Clear();
            Console.WriteLine("=== Добавление машины ===");
            Console.Write("Прозвище участника"); // Масса
            string name = GetConsoleString();
            Console.Write("Масса машины"); // Масса
            int mass = GetConsoleInt();
            Console.Write("Объём бака"); // Объем бака
            int fuelTank = GetConsoleInt();
            Console.WriteLine("(1 - bens, 2 - gas, 3 - disel)");
            Console.Write("Тип топлива"); // Тип топлива
            int fuelType = GetConsoleInt();
            Console.Write("Кол-вo топлива в баке"); // Количество бензина
            double fuel = GetConsoleDouble();

            Autos.Add(new Auto(name, mass, fuelTank, fuelType, fuel)); // Добавление машины в массив

            Console.Clear(); // Вывод итога
            Console.WriteLine($"=== Участник #{name} добавлен ===");
            Console.WriteLine($"Масса: {mass}");
            Console.WriteLine($"Объем бака: {fuelTank}");
            Console.WriteLine($"Тип топлива: {fuelType}");
            Console.WriteLine($"Кол-во топлива в баке: {fuel}");
            GetConsoleString();
        }
        private void AddCarriage()
        {
            Console.Clear();
            Console.WriteLine("=== Добавление повозки ===");
            Console.Write("Прозвище участника"); // Масса
            string name = GetConsoleString();
            Console.Write("Масса повозки"); // Масса
            int mass = GetConsoleInt();

            Carriages.Add(new Carriage(name, mass)); // Создаём повозку

            string horseFatigue;
            while (true) // Добавляем коней
            {
                Console.Write("Усталость коня №" + (Carriages[0].GetHorseCount() + 1) + ":");
                horseFatigue = Console.ReadLine();
                if (horseFatigue == "q") // Если нажал q - выйти
                {
                    break;
                }
                Carriages[0].AddHorse(ConvertToDouble(CheckForEmpty(horseFatigue))); // Добавляем коня
            }

            Console.Clear();
            Console.WriteLine($"=== Участник #{name} добавлен ===");
            Console.WriteLine($"Масса: {mass}");
            for (int i = 0; i < Carriages[0].GetHorseCount(); i++) // Выводим коней
            {
                Console.WriteLine($" - Конь №{i + 1} с усталостью {Carriages[0].GetHorseFatigue(i)}");
            }
            GetConsoleString();
        }
        private void StartCompetition()
        {
            List<Result> result = new Competition(Autos, Carriages).RunCompetition();

            Console.WriteLine("=== Места ===");
            foreach(Result res in result)
            {
                Console.WriteLine($" - №{res.Place}: {res.Name}");
            }

            GetConsoleString();
        }
        // 
        private void CheckParcipiant()
        {
            Console.Clear();
            Console.WriteLine("=== Машины ===");
            for (int i = 0; i < Autos.Count; i++) // Выводим машины
            {
                Console.WriteLine($"- #{Autos[i].Name}");
                Console.WriteLine($"Масса: {Autos[i].Mass}");
                Console.WriteLine($"Объём бака: {Autos[i].FuelTank}");
                Console.WriteLine($"Тип топлива: {Autos[i].FuelType}");
                Console.WriteLine($"Кол-во топлива: {Autos[i].Fuel}");
            }

            Console.WriteLine("\n=== Повозки ===");
            for (int i = 0; i < Carriages.Count; i++) // Выводим кареты
            {
                Console.WriteLine($"- #{Carriages[i].Name}");
                Console.WriteLine($"Масса: {Carriages[i].MassCarriage}");

                for (int j = 0; j < Carriages[i].GetHorseCount(); j++) // Выводим коней
                {
                    Console.WriteLine($" - Конь №{j + 1} с усталостью {Carriages[i].GetHorseFatigue(j)}");
                }

            }
            GetConsoleString();
        }
        private string CheckForEmpty(string value)
        {
            if (value == "") // Проверка на пустоту
            {
                throw new ArgumentException("Вы не указали значение");
            }
            return value;
        }
        // Удобная конвертация с проверками
        private int ConvertToInt(string value)
        {
            try
            {
                return Convert.ToInt32(value);
            }
            catch (Exception ex)
            {
                throw new ArgumentException("Вы ввели cтроку вместо числа");
            }
        }
        private double ConvertToDouble(string value)
        {
            try
            {
                return Convert.ToDouble(value);
            }
            catch (Exception ex)
            {
                throw new ArgumentException("Вы ввели cтроку вместо числа");
            }
        }
        // Получение из консоли
        private int GetConsoleInt()
        {
            Console.Write(": ");
            return ConvertToInt(CheckForEmpty(Console.ReadLine()));
        }
        private double GetConsoleDouble()
        {
            Console.Write(": ");
            return ConvertToDouble(CheckForEmpty(Console.ReadLine()));
        }
        private string GetConsoleString()
        {
            Console.Write(": ");
            return Console.ReadLine();
        }
    }
}

