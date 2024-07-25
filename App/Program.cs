using System;
using FakeItEasy;

Console.WriteLine("Creating fake.");
_ = A.Fake<IConvertible>();
Console.WriteLine("Fake created.");
Console.WriteLine("Press any key to exit.");
Console.ReadKey();
