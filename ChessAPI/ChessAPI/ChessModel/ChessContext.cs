using Microsoft.EntityFrameworkCore;

namespace ChessAPI.ChessModel
{
    public class ChessContext : DbContext
    {
        public ChessContext(DbContextOptions<ChessContext> options)
            : base(options)
        {
        }

        public DbSet<ChessPiece> ChessPieces { get; set; } = null!;
        public DbSet<ChessGame> ChessGames { get; set; } = null!;

    }
}
