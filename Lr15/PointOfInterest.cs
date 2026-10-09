using System;
using System.Collections.Generic;
using System.Text;

namespace Lr15;

/// <summary>
/// Доменная модель: точка интереса на карте.
/// </summary>
public sealed class PointOfInterest
{
    public string Name { get; init; } = "";
    public double Latitude { get; init; }
    public double Longitude { get; init; }

    /// <summary>
    /// Рейтинг, который вычисляется асинхронно (консьюмером).
    /// До обработки — 0.
    /// </summary>
    public double Rating { get; set; }
}