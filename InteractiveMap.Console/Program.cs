using InteractiveMap.Console.Abstractions;
using InteractiveMap.Console.Adapters;
using InteractiveMap.Console.Bridge;
using InteractiveMap.Console.Commands;
using InteractiveMap.Console.Composite;
using InteractiveMap.Console.Decorator;
using InteractiveMap.Console.Export;
using InteractiveMap.Console.Facade;
using InteractiveMap.Console.Factories;
using InteractiveMap.Console.Flyweight;
using InteractiveMap.Console.Iterators;
using InteractiveMap.Console.Mediation;
using InteractiveMap.Console.Memento;
using InteractiveMap.Console.Models;
using InteractiveMap.Console.Observer;
using InteractiveMap.Console.Proxies;
using InteractiveMap.Console.Services;
using InteractiveMap.Console.State;
using InteractiveMap.Console.Strategy;
using InteractiveMap.Console.Validation;

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

void Lab4()
{

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
}

void Lab5()
{
    System.Console.WriteLine("===== Фасад: единая точка входа =====\n");

    MapFacade facade = new();

    facade.AddTouristPoi("Центральный парк", 55.7601, 37.6184);
    facade.AddTouristPoi("Музей истории", 55.7580, 37.6150);
    facade.AddFoodPoi("Кофейня на углу", 55.7612, 37.6205);

    System.Console.WriteLine($"Всего точек: {facade.TotalCount()}\n");

    System.Console.WriteLine("--- Рендер туристической карты в консоль ---");
    facade.RenderTouristConsole();

    System.Console.WriteLine("\n--- Рендер гастрономической карты в графическое окно ---");
    facade.RenderFoodGraphics();

    System.Console.WriteLine("\n\n===== Приспособленец: разделение категорий =====\n");

    List<MapPoint> points = new()
{
    new("Парк Горького", 55.7300, 37.6010, CategoryFactory.Get("park")),
    new("Сокольники", 55.7940, 37.6760, CategoryFactory.Get("park")),
    new("Кофейня №1",  55.7612, 37.6205, CategoryFactory.Get("cafe")),
    new("Кофейня №2", 55.7655, 37.6100, CategoryFactory.Get("cafe")),
    new("Кофейня №3",  55.7500, 37.6300, CategoryFactory.Get("cafe")),
    new("Музей истории",  55.7580, 37.6150, CategoryFactory.Get("museum")),
    new("Пушкинский музей",55.7350, 37.6050, CategoryFactory.Get("museum")),
    new("Книжный магазин", 55.7595, 37.6170, CategoryFactory.Get("shop")),
};

    foreach (MapPoint p in points)
    {
        System.Console.WriteLine(p.Describe());
    }

    System.Console.WriteLine($"\nТочек создано:          {points.Count}");
    System.Console.WriteLine($"Объектов Category:      {CategoryFactory.CacheSize}  (вместо {points.Count})");
    System.Console.WriteLine($"Экономия по категориям: {points.Count - CategoryFactory.CacheSize} объектов");
}

