using entity.goods;
using Microsoft.VisualBasic.FileIO;
using entity.taskcondition;

namespace service {
    class CsvRead {
          public static List<Goods> getListGoods(string nameFile){
           
            var delimiter = ";";
                 List<Goods> listGoods = new List<Goods>();
                
                using (var parser = new TextFieldParser(nameFile))
                {
                    parser.TextFieldType = FieldType.Delimited;
                    parser.SetDelimiters(delimiter);

                    while (!parser.EndOfData)
                    {
                        string[]? fields = parser.ReadFields();
                        List<string> list = fields.ToList();
                    listGoods.Add(new Goods(long.Parse(list[0]), list[1], decimal.Parse(list[3]), decimal.Parse(list[2]), Int32.Parse(list[4])));
                    }
                }

        return listGoods;
    }


        public static TaskConditions getTaskConditions(string nameFile)
        {

            var delimiter = ";";
            TaskConditions taskConditions = null;

            using (var parser = new TextFieldParser(nameFile))
            {
                parser.TextFieldType = FieldType.Delimited;
                parser.SetDelimiters(delimiter);
                
                while (!parser.EndOfData)
                {
                    string[]? fields = parser.ReadFields();
                    List <string> list = fields.ToList();
                    taskConditions = (new TaskConditions(decimal.Parse(list[0]), decimal.Parse(list[1]), Int32.Parse(list[2])));
                }
            }
            if(taskConditions == null){
                return new TaskConditions(0m,0m,0);
            } else{
                return taskConditions;
            }
        }
}
}