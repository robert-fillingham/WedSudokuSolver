using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using SudokuApi.Logic;
using SudokuApi.Models;
using System.Net;

namespace SudokuApi.Controllers
{
    [ApiController]
    [EnableCors]
    [Route("[controller]/Sudoku")]
    public class Sudoku : Controller
    {
        [HttpPost]
        public JsonResult GetAnswer(RequestBody body)
        {
            if (body.ca.Length != 81)
            {
                return new JsonResult(new ResponseBody { status = -1, message = $"Inccorect number of cells.  Expected 81, Recieved: {body.ca.Length}"});
            }

            if (!ValidateAndPopulateCells.verifyCells(body.ca))
            {
                return new JsonResult(new ResponseBody { status = -1, message = "One or more cells contained an invalid value" });
            }

            ValidateAndPopulateCells.setupCells(body.ca);

            foreach(Cell c in body.ca)
            {
                c.calculatepossible(body.ca);
            }

            int checkingIndex = -1;

            bool cont = true;
            string next = "NG";
            while (cont)
            {
                switch (next)
                {
                    case "NG":
                        checkingIndex++;
                        if (checkingIndex == 81)
                        {
                                return new JsonResult(new ResponseBody() { status = 1, message = "Solved", cells = body.ca });
                        }
                        if (!body.ca[checkingIndex].known)
                        {
                            next = body.ca[checkingIndex].nextGuess(body.ca);
                        }
                        break;

                    case "RB":
                        checkingIndex--;
                        if (checkingIndex == -1)
                        {
                            Console.WriteLine("COULD NOT SOLVE GRID");
                            return new JsonResult(new ResponseBody() { status = -1, message = "Couldn't solve the grid" });
                        }
                        if (!body.ca[checkingIndex].known)
                        {
                            next = body.ca[checkingIndex].nextGuess(body.ca);
                        }
                        break;
                    default:
                        cont = false;
                        break;

                }

            }
            return new JsonResult(new ResponseBody() { status = -1, message = "You shouldn't be able to get here, something went very wrong." });
        }
    }
}
