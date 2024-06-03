/*public interface IAnimal
{
    void Move();
    string Name { get; }

    // direcations for the animal to move
    List<Tuple<int, int>> MoveDirections { get; }

    System.ConsoleColor AnimalBackgroundColor { get; }
    System.ConsoleColor AnimalForegroundColor { get; }

    string getName();

}*/

public interface IAnimal
{
    public void Move(Zoo zoo)
    {
        Random rnd = new Random();
        var directions = new List<Tuple<int, int>>(MoveDirections);
        var currentPos = zoo.GetAnimalPosition(this);
        bool moved = false;

        while (directions.Count > 0 && !moved)
        {
            int index = rnd.Next(directions.Count);
            var (dirRow, dirCol) = directions[index];
            directions.RemoveAt(index);

            int newRow = currentPos.row + dirRow * zoo.GetAnimalMatrixSize();
            int newCol = currentPos.col + dirCol * zoo.GetAnimalMatrixSize();

            if (newRow >= 0 && newRow <= zoo.GetZooMap().Length - zoo.GetAnimalMatrixSize() &&
                newCol >= 0 && newCol <= zoo.GetZooMap()[0].Length - zoo.GetAnimalMatrixSize() &&
                zoo.CheckIfEmpty(newRow, newCol))
            {
                zoo.ClearAnimalPosition(this, currentPos.row, currentPos.col);
                zoo.InsertAnimal(this, newRow, newCol);
                zoo.UpdateRowAndColArrays(currentPos.row, currentPos.col, newRow, newCol);
                zoo.UpdateSpecificCellsAfterAnimalMove2(currentPos.row, currentPos.col, newRow, newCol);
                moved = true;
            }
        }
        if (!moved)
        {
            //Console.WriteLine($"{Name} couldn't find a place to move.");
        }
    }

    // direcations for the animal to move
    List<Tuple<int, int>> MoveDirections { get; }

    public System.ConsoleColor AnimalBackgroundColor { get; }
    public System.ConsoleColor AnimalForegroundColor { get; }

    public AnimalType AnimalType { get; }
}