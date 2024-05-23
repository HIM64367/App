using Microsoft.EntityFrameworkCore;
using System.Net.NetworkInformation;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ChessAPI.ChessModel
{

    public class ChessGame
    {
        [Key]
        public int GameId { get; set; }
        public string? Turn { get; set; }
        public string GameState { get; set; } = "Normal";
        public List<ChessPiece>? Pieces { get; set; }
    }

    public class ChessPiece
    {
        [Key]
        [JsonIgnore]
        public int PieceId { get; set; }
        [Required]
        public string Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        [Required]
        public string Position { get; set; }
        public bool IsFirstMove { get; set; } = true;

    }

    public class ChessMove
    {
        public string? OnePiece { get; set; }
        public string? Destination { get; set; }
    }
}