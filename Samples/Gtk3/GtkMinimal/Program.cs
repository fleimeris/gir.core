﻿using System;
using Gtk.Core;

namespace GtkDemo
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var app = new Program();
        }

        public Program()
        {
            var app = new GApplication("org.gircore.minimal");
            app.Activate += OnActivate;
            app.Run();
        }

        private void OnActivate(object? sender, EventArgs args)
        {
            if(sender is GApplication app)
            {
                var w = new DemoWindow(app);
                w.DefaultHeight.Value = 600;
                w.DefaultWidth.Value = 800;
                w.ShowAll();
            }
        }
    }
}