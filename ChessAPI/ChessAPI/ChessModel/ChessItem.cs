﻿using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using System.Net.NetworkInformation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ChessAPI.ChessModel
{
    public class ChessPiece
    {
        public string? Id { get; set; }
        public string? Name { get; set; }
        public string? ImageUrl { get; set; }
        public string? Position { get; set; }
    }

    public class ChessGame
    {
        public int Id { get; set; }
        public string? Turn { get; set; }
        public List<ChessPiece>? Pieces { get; set; }

    }

    public class ChessMove
    {
        public string? OnePiece { get; set; }
        public string? Destination { get; set; }
    }

}