using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ChessAPI.ChessModel;
using System.ComponentModel.DataAnnotations;

namespace ChessAPI.Controllers
{

    [ApiController]
    [Route("api/chess")]
    public class ChessController : ControllerBase
    {

        private readonly ChessContext _context;

        public ChessController(ChessContext context)
        {
            _context = context;
        }


        // POST: api/chess/Start
        [HttpPost("Start")]
        public ActionResult<ChessGame> StartGame()
        {
            var game = new ChessGame
            {
                Turn = "White",
                Pieces = new List<ChessPiece>
                {

                new ChessPiece { Id = "white_rook_1", Name = "rook", ImageUrl = "http://chess.com/rook.png", Position = "a1" },
                new ChessPiece { Id = "white_knight_1", Name = "knight", ImageUrl = "http://chess.com/knight.png", Position = "b1" },
                new ChessPiece { Id = "white_bishop_1", Name = "bishop", ImageUrl = "http://chess.com/bishop.png", Position = "c1" },
                new ChessPiece { Id = "white_queen", Name = "queen", ImageUrl = "http://chess.com/queen.png", Position = "d1" },
                new ChessPiece { Id = "white_king", Name = "king", ImageUrl = "http://chess.com/king.png", Position = "e1" },
                new ChessPiece { Id = "white_bishop_2", Name = "bishop", ImageUrl = "http://chess.com/bishop.png", Position = "f1" },
                new ChessPiece { Id = "white_knight_2", Name = "knight", ImageUrl = "http://chess.com/knight.png", Position = "g1" },
                new ChessPiece { Id = "white_rook_2", Name = "rook", ImageUrl = "http://chess.com/rook.png", Position = "h1" },

                new ChessPiece { Id = "white_pawn_1", Name = "pawn", ImageUrl = "http://chess.com/pawn.png", Position = "a2" },
                new ChessPiece { Id = "white_pawn_2", Name = "pawn", ImageUrl = "http://chess.com/pawn.png", Position = "b2" },
                new ChessPiece { Id = "white_pawn_3", Name = "pawn", ImageUrl = "http://chess.com/pawn.png", Position = "c2" },
                new ChessPiece { Id = "white_pawn_4", Name = "pawn", ImageUrl = "http://chess.com/pawn.png", Position = "d2" },
                new ChessPiece { Id = "white_pawn_5", Name = "pawn", ImageUrl = "http://chess.com/pawn.png", Position = "e2" },
                new ChessPiece { Id = "white_pawn_6", Name = "pawn", ImageUrl = "http://chess.com/pawn.png", Position = "f2" },
                new ChessPiece { Id = "white_pawn_7", Name = "pawn", ImageUrl = "http://chess.com/pawn.png", Position = "g2" },
                new ChessPiece { Id = "white_pawn_8", Name = "pawn", ImageUrl = "http://chess.com/pawn.png", Position = "h2" },



                new ChessPiece { Id = "black_rook_1", Name = "rook", ImageUrl = "http://chess.com/rook.png", Position = "a8" },
                new ChessPiece { Id = "black_knight_1", Name = "knight", ImageUrl = "http://chess.com/knight.png", Position = "b8" },
                new ChessPiece { Id = "black_bishop_1", Name = "bishop", ImageUrl = "http://chess.com/bishop.png", Position = "c8" },
                new ChessPiece { Id = "black_queen", Name = "queen", ImageUrl = "http://chess.com/queen.png", Position = "d8" },
                new ChessPiece { Id = "black_king", Name = "king", ImageUrl = "http://chess.com/king.png", Position = "e8" },
                new ChessPiece { Id = "black_bishop_2", Name = "bishop", ImageUrl = "http://chess.com/bishop.png", Position = "f8" },
                new ChessPiece { Id = "black_knight_2", Name = "knight", ImageUrl = "http://chess.com/knight.png", Position = "g8" },
                new ChessPiece { Id = "black_rook_2", Name = "rook", ImageUrl = "http://chess.com/rook.png", Position = "h8" },

                new ChessPiece { Id = "black_pawn_1", Name = "pawn", ImageUrl = "http://chess.com/pawn.png", Position = "a7" },
                new ChessPiece { Id = "black_pawn_2", Name = "pawn", ImageUrl = "http://chess.com/pawn.png", Position = "b7" },
                new ChessPiece { Id = "black_pawn_3", Name = "pawn", ImageUrl = "http://chess.com/pawn.png", Position = "c7" },
                new ChessPiece { Id = "black_pawn_4", Name = "pawn", ImageUrl = "http://chess.com/pawn.png", Position = "d7" },
                new ChessPiece { Id = "black_pawn_5", Name = "pawn", ImageUrl = "http://chess.com/pawn.png", Position = "e7" },
                new ChessPiece { Id = "black_pawn_6", Name = "pawn", ImageUrl = "http://chess.com/pawn.png", Position = "f7" },
                new ChessPiece { Id = "black_pawn_7", Name = "pawn", ImageUrl = "http://chess.com/pawn.png", Position = "g7" },
                new ChessPiece { Id = "black_pawn_8", Name = "pawn", ImageUrl = "http://chess.com/pawn.png", Position = "h7" },
                }


            };

            _context.ChessGames.AddAsync(game);
            _context.SaveChangesAsync();
            return game;

        }

