using entity.gifts;
using entity.giftsinbag;
using entity.goods;
using entity.taskcondition;
using service.solution;
using service;

    
namespace result {

public class ResultOfTaskAboutGift 
{
     static void Main(string [] args)
    {
            /* 
            nameFileGoods - it's filename of the Goods values
            nameFileTaskConditions - it's filename of the TaskConditions values

            example run command from the terminal in this app : 
                      dotnet run ingoods.csv intaskconditions.csv
            */

        string nameFileGoods = args [0];
        string nameFileTaskConditions = args[1];

            List<Goods> inputGoods = CsvRead.getListGoods(nameFileGoods);
            TaskConditions taskConditions = CsvRead.getTaskConditions(nameFileTaskConditions);

      
        /* 
        Here is typing of the TaskConditions  
        */
        Console.WriteLine(taskConditions);

        /* 
        Here is typing of the Goods list from the DB  
        */
            foreach(Goods goods in inputGoods){
            Console.WriteLine(goods);
        }
        
        SolutionDTO solution = new SolutionDTO(inputGoods, taskConditions);
            Console.WriteLine("BUGET = " + solution.delta);
            solution.createGifts();
            Console.WriteLine("CHANGE MONEY after  = " + solution.delta);
            
        /* 
        See result in the testres.txt file
        */   
            solution.printResult();
 
    }
  
}
}