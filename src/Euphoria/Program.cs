﻿using System;
using System.Drawing;
using u4.Core;
using u4.Engine;

Logger.AttachConsole();

Logger.Trace("Trace message.");
Logger.Debug("Debug message.");
Logger.Info("Info message.");
Logger.Warn("Warning message");
Logger.Error("Error message.");
Logger.Fatal("Fatal message.");

LaunchOptions options = new LaunchOptions("Test", new Version(1, 0));
App.Run(options);