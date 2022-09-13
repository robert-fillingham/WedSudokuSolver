namespace SudokuApi.Models
{
    public class Cell
    {
        public int value { get; set; }
        public bool known { get; set; }
        public int row { get; set; }
        public int col { get; set; }
        public int group { get; set; }
        public int guessindex { get; set; }

        public List<int> possibles = new List<int>();

        public void calculatepossible(Cell[] CA)
        {
            List<int> present = new List<int>();
            List<int> temp = new List<int>();
            foreach (Cell c in CA)
            {
                if (c.row == this.row || c.col == this.col || c.group == this.group)
                {
                    present.Add(c.value);
                }
            }
            for (int i = 1; i < 10; i++)
            {
                if (!present.Contains(i))
                {
                    temp.Add(i);
                }
            }

            possibles = temp;
        }

        public bool checkValidity(Cell[] CA)
        {
            foreach (Cell c in CA)
            {
                if (c != this && (c.row == this.row || c.col == this.col || c.group == this.group))
                {
                    if (c.value == this.value)
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        public string nextGuess(Cell[] CA)
        {
            if (this.guessindex >= this.possibles.Count)
            {
                this.guessindex = 0;
                this.value = 0;
                return "RB";
            }
            else
            {
                this.value = this.possibles[this.guessindex];
                this.guessindex++;

                if (this.checkValidity(CA))
                {
                    return "NG";
                }
                else
                {
                    return this.nextGuess(CA);
                };
            }
        }
    }

}