void Lab6()
{

    // Заполняем реестр через MapService (Singleton из Lab1).
    MapService service = MapService.Instance;

    PointOfInterest park = service.AddPoi(new PointOfInterest { Name = "Центральный парк", Coordinate = new Coordinate(55.7601, 37.6184) });
    PointOfInterest museum = service.AddPoi(new PointOfInterest { Name = "Музей истории", Coordinate = new Coordinate(55.7580, 37.6150) });
    PointOfInterest cafe = service.AddPoi(new PointOfInterest { Name = "Кофейня на углу", Coordinate = new Coordinate(55.7612, 37.6205) });

    System.Console.WriteLine("===== Заместитель (Caching Proxy) =====\n");

    // 1. Работаем напрямую через RealSubject (без кэша).
    IDistanceCalculator realOnly = service;
    System.Console.WriteLine("--- Без прокси ---");
    {
        double d = realOnly.GetDistance(park.Id, museum.Id);
        System.Console.WriteLine($"{d:0.0000} км");
    }

    // 2. Работаем через CachingProxy.
    IDistanceCalculator proxy = new CachingDistanceProxy(service);
    System.Console.WriteLine("\n--- С прокси ---");
    {
        double d = proxy.GetDistance(park.Id, museum.Id);
        System.Console.WriteLine($"{d:0.0000} км");
    }

    // 3. Симметрия: (a,b) и (b,a) должны дать один и тот же кэш-ключ.
    System.Console.WriteLine("\n--- Симметрия: (park, museum) и (museum, park) ---");
    double ab = proxy.GetDistance(park.Id, museum.Id);
    double ba = proxy.GetDistance(museum.Id, park.Id);
    System.Console.WriteLine($"  (park, museum) = {ab:0.0000}");
    System.Console.WriteLine($"  (museum, park) = {ba:0.0000}");

    // 4. Разные пары — промахи кэша.
    System.Console.WriteLine("\n--- Добавляем новую пару (park, cafe) ---");
    double pc = proxy.GetDistance(park.Id, cafe.Id);
    System.Console.WriteLine($"  (park, cafe) = {pc:0.0000}");

    // 5. Итоговая статистика.
    CachingDistanceProxy stats = (CachingDistanceProxy)proxy;
    System.Console.WriteLine("\n===== Статистика прокси =====");
    System.Console.WriteLine($"  попаданий в кэш (hits):  {stats.Hits}");
    System.Console.WriteLine($"  промахов (misses):       {stats.Misses}");
    System.Console.WriteLine($"  размер кэша:             {stats.CacheSize}");
}

void Lab7()
{

    CommandInvoker invoker = new();

    System.Console.WriteLine("===== Команда: журнал операций с Undo =====\n");

    // Выполняем команды через Invoker.
    invoker.Execute(new AddPoiCommand(mapService,
        new PointOfInterest { Name = "Центральный парк", Category = "Парк", Coordinate = new Coordinate(55.7601, 37.6184) }));

    invoker.Execute(new AddPoiCommand(mapService,
        new PointOfInterest { Name = "Музей истории", Category = "Музей", Coordinate = new Coordinate(55.7580, 37.6150) }));

    invoker.Execute(new AddPoiCommand(mapService,
        new PointOfInterest { Name = "Кофейня на углу", Category = "Кафе", Coordinate = new Coordinate(55.7612, 37.6205) }));

    System.Console.WriteLine($"После 3 добавлений (всего точек в реестре: {mapService.All.Count}):");
    foreach (var p in mapService.All) System.Console.WriteLine("  " + p);

    // Отменяем последнее действие.
    System.Console.WriteLine("\n--- UndoLast() ---");
    invoker.UndoLast();

    System.Console.WriteLine($"После отмены (осталось точек: {mapService.All.Count}):");
    foreach (var p in mapService.All) System.Console.WriteLine("  " + p);

    // Удаление с откатом.
    System.Console.WriteLine("\n--- Команда RemovePoiCommand для #1 + Undo ---");
    invoker.Execute(new RemovePoiCommand(mapService, poiId: 1));
    System.Console.WriteLine($"После удаления #1 (всего: {mapService.All.Count}):");
    foreach (var p in mapService.All) System.Console.WriteLine("  " + p);

    invoker.UndoLast();
    System.Console.WriteLine($"После отмены удаления (всего: {mapService.All.Count}):");
    foreach (var p in mapService.All) System.Console.WriteLine("  " + p);

    // ==================== ITERATOR ====================
    System.Console.WriteLine("\n\n===== Итератор: обход с фильтром по категории =====\n");

    // Обход всех.
    System.Console.WriteLine("--- Все точки (без фильтра) ---");
    IPoiIterator allIterator = mapService.CreateIterator();
    while (allIterator.HasNext())
    {
        System.Console.WriteLine("  " + allIterator.Next().Name);
    }

    // Обход только категории "Музей".
    System.Console.WriteLine("\n--- Только категория \"Музей\" ---");
    IPoiIterator museumIterator = mapService.CreateIterator("Музей");
    while (museumIterator.HasNext())
    {
        System.Console.WriteLine("  " + museumIterator.Next().Name);
    }

    // Обход несуществующей категории — итератор сразу пуст.
    System.Console.WriteLine("\n--- Категория \"Ресторан\" (нет таких) ---");
    IPoiIterator noneIterator = mapService.CreateIterator("Ресторан");
    System.Console.WriteLine($"  HasNext в начале: {noneIterator.HasNext()}");
}

