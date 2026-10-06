using InteractiveMap.Console.Models;
using InteractiveMap.Console.Services;

MapService mapService = new MapService();

// 1. Добавление точек
PointOfInterest park = mapService.AddPoi("Центральный парк", new Coordinate(55.7601, 37.6184));
PointOfInterest cafe = mapService.AddPoi("Кофейня на углу", new Coordinate(55.7612, 37.6205));
PointOfInterest museum = mapService.AddPoi("Музей истории", new Coordinate(55.7580, 37.6150));

Console.WriteLine("Все точки");
foreach (PointOfInterest p in mapService.All)
{
    Console.WriteLine(p);
}

// 2. Поиск по имени
Console.WriteLine("\nПоиск: \"Музей истории\" ");
mapService.FindByName("Музей истории")?.ToString();

// 3. Расстояние
Console.WriteLine($"\nРасстояние парк -> музей: {mapService.GetDistance(park.Id, museum.Id):3} км");

// 4. Удаление
Console.WriteLine($"\nУдаление #{cafe.Id}: {mapService.RemovePoi(cafe.Id)}");
Console.WriteLine("Осталось точек: " + mapService.All.Count);

// 5. Проверка валидации (ожидаем ошибку)
Console.WriteLine("\nПроверка валидации, добавление точки (широта 100)");

try
{
    mapService.AddPoi("Ошибка", new Coordinate(100, 0));
}

catch (Exception ex)
{
    Console.WriteLine($"Ожидаемая ошибка: {ex.Message} | тип ошибки: {ex.GetType().Name}");
}