// See https://aka.ms/new-console-template for more information
using System;
using System.Numerics;

struct Point
{
    public float x;
    public float y;

    public Point(float x, float y)
    {
        this.x = x;
        this.y = y;
    }

    public static double GetDistance(Point p0, Point p1)
    {
        return Math.Sqrt(Math.Pow(p1.x - p0.x, 2) + Math.Pow(p1.y - p0.y, 2));
    }
    public static double GetDistanceSqrt(Point p0, Point p1)
    {
        return Math.Pow(p1.x - p0.x, 2) + Math.Pow(p1.y - p0.y, 2);
    }

    public override string ToString()
    {
        return "X = " + x.ToString() + " , " + "Y = " + y.ToString();
    }
}
class Order
{
    public string name;
    public Point pointA;
    public Point pointB;
    public int cost;
    public double distanceSqrtToCourier;

    public static readonly List<string> ordersNames = [
        "Bouquet of roses",
        "Сhocolate",
        "Present",
        "Bouquet of lilies",
        "a box of chocolate",
        "a toy",
        "Bouquet of flowers",
        "A teddy bear",
        "A toy bunny",
        "Doll"];

    public override string ToString()
    {
        return "'" + name + "' " + "the distance to the order: " + Math.Sqrt(distanceSqrtToCourier).ToString();
    }
}
class Courier
{
    public string name;
    public Order orderTarget;
    public Point position;

    public List<Order> orders = new();

    public static List<string> courierNames = [
        "Alexey",
        "Vladimir",
        "Sergey",
        "Nicolay",
        "Alexandr",
        "Georgy",
        "Dmitry",
        "Andrey",
        "Semyon",
        "Victor"];

    public override string ToString()
    {
        return "Courier " + name + ", " + "Closest Order: " + orderTarget.ToString();
    }
}

struct RandomNumber
{
    public static float RandNum()
    {
        Random random = new();
        float rand = random.Next(0, 100);

        return rand;
    }
}

class Program
{
    static List<Order> orders = new();
    static List<Courier> couriers = new();

    static readonly int orderCount = 10;
    static readonly int courierCount = 10;
    static void Main(string[] args)
    {
        for (int i = 0; i < orderCount; i++)
        {
            Order order = new();
            order.name = Order.ordersNames[i];
            order.pointA = new Point(RandomNumber.RandNum(), RandomNumber.RandNum());
            order.pointB = new Point(RandomNumber.RandNum(), RandomNumber.RandNum());
            order.cost = (int)RandomNumber.RandNum();

            orders.Add(order);
        }
        for (int i = 0; i < courierCount; i++)
        {
            Courier courier = new();
            courier.name = Courier.courierNames[i];
            courier.position = new Point(RandomNumber.RandNum(), RandomNumber.RandNum());

            couriers.Add(courier);
        }

        foreach (var courier in couriers)
        {
            Point courierPoint = courier.position;

            foreach (var order in orders)
            {
                Point orderPointA = order.pointA;

                double distance = Point.GetDistanceSqrt(orderPointA, courierPoint);

                order.distanceSqrtToCourier = distance;

                courier.orders.Add(order);                
            }

            Order temp;

            for (int i = 0; i < courier.orders.Count; i++)
            {
                for (int j = 0; j < courier.orders.Count - 1 - i; j++)
                {
                    if (courier.orders[j].distanceSqrtToCourier > courier.orders[j + 1].distanceSqrtToCourier)
                    {
                        temp = courier.orders[j];
                        courier.orders[j] = courier.orders[j + 1];
                        courier.orders[j + 1] = temp;
                    }
                }
            }

            courier.orderTarget = courier.orders[0];

            orders.Remove(courier.orders[0]);
        }

        for (int i = 0; i < couriers.Count; i++)
        {
            Console.WriteLine(couriers[i].ToString());
        }
    }
}

