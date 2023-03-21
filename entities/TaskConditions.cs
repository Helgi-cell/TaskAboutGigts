namespace entity.taskcondition{


class TaskConditions
{

    private decimal budget;
    private decimal bagVolume;
    private int peopleNum;

    public TaskConditions(decimal budget, decimal bagVolume, int peopleNum)
    {
        this.Budget = budget;
        this.BagVolume = bagVolume;
        this.PeopleNum = peopleNum;
    }



    public decimal Budget { get => budget; set => budget = value; }
    public decimal BagVolume { get => bagVolume; set => bagVolume = value; }
    public int PeopleNum { get => peopleNum; set => peopleNum = value; }

  

    public override string? ToString()
    {
        return "budget = " + this.budget + "\n bag = " + this.bagVolume + "\n people = " + this.peopleNum + "\n\n";
    }
}
}