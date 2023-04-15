using System;

namespace CSharpApp
{
    class Competition
    {
        public List<Auto> Cars { get; init; } // массив машин
        public List<Carriage> HorseCarriages { get; init; } // массив повозок

        public Competition(List<Auto> cars, List<Carriage> carriages)
        {
            Cars = cars;
            HorseCarriages = carriages;
        }

         
        public  List<Result> RunCompetition() { //начало соерeвнования
            List<Result> results = new List<Result>();
            int place = 1;

            foreach (Auto car in Cars) {
                double actualDistance = car.ActualDistance();
                results.Add(new Result(car.Name, place++, actualDistance));
            }

            foreach (Carriage horseCarriage in HorseCarriages) {
                double actualDistance = horseCarriage.ActualDistance();
                results.Add(new Result(horseCarriage.Name, place++, actualDistance));
            }

            results.OrderBy(p => p.Distance);
            return results; 
        }
    }   
        

    
}
