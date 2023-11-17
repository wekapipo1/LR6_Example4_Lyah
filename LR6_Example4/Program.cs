using System;
class TrafficLight
{
    //делегат, відповідаючий всим обробникам
    public delegate void Hadler();
    //опис подій СТАТИЧНИХ
    public static event Hadler Event1; //подія горить зелений
    public static event Hadler Event2; //подія горить червоний
    public void ChangeLight (int i)
    {
        if (i%2 == 0)
        { Event2(); } //генеруємо подію, горить червоний
        else
        { Event1(); } //генеруємо подію, горить зелений
    }
    public void Red() //обробник події горить червоний
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("Горить червоний");
    }
    public void Green() //обробник події горить зелений
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("Горить зелений");
    }
}
class Driver
{
    string name;
    public Driver() { }
    public Driver(string name)
    {
        this.name = name;
    }
    public void Ride() //обробник події горить зелений
    {
        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine("{0} їду", name);
        //ВІДПИСУЄМОСЬ ВІД ПОДІЙ
        //зліва від = клас та подія, зправа об'єкт this і метод
        TrafficLight.Event1 -= this.Ride;
        TrafficLight.Event2 -= this.Stand;
    }
    public void Stand()
    {
        Console.ForegroundColor= ConsoleColor.White;
        Console.WriteLine("{0} стою", name);
    }
}
class Pedestrian
{
    string name;
    public Pedestrian() { }
    public Pedestrian(string name)
    {
        this.name = name;
    }
    public void Go() //обробник події горить червоний
    {
        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine("{0} йду", name);
        //ВІДПИСУЄМОСЬ ВІД ПОДІЙ
        //зліва від = клас та подія, зправа об'єкт this і метод
        TrafficLight.Event1 -= this.Stand;
        TrafficLight.Event2 -= this.Go;
    }
    public void Stand() //обробник події горить зелений
    {
        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine("{0} стою", name);
    }
}
class Program
{
    static void Main(string[] args)
    {
        //об'єкт світлофор
        TrafficLight svet = new TrafficLight();
        //підписка об'єкту svet на подію Event1
        //обробник події називається Green
        TrafficLight.Event1 += svet.Green;
        //підписка об'єкту svet на подію Event2
        //обробник події називається Red
        TrafficLight.Event2 += svet.Red;
        for (int i=1; i<=5; i++)
        {
            //об'єкт і-й пішохід
            Pedestrian pesh =new Pedestrian("Пiшохiд " + i);
            //об'єкт і-й водій
            Driver vod = new Driver("Водiй " + i);
            //підписка пішохода й водія на 1 подію
            TrafficLight.Event1 += pesh.Stand;
            TrafficLight.Event1 += vod.Ride;
            //підписка пішохода й водія на 2 подію
            TrafficLight.Event2 += pesh.Go;
            TrafficLight.Event2 += vod.Stand;
            svet.ChangeLight(i);
        }
        Console.ReadKey();
    }
}