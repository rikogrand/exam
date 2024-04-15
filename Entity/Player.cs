using System;
using System.IO;
using Avalonia.Controls;
using Avalonia.Media.Imaging;

namespace SuperVolleyball.Entity;

public class Player
{
    public int ID { get; set; }
    public string Name { get; set; }
    public string Position { get; set; }
    public int Weight { get; set; }
    public int Height { get; set; }
    public DateTime DateOfBirth { get; set; }
    public DateTime DateOfStartMatch { get; set; }
    public string Team { get; set; }
    
    }
