using System;

namespace TestPurposes.Patterns.AbstractFactory
{
    #region products

    public abstract class Furniture { public virtual string Type { get; init; } = "Мебель"; }

    public class Chair : Furniture { public Chair() => Type = "Стул"; }
    public class ModernChair : Chair { public ModernChair() => Type = "Modern стул"; }
    public class VenecianceChair : Chair { public VenecianceChair() => Type = "Veneciance стул"; }

    public class Table : Furniture { public Table() => Type = "Стол"; }
    public class ModernTable : Table { public ModernTable() => Type = "Modern стол"; }
    public class VenecianceTable : Table { public VenecianceTable() => Type = "Veneciance стол"; }

    #endregion

    #region factories

    public abstract class FurnitureAbstractFactory
    {
        public abstract Chair CreateChair();
        public abstract Table CreateTable();
    }

    public class ModernFurnitureFactory : FurnitureAbstractFactory
    {
        public override Chair CreateChair() => new ModernChair();
        public override Table CreateTable() => new ModernTable();
    }

    public class VenecianceFurnitureFactory : FurnitureAbstractFactory
    {
        public override Chair CreateChair() => new VenecianceChair();
        public override Table CreateTable() => new VenecianceTable();
    }

    #endregion

    #region room things

    public class Room
    {
        public Chair Chair { get; init; }
        public Table Table { get; init; }

        // немножко нарушаем SRP и OCP (помни: не суди да не судим будешь)
        public void WhatInRoom() => Console.WriteLine($"В комнате: \n\tСтул: \"{Chair.Type}\"\n\tСтол: \"{Table.Type}\"\n");
    }

    public static class RoomBuilder
    {
        public static Room Build(FurnitureAbstractFactory factory) =>
            // абстрактная фабрика раскрывается как раз тут
            new() { Chair = factory.CreateChair(), Table = factory.CreateTable() };
    }

    #endregion

    public static class AbstractFactoryTester
    {
        public static void DealWithModernRoom()
        {
            FurnitureAbstractFactory modernFactory = new ModernFurnitureFactory();
            Room modernRoom = RoomBuilder.Build(modernFactory);

            Console.WriteLine("modern room");
            modernRoom.WhatInRoom();
        }

        public static void DealWithVenecianceRoom()
        {
            FurnitureAbstractFactory venecianceFactory = new VenecianceFurnitureFactory();
            Room venecianceRoom = RoomBuilder.Build(venecianceFactory);

            Console.WriteLine("veneciance room");
            venecianceRoom.WhatInRoom();
        }

        public static void Main()
        {
            DealWithModernRoom();
            DealWithVenecianceRoom();
        }
    }
}
