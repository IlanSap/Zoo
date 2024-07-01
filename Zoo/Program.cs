﻿using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ZooProject.Data;

namespace ZooProject;

class Program {
    static void Main(string[] args) {
        // Setup dependency injection
        var services = new ServiceCollection();
        ConfigureServices(services);

        var serviceProvider = services.BuildServiceProvider();

        try {
            // Using a scope to resolve scoped services
            using (var scope = serviceProvider.CreateScope()) {
                var consoleHelper = scope.ServiceProvider.GetRequiredService<ConsoleHelper>();


                ZooManager zooManager = scope.ServiceProvider.GetRequiredService<ZooManager>();
                if (consoleHelper.GetRunType() == 1) {
                    zooManager.Run();
                } else {
                    zooManager.RunWithComposite();
                }
            }
        } catch (Exception ex) {
            Console.WriteLine($"An error occurred: {ex.Message}");
        }
    }

    public static void ConfigureServices(IServiceCollection services) {
        services.AddDbContext<ZooContext>(options =>
            options.UseSqlServer(
                @"Server=localhost\SQLEXPRESS;Database=ZooDB;Trusted_Connection=True; Encrypt=True; TrustServerCertificate=True;"));
        services.AddTransient<ZooService>();
        services.AddSingleton<ConsoleHelper>();
        services.AddTransient<ZooManager>();
    }
}