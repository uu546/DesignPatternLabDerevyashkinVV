using InteractiveMap.Console.Models;
using InteractiveMap.Console.Services;

MapService mapService = MapService.Instance;

// 1. Добавление точек
PointOfInterest park = mapService.AddPoi(
    new PointOfInterestBuilder()
        .WithName("Центральный парк")
        .WithCoordinate(new Coordinate(55.7601, 37.6184))
        .WithCategory("Парк")
        .WithDescription("Главный городской парк")
        .WithRating(4.7)
        .WithOpeningHours("круглосуточно")
        .Build());

PointOfInterest cafe = mapService.AddPoi(
    new PointOfInterestBuilder()
        .WithName("Кофейня на углу")
        .WithCoordinate(new Coordinate(55.7612, 37.6205))
        .WithCategory("Кафе")
        .WithRating(4.3)
        .WithOpeningHours("08:00–22:00")
        .Build());

PointOfInterest museum = mapService.AddPoi(
    new PointOfInterestBuilder()
        .WithName("Музей истории")
        .WithCoordinate(new Coordinate(55.7580, 37.6150))
        .WithCategory("Музей")
        .WithRating(4.9)
        .Build());

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
    mapService.AddPoi(
        new PointOfInterestBuilder()
        .WithName("Проверка ошибки")
        .WithCoordinate(new Coordinate(100, 0))
        .Build());
}

catch (Exception ex)
{
    Console.WriteLine($"Ожидаемая ошибка: {ex.Message} | тип ошибки: {ex.GetType().Name}");
}