void Lab8()
{

    System.Console.WriteLine("===== Цепочка обязанностей: валидация точек =====\n");

    PoiValidationPipeline pipeline = new();

    // Кейс 1: корректная точка.
    PointOfInterest ok = new()
    {
        Name = "Центральный парк",
        Coordinate = new Coordinate(55.7601, 37.6184),
        Category = "Парк",
        Rating = 4.5,
        OpeningHours = "круглосуточно"
    };
    System.Console.WriteLine($"1) Корректная точка      -> {pipeline.Validate(ok)}");

    // Кейс 2: пустое имя — ошибка NameValidator (первый в цепочке).
    PointOfInterest badName = new()
    {
        Name = "   ",
        Coordinate = new Coordinate(55.7601, 37.6184),
        Rating = 4.5
    };
    System.Console.WriteLine($"2) Пустое имя            -> {pipeline.Validate(badName)}");

    // Кейс 3: отрицательный рейтинг — ошибка RatingValidator.
    PointOfInterest badRating = new()
    {
        Name = "Кофейня",
        Coordinate = new Coordinate(55.7612, 37.6205),
        Rating = 9.9
    };
    System.Console.WriteLine($"3) Рейтинг вне диапазона -> {pipeline.Validate(badRating)}");

    // Кейс 4: пустая категория — поймает CategoryValidator (последний).
    PointOfInterest badCategory = new()
    {
        Name = "Музей",
        Coordinate = new Coordinate(55.7580, 37.6150),
        Category = "   ",
        Rating = 4.8
    };
    System.Console.WriteLine($"4) Пустая категория      -> {pipeline.Validate(badCategory)}");

    System.Console.WriteLine("\n===== Конец демонстрации =====");
}

void Lab9()
{

    // ==================== MEDIATOR ====================
    System.Console.WriteLine("===== Посредник: централизация взаимодействия =====\n");

    MapMediator mediator = new(new ConsoleRenderer());
    MapPanel panel = new();
    panel.SetMediator(mediator);

    // Панель не знает о MapService / валидаторе / рендерере — только о посреднике.
    panel.RequestAdd("Центральный парк", 55.7601, 37.6184, "Парк", 4.7);
    panel.RequestAdd("Музей истории", 55.7580, 37.6150, "Музей", 4.9);

    // Некорректный ввод — отклонён цепочкой валидаторов (Lab8) через посредника.
    panel.RequestAdd("   ", 55.7601, 37.6184, "Парк", 4.7); // пустое имя

    panel.RequestRender();

    // ==================== MEMENTO ====================
    System.Console.WriteLine("\n\n===== Снимок: сохранение и откат состояния =====\n");

    PoiOriginator originator = new("Кофейня на углу");
    PoiHistoryCaretaker caretaker = new();

    System.Console.WriteLine("Начальное состояние:");
    System.Console.WriteLine("  " + originator.Describe());

    // Сохраняем снимок перед изменением.
    caretaker.Save(originator.Save());

    // Редактируем.
    originator.Edit("Кофейня на углу (обновлено)", "Кафе", 4.5, "08:00–22:00");
    System.Console.WriteLine("\nПосле первого редактирования:");
    System.Console.WriteLine("  " + originator.Describe());

    // Ещё раз сохраняем и редактируем.
    caretaker.Save(originator.Save());
    originator.Edit("Совсем другое имя", "Ресторан", 3.0, "12:00–23:00");
    System.Console.WriteLine("\nПосле второго редактирования:");
    System.Console.WriteLine("  " + originator.Describe());

    // Откатываем последнее изменение.
    PoiMemento? last = caretaker.Undo();
    if (last is not null)
    {
        originator.Restore(last);
    }
    System.Console.WriteLine("\nПосле Undo #1:");
    System.Console.WriteLine("  " + originator.Describe());

    // Откатываем ещё раз.
    last = caretaker.Undo();
    if (last is not null)
    {
        originator.Restore(last);
    }
    System.Console.WriteLine("\nПосле Undo #2 (вернулись к началу):");
    System.Console.WriteLine("  " + originator.Describe());
}

