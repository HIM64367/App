using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using System.Net.NetworkInformation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;

namespace ChessAPI.ChessModel
{
    public class ChessPiece
    {
        [Key]
        [JsonIgnore]
        public int PieceId { get; set; }
        public string? Id { get; set; }
        public string? Name { get; set; }
        public string? ImageUrl { get; set; }
        public string? Position { get; set; }
        public bool IsFirstMove { get; set; } = true;

        
    }

    public class ChessGame
    {
        [Key]
        public int GameId { get; set; }
        public string? Turn { get; set; }
        public string GameState { get; set; } = "Normal";
        public List<ChessPiece>? Pieces { get; set; }
    }

    public class ChessMove
    {
        public string? OnePiece { get; set; }
        public string? Destination { get; set; }
    }
}