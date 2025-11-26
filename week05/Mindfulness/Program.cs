using System;


class Program
{
static void Main()
{
while (true)
{
Console.WriteLine("Mindfulness Program Menu:");
Console.WriteLine("1. Breathing Activity");
Console.WriteLine("2. Reflection Activity");
Console.WriteLine("3. Listing Activity");
Console.WriteLine("4. Exit");
Console.Write("Choose an option: ");


string choice = Console.ReadLine();
if (choice == "4") break;


Console.Write("Enter duration in seconds: ");
int duration = int.Parse(Console.ReadLine());


switch (choice)
{
case "1":
var breathing = new BreathingActivity();
breathing.SetDuration(duration);
breathing.Perform();
break;
case "2":
var reflection = new ReflectionActivity();
reflection.SetDuration(duration);
reflection.Perform();
break;
case "3":
var listing = new ListingActivity();
listing.SetDuration(duration);
listing.Perform();
break;
}
}
}
}