using InteractiveMap.Console.Adapters;
using InteractiveMap.Console.Bridge;
using InteractiveMap.Console.Composite;
using InteractiveMap.Console.Decorator;
using InteractiveMap.Console.Factories;
using InteractiveMap.Console.Models;
using InteractiveMap.Console.Services;

MapService mapService = MapService.Instance;


void Lab1Builder()
{
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

}

void Lab2AbstractFactory()
{
    IMapFactory factory = new TouristMapFactory();

    // Абстрактная фабрика создаёт семество: точку и её описание.
    IPointOfInterest point = factory.CreatePoi("Центральный парк", new Coordinate(55.7601, 37.6184));
    IPoiDescription pointDescription = factory.CreateDescription(point.Name);

    PointOfInterest pointAdapter = new PointOfInterestAdapter(point).ToPointOfInterest();

    mapService.AddPoi(pointAdapter);

    Console.WriteLine($"[Factory Tourist] {point.Name}, {point.Category}, {point.OpeningHours}, *{point.Rating}");
    Console.WriteLine($"  Описание: {pointDescription.Content}");

    // Меняем семейство на другую фабрику.
    factory = new FoodMapFactory();

    point = factory.CreatePoi("Кофейня на углу", new Coordinate(55.7612, 37.6205));
    pointDescription = factory.CreateDescription(point.Name);

    pointAdapter = new PointOfInterestAdapter(point).ToPointOfInterest();


    mapService.AddPoi(pointAdapter);

    Console.WriteLine($"\n[Factory Food] {point.Name}, {point.Category}, {point.OpeningHours}, *{point.Rating}");
    Console.WriteLine($"  Описание: {pointDescription.Content}");

    Console.WriteLine("\nВсе точки на карте:");
    foreach (PointOfInterest p in mapService.All)
    {
        Console.WriteLine(p);
    }
}

//Lab2AbstractFactory();

void Lab3()
{

    // --- Lab2: фабрики создают продукты ---
    IMapFactory touristFactory = new TouristMapFactory();
    IMapFactory foodFactory = new FoodMapFactory();

    IPointOfInterest park = touristFactory.CreatePoi("Центральный парк", new Coordinate(55.7601, 37.6184));
    IPointOfInterest museum = touristFactory.CreatePoi("Музей истории", new Coordinate(55.7580, 37.6150));
    IPointOfInterest cafe = foodFactory.CreatePoi("Кофейня на углу", new Coordinate(55.7612, 37.6205));

    // Lab2: адаптер
    mapService.AddPoi(new PointOfInterestAdapter(park).ToPointOfInterest());
    mapService.AddPoi(new PointOfInterestAdapter(museum).ToPointOfInterest());
    mapService.AddPoi(new PointOfInterestAdapter(cafe).ToPointOfInterest());

    System.Console.WriteLine("Реестр MapService:");
    foreach (PointOfInterest p in mapService.All)
    {
        System.Console.WriteLine(p);
    }

    // --- Lab3 (Bridge): комбинируем Вид карты × Способ отрисовки ---
    System.Console.WriteLine("\n----- Комбинация 1: TouristicMap + ConsoleRenderer -----");
    Map touristicConsole = new TouristicMap(new ConsoleRenderer());
    touristicConsole.AddPoint(park);
    touristicConsole.AddPoint(museum);
    touristicConsole.Draw();

    System.Console.WriteLine("\n----- Комбинация 2: FoodMap + GraphicsWindowRenderer -----");
    Map foodGraphics = new FoodMap(new GraphicsWindowRenderer());
    foodGraphics.AddPoint(cafe);
    foodGraphics.Draw();

    System.Console.WriteLine("\n----- Комбинация 3: TouristicMap + GraphicsWindowRenderer -----");
    Map touristicGraphics = new TouristicMap(new GraphicsWindowRenderer());
    touristicGraphics.AddPoint(park);
    touristicGraphics.AddPoint(museum);
    touristicGraphics.Draw();
}

// Листья — точки интереса.
PointOfInterestLeaf park = new("Центральный парк", "Парк", "55.7601, 37.6184");
PointOfInterestLeaf museum = new("Музей истории", "Музей", "55.7580, 37.6150");
PointOfInterestLeaf cafe = new("Кофейня на углу", "Кафе", "55.7612, 37.6205");
PointOfInterestLeaf shop = new("Книжный магазин", "Магазин", "55.7595, 37.6170");

// Подрегионы (тоже композиты).
MapRegionComposite kitayGorod = new("Китай-город");
kitayGorod.Add(museum);
kitayGorod.Add(shop);

MapRegionComposite arbat = new("Арбат");
arbat.Add(cafe);

// Главный регион — содержит подрегионы и точки.
MapRegionComposite center = new("Центр");
center.Add(park);
center.Add(kitayGorod);
center.Add(arbat);

// Клиент работает с любым узлом одинаково — лист или группа.
Console.WriteLine(center.Describe());
Console.WriteLine($"\nВсего точек в регионе \"Центр\": {center.Count()}");
Console.WriteLine($"Всего точек в \"Китай-город\": {kitayGorod.Count()}");
Console.WriteLine($"Всего в листе \"Парк\": {park.Count()}");

System.Console.WriteLine("\n\n===== Декоратор: динамическое форматирование вывода =====\n");

// Базовый форматтер — только имя.
IPoiFormatter formatter = new BasePoiFormatter();
System.Console.WriteLine("1) Базовый:            " + formatter.Format("Центральный парк", "Парк", 4.7, "круглосуточно"));

// Оборачиваем одной обязанностью.
formatter = new RatingDecorator(formatter);
System.Console.WriteLine("2) +рейтинг:           " + formatter.Format("Центральный парк", "Парк", 4.7, "круглосуточно"));

// Навешиваем ещё одну.
formatter = new OpeningHoursDecorator(formatter);
System.Console.WriteLine("3) +часы работы:       " + formatter.Format("Центральный парк", "Парк", 4.7, "круглосуточно"));

// И ещё одну.
formatter = new CategoryDecorator(formatter);
System.Console.WriteLine("4) +категория:         " + formatter.Format("Центральный парк", "Парк", 4.7, "круглосуточно"));