void Lab10()
{
    // ==================== OBSERVER ====================
    System.Console.WriteLine("===== Наблюдатель: подписка на реестр =====\n");

    MapService service = MapService.Instance;

    // Подписываем двух наблюдателей.
    LoggingObserver logger = new();
    CountingObserver counter = new();
    service.Attach(logger);
    service.Attach(counter);

    // Операции над реестром — оба наблюдателя реагируют.
    PointOfInterest park = service.AddPoi(new PointOfInterest
    {
        Name = "Центральный парк",
        Coordinate = new Coordinate(55.7601, 37.6184),
        Category = "Парк"
    });

    PointOfInterest museum = service.AddPoi(new PointOfInterest
    {
        Name = "Музей истории",
        Coordinate = new Coordinate(55.7580, 37.6150),
        Category = "Музей"
    });

    service.RemovePoi(park.Id);

    System.Console.WriteLine($"\nСтатистика CountingObserver: добавлено {counter.Added}, удалено {counter.Removed}");

    // Отписываем логгер — дальнейшие события его не касаются.
    System.Console.WriteLine("\n--- Detach(logger) ---");
    service.Detach(logger);

    service.AddPoi(new PointOfInterest
    {
        Name = "Кофейня на углу",
        Coordinate = new Coordinate(55.7612, 37.6205),
        Category = "Кафе"
    });

    System.Console.WriteLine($"\nИтог: добавлено {counter.Added}, удалено {counter.Removed}");

    // ==================== STATE ====================
    System.Console.WriteLine("\n\n===== Состояние: жизненный цикл точки =====\n");

    PoiContext poi = new("Кофейня на углу");
    System.Console.WriteLine(poi.Describe());

    // В Draft можно редактировать.
    poi.Edit("Кофейня на углу (уточнено)");
    System.Console.WriteLine(poi.Describe());

    // Публикуем — переход в Published.
    poi.Publish();
    System.Console.WriteLine(poi.Describe());

    // В Published редактирование запрещено.
    poi.Edit("Попытка изменить");
    System.Console.WriteLine(poi.Describe());

    // Архивируем — переход в Archived.
    poi.Archive();
    System.Console.WriteLine(poi.Describe());

    // Из архива можно вернуться в Draft.
    poi.ResetToDraft();
    System.Console.WriteLine(poi.Describe());

    // Снова можно редактировать.
    poi.Edit("Снова черновик");
    System.Console.WriteLine(poi.Describe());
}

PointOfInterest park = mapService.AddPoi(new PointOfInterest
{
    Name = "Центральный парк",
    Coordinate = new Coordinate(55.7601, 37.6184),
    Category = "Парк"
});

PointOfInterest museum = mapService.AddPoi(new PointOfInterest
{
    Name = "Музей истории",
    Coordinate = new Coordinate(55.7580, 37.6150),
    Category = "Музей"
});

// ==================== STRATEGY ====================
System.Console.WriteLine("===== Стратегия: взаимозаменяемые алгоритмы =====\n");

NavigationService navigation = new(mapService, new HaversineStrategy());
System.Console.WriteLine($"Стратегия: {navigation.CurrentStrategy}");
System.Console.WriteLine($"Расстояние парк -> музей: {navigation.Distance(park.Id, museum.Id):0.0000} км");

// Меняем стратегию в работающем прилоожении - Context не меняется.
navigation.SetStrategy(new ManhattanStrategy());
System.Console.WriteLine($"\nСтратегия: {navigation.CurrentStrategy}");
System.Console.WriteLine($"Расстояние парк -> музей: {navigation.Distance(park.Id, museum.Id):0.0000} км");

// ==================== TEMPLATE METHOD ====================
System.Console.WriteLine("\n\n===== Шаблонный метод: единый скелет экспорта =====\n");

MapExporter txt = new TxtExporter();
System.Console.WriteLine("--- TXT ---");
System.Console.WriteLine(txt.Export(mapService.All));

MapExporter csv = new CsvExporter();
System.Console.WriteLine("\n--- CSV ---");
System.Console.WriteLine(csv.Export(mapService.All));

MapExporter json = new JsonExporter();
System.Console.WriteLine("\n--- JSON ---");
System.Console.WriteLine(json.Export(mapService.All));