        // GET: api/chess/games/1
        [HttpGet("games/{id}")]
        public ActionResult<ChessGame> GetGame([Required] int id)
        {
            var game = _context.ChessGames.Include(g => g.Pieces).FirstOrDefault(g => g.Id == id);

            if (game == null)
            {
                return NotFound();
            }

            return game;
        }


        //PATCH: api/chess/games/1/
        [HttpPatch("games/{id}")]
        public IActionResult PatchGame(int id, [FromBody] ChessMove move)
        {

            var game = _context.ChessGames.Include(g => g.Pieces).FirstOrDefault(g => g.Id == id);
            if (game == null)
            {
                return NotFound();
            }


            var piece = game.Pieces.FirstOrDefault(p => p.Id == move.OnePiece);
            if (piece == null)
            {
                return BadRequest("Il pezzo specificato non esiste.");
            }


            string[] idParts = piece.Id.Split('_');
            string color = idParts[0];
            string name = idParts[1];
            string number = idParts[2];


            if (game.Turn.ToLower() == color)
            {
                if (IsValidMove(piece.Position, move.Destination, move))
                {


                    if (PieceExist(move.Destination))
                    {

                        var pieceToEat = _context.ChessPieces.FirstOrDefault(p => p.Position == move.Destination);
                        if (pieceToEat != null)
                        {
                            EatPiece(pieceToEat);
                        }
                    }
                    piece.Position = move.Destination;
                    game.Turn = game.Turn == "White" ? "Black" : "White";

                    if (IsCheck(new ChessMove()))
                    {
                        game.Turn = game.Turn == "White" ? "Black" : "White";
                        return BadRequest("The move puts your king in check.");
                    }

                    if (IsCheckmate())
                    {
                        return BadRequest("The move results in checkmate.");
                    }

                }

            }


            _context.SaveChanges();


            var response = new
            {
                id = game.Id,
                turn = game.Turn,
                pieces = game.Pieces.Select(p => new
                {
                    name = p.Name,
                    id = p.Id,
                    image_url = p.ImageUrl,
                    position = p.Position
                })
            };


            return Ok(response);
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteChessItem(int id)
        {
            var chessItem = await _context.ChessGames.FindAsync(id);

            if (chessItem == null)
            {
                return NotFound();
            }

            _context.ChessGames.Remove(chessItem);
            await _context.SaveChangesAsync();

            return NoContent();
        }





        //PATCH metods
        private bool PieceExist(string position)
        {
            bool exsist = false;
            position = position.ToLower();


            if (position.Length != 2 || !char.IsLetter(position[0]) || !char.IsDigit(position[1]))
            {
                throw new ArgumentException("La posizione deve essere nel formato lettera seguita da numero (ad esempio: 'a3').");
            }


            char letter = position[0];
            int number = int.Parse(position[1].ToString());


            if (letter < 'a' || letter > 'h')
            {
                throw new ArgumentOutOfRangeException("La lettera deve essere compresa tra 'a' e 'h'.");
            }


            if (number < 1 || number > 8)
            {
                throw new ArgumentOutOfRangeException("Il numero deve essere compreso tra 1 e 8.");
            }



            ChessPiece? piece = _context.ChessPieces.FirstOrDefault(p => p.Position == position);

            if (piece != null)
            {

                exsist = true;

            }
            return exsist;
        }


        private void EatPiece(ChessPiece piece)
        {
            if (PieceExist(piece.Position))
            {

                var foundPiece = _context.ChessPieces.FirstOrDefault(p => p.Position == piece.Position);

                if (foundPiece != null)
                {
                    _context.ChessPieces.Remove(foundPiece);
                    _context.SaveChanges();

                }


            }

        }


        private bool IsValidMove(string position, string destination, ChessMove move)
        {
            if (!PieceExist(position))
            {
                return false;
            }
            else
            {
                char startL = position[0];
                int startN = int.Parse(position[1].ToString());
                char endL = destination[0];
                int endN = int.Parse(destination[1].ToString());

                int startX = startL - 'a' + 1;
                int startY = startN;
                int endX = endL - 'a' + 1;
                int endY = endN;

                ChessPiece piece = _context.ChessPieces.FirstOrDefault(p => p.Position == position);
                if (piece == null) return false;

                string[] idParts = piece.Id.Split('_');
                string color = idParts[0];
                string name = idParts[1];
                string number = idParts.Length > 2 ? idParts[2] : null;


                switch (name.ToLower())
                {
                    case "pawn":
                        if (color == "white")
                        {
                            if (piece.IsFirstMove && endY - startY == 2 && startX == endX && !PieceExist(((char)(startX + 'a' - 1)).ToString() + (startY + 1)) && !PieceExist(move.Destination)) return true;
                            if (endY - startY != 1 || startX != endX || PieceExist(move.Destination)) return false;
                            if (endY - startY == 1 && Math.Abs(endX - startX) == 1 && !PieceExist(move.Destination)) return false;
                        }
                        else if (color == "black")
                        {
                            if (piece.IsFirstMove && startY - endY == 2 && startX == endX && !PieceExist(((char)(startX + 'a' - 1)).ToString() + (startY - 1)) && !PieceExist(move.Destination)) return true;
                            if (startY - endY != 1 || startX != endX || PieceExist(move.Destination)) return false;
                            if (startY - endY == 1 && Math.Abs(endX - startX) == 1 && !PieceExist(move.Destination)) return false;
                        }
                        break;

                    case "rook":
                        if (startX != endX && startY != endY) return false;
                        break;
                    case "knight":
                        if (Math.Abs(endX - startX) * Math.Abs(endY - startY) != 2) return false;
                        break;
                    case "bishop":
                        if (Math.Abs(endX - startX) != Math.Abs(endY - startY)) return false;
                        break;
                    case "queen":
                        if (startX != endX && startY != endY && Math.Abs(endX - startX) != Math.Abs(endY - startY)) return false;
                        break;
                    case "king":
                        if (Math.Max(Math.Abs(endX - startX), Math.Abs(endY - startY)) > 1) return false;
                        break;
                }
            }
            return true;
        }


        private bool IsCheck(ChessMove move)
        {
            ChessPiece king = _context.ChessPieces.FirstOrDefault(p => p.Id.ToLower().Contains((_context.ChessGames.FirstOrDefault().Turn.ToLower() == "white" ? "white_king" : "black_king")));

            string kingPosition = king.Position;
            char kingX = kingPosition[0];
            int kingY = int.Parse(kingPosition[1].ToString());

            foreach (var piece in _context.ChessPieces)
            {
                string color = piece.Id.Split('_')[0];

                if (color != (_context.ChessGames.FirstOrDefault().Turn.ToLower() == "white" ? "white" : "black"))
                {
                    if (IsValidMove(piece.Position, kingPosition, move))
                    {
                        return true;
                    }
                }
                else
                {
                    for (char character = 'a'; character <= 'h'; character++)
                    {
                        for (int number = 1; number <= 8; number++)
                        {
                            string newPosition = $"{character}{number}";

                            ChessMove simulatedMove = new ChessMove { OnePiece = piece.Id, Destination = newPosition };

                            if (IsValidMove(piece.Position, newPosition, simulatedMove))
                            {
                                string oldPosition = piece.Position;
                                piece.Position = newPosition;

                                bool check = IsCheck(simulatedMove);

                                piece.Position = oldPosition;

                                if (!check) return false;
                            }
                        }
                    }
                }
            }

            return false;
        }



        private bool IsCheckmate()
        {
            var game = _context.ChessGames.FirstOrDefault();

            if (game == null)
            {
                return false;
            }

            if (IsCheck(new ChessMove()))
            {
                game.GameState = "Check";
                if (IsCheckmate())
                {
                    game.GameState = "Checkmate";
                }
            }
            else
            {
                game.GameState = "Normal";
            }

            _context.SaveChanges();

            return game.GameState == "Checkmate";
        }


        private void PromotePawn(ChessPiece pawn, string newPiece)
        {
            if ((pawn.Name.ToLower() == "pawn" && pawn.Id.StartsWith("white") && pawn.Position[1] == '8') ||
                (pawn.Name.ToLower() == "pawn" && pawn.Id.StartsWith("black") && pawn.Position[1] == '1'))
            {
                if (newPiece.ToLower() == "queen" || newPiece.ToLower() == "rook" ||
                    newPiece.ToLower() == "bishop" || newPiece.ToLower() == "knight")
                {
                    string[] idParts = pawn.Id.Split('_');
                    string color = idParts[0];
                    pawn.Name = newPiece;
                    pawn.Id = $"{color}_{newPiece}_{idParts[2]}";
                }
                else
                {
                    throw new ArgumentException("Invalid piece for promotion. Must be queen, rook, bishop, or knight.");
                }
            }
            else
            {
                throw new InvalidOperationException("Pawn must be on the opposite side of the board to be promoted.");
            }
        }


    }
}