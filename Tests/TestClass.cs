using System;
using System.Diagnostics;
using System.IO;
using System.Threading;
using FakeItEasy;
using FluentAssertions;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Windows;

namespace Tests;

[TestClass]
public class TestClass
{
    [TestMethod]
    public void ReproTest()
    {
        StartWinAppDriver();
        var appiumOptions = CreateAppiumOptions();
        try
        {
            new WindowsDriver<WindowsElement>(new Uri("http://127.0.0.1:4723"), appiumOptions);
        }
        catch
        {
            Trace.WriteLine("Expecting failure.");
        }
    }

    /// <summary>
    /// Starts WinAppDriver if it is not already running.
    /// </summary>
    private static void StartWinAppDriver()
    {
        const string wadProcessName = "WinAppDriver";
        if (Process.GetProcessesByName(wadProcessName).Length > 0)
        {
            return;
        }
        var wadPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86), "Windows Application Driver", wadProcessName + ".exe");
        var startInfo = new ProcessStartInfo();
        var fileInfo = new FileInfo(wadPath);
        startInfo.WorkingDirectory = fileInfo.DirectoryName;
        startInfo.FileName = wadPath;
        startInfo.UseShellExecute = true;
        startInfo.CreateNoWindow = false;
        startInfo.WindowStyle = ProcessWindowStyle.Minimized;
        Process.Start(startInfo);
    }

    private static AppiumOptions CreateAppiumOptions()
    {
        var appPath = Path.Combine(Environment.CurrentDirectory, "App.exe");
        var appiumOptions = new AppiumOptions();
        appiumOptions.AddAdditionalCapability("app", appPath);
        appiumOptions.AddAdditionalCapability("deviceName", "WindowsPC");
        appiumOptions.AddAdditionalCapability("platformName", "Windows");
        appiumOptions.AddAdditionalCapability("platformVersion", "10");
        return appiumOptions;
    }
}
