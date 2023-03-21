

using System.Text;
using entity.gifts;
using entity.giftsinbag;
using entity.goods;
using entity.taskcondition;

namespace service.solution
{
 
    class SolutionDTO {

    public List<Goods> goodsList;
    public TaskConditions taskCondition;
    public List<Gifts> bagGifts;
    public List<GiftsInBag> resultGifts;
    public int criteria = 1;
    public Decimal delta;

     public SolutionDTO(List<Goods> goodsLists, TaskConditions taskCondition) {
        this.goodsList = goodsLists;
        this.taskCondition = taskCondition;
        this.bagGifts = new List<Gifts>();
        this.resultGifts = new List<GiftsInBag>();
        this.delta = taskCondition.Budget;
    }


  public void createGifts() {
        this.bagGifts = initGifts();
        while(this.bagGifts.Count != 0) {
            getBagsWithGifts();
            this.bagGifts = encountGifts(this.bagGifts);
        }
    }


     private List<Gifts> initGifts() {
        Gifts gift ;
        for (int i = 0; i < this.goodsList.Count; i++){
            if ((this.goodsList[i].PresentPrice == taskCondition.Budget)
                    || (this.goodsList[i].PresentVolume == taskCondition.BagVolume) ){
                goodsList.RemoveAt(i);
                i = 0;
            }
        }

        for (int i = 0; i < this.goodsList.Count; i++) {
            List<Goods> goods = new List<Goods>();
            goods.Add(cloneGood(this.goodsList[i]));
            gift = new Gifts(goods);
            this.bagGifts.Add(gift);
        }
        return this.bagGifts;
    }


     private List<Gifts> encountGifts(List<Gifts> gifts) {
        List<Gifts> newGifts = new List<Gifts>();
        Gifts newGift ;
        List<Goods> newGoods;
        foreach (Gifts gift in gifts) {
            for (int i = 0; i < goodsList.Count; i++) {
                newGoods = cloneListGoods(gift.Gift);
                newGoods.Add(cloneGood(goodsList[i]));
                newGift = new Gifts(newGoods);
                if((taskCondition.Budget >= newGift.PriceGift) 
                        && (taskCondition.BagVolume >= newGift.VolumeGift)
                        && (isQuantityGifts(newGift))) {
                    newGifts.Add(newGift);
                }
            }
        }
        if (newGifts.Count > 1){
        newGifts = deleteEqualsListGifts(newGifts);
        }
        return newGifts;
    }


    private void getBagsWithGifts(){
        Boolean[][] matrix = initMatrix(taskCondition.PeopleNum, this.bagGifts);
        GiftsInBag giftsInBag ;
        List<Gifts> giftsList = new List<Gifts>();
        do {
            for (int i = 0; i < matrix.Length; i++){
                for (int j = 0; j < matrix[i].Length; j++){
                    if (matrix[i][j]) {

                        giftsList.Add(cloneGift(this.bagGifts[j]));
                    }
                }
            }
            giftsInBag = new GiftsInBag(giftsList, taskCondition);


            if (isQuantityGiftsInBag(giftsInBag)) {

                if (giftsInBag.DeltaToBudget == this.delta) {
                    this.resultGifts.Add(giftsInBag);
                    this.resultGifts = deleteSimilarGiftsInBag(resultGifts);
                } else {
                    if (this.delta > giftsInBag.DeltaToBudget ) {
                        this.resultGifts = new List<GiftsInBag>();
                        this.resultGifts.Add(cloneGiftsInBag(giftsInBag));
                        delta = giftsInBag.DeltaToBudget;
                    }
                }
            }
            giftsList = new List<Gifts>();
            incrementMatrix(matrix);
        } while (!isMatrixComplete(matrix));
    }

  private List<Gifts> deleteEqualsListGifts(List <Gifts> giftsList){
        if (giftsList.Count < 2){
            return giftsList;
        } else {
            for (int i = 0; i <= giftsList.Count - 2; i++) {
                for (int j = giftsList.Count - 1; j > i; j--) {
                    if (giftsList[i].Equals(giftsList[j])) {
                        giftsList.RemoveAt(j);
                        if (giftsList.Count > 1) {
                            return deleteEqualsListGifts(giftsList);
                        } else return giftsList;
                    }
                }
            }
        }
        return giftsList;
    }

 private Boolean isQuantityGifts(Gifts gift){
       if (gift.Gift.Count == 0){
           return false;
       } else {
           long id = 0L;
           int i = 0;
           foreach (Goods good in gift.Gift) {
               if (id == 0L) {
                   id = good.IdGood;
                   i++;
               } else {
                   if (id == good.IdGood) {
                       i++;
                       if (i > good.Quantity) {
                           return false;
                       }
                   } else {
                       if (id != good.IdGood) {
                           id = good.IdGood;
                           i = 1;
                       }
                   }
               }
           }
       }
         return true;
    }

 private Boolean isQuantityGiftsInBag(GiftsInBag giftsInBag){
        int num = 0;
        foreach (Goods goodStore in goodsList) {
            foreach (Gifts gift in giftsInBag.BagWithGifts) {
                foreach (Goods good in gift.Gift) {
                    if (good.IdGood == goodStore.IdGood){
                        num++;
                        if (num > goodStore.Quantity){
                            return false;
                        }
                    }
                }
            }
            num = 0;
        }
        return true;
    }


   private List<GiftsInBag> deleteSimilarGiftsInBag(List<GiftsInBag> result){
        if (result.Count < 2) {
            return result;
        } else {
            for (int i = 0; i <= result.Count - 2; i++) {
                for (int j = result.Count - 1; j > i; j--) {
                    if (result[i].Equals(result[j])) {
                        result.RemoveAt(j);
                        if (result.Count > 1) {
                            return deleteSimilarGiftsInBag(result);
                        } else {
                            return result;
                        }
                        }
                    }
                }
            }

        return result;
    }


     public Goods cloneGood(Goods good) {
        return new Goods(good.IdGood, good.GoodName, good.PresentVolume, good.PresentPrice, good.Quantity);
    }


     public List<Goods> cloneListGoods(List<Goods> goods) {
        List<Goods> clonedGoods = new List<Goods>();
        foreach (Goods good in goods) {
            clonedGoods.Add(new Goods(good.IdGood, good.GoodName, good.PresentVolume, good.PresentPrice, good.Quantity));
        }
        return clonedGoods;
    }

     public Gifts cloneGift(Gifts gift) {
        List<Goods> goods = cloneListGoods(gift.Gift);
        return new Gifts(goods);
    }

  public List<Gifts> cloneListGift(List<Gifts> gifts) {
        List<Gifts> newGifts = new List<Gifts>();
        foreach(Gifts gift in gifts){
            newGifts.Add(cloneGift(gift));
        }
        return newGifts;
    }


   private GiftsInBag cloneGiftsInBag(GiftsInBag giftsInBag){
        List<Gifts> gifts = cloneListGift(giftsInBag.BagWithGifts);
        return new GiftsInBag(gifts, taskCondition);
    }


  private Boolean[][] initMatrix(int numPersons, List<Gifts> listGifts) {
        Boolean[][] matrix = new Boolean [numPersons][];
        for(int i = 0; i < matrix.Length; i++){
            matrix[i] = new Boolean[listGifts.Count];
            for(int j = 0; j < listGifts.Count; j++){
                matrix[i][j] = false;
            }
        }

        for (int i = 0; i < numPersons; i++) {
            for (int j = 0; j < listGifts.Count; j++) {
                if (j == 0) {
                    matrix[i][j] = true;
                } else {
                    matrix[i][j] = false;
                }
            }
        }
        return matrix;
    }


   private Boolean[][] incrementMatrix(Boolean[][] matrix) {
        if (matrix[0].Length == 1){return matrix;}
        for (int i = 0; i < matrix.Length; i++) {
            for (int j = 0; j < matrix[i].Length; j++) {
                if (matrix[i][j] == true) {
                    if (j < matrix[i].Length - 1) {
                        matrix[i][j] = false;
                        matrix[i][j + 1] = true;
                        return matrix;
                    } else {
                        if (j == matrix[i].Length - 1) {
                            matrix[i][j] = false;
                            matrix[i][0] = true;
                        }
                    }
                }
            }
        }
        return matrix;
    }


  private Boolean isMatrixComplete(Boolean[][] matrix) {
        if (matrix[0].Length == 1){return true;}
        int isComplete = 0;
        for (int i = 0; i < matrix.Length; i++) {
            if (matrix[i][0] == true) {
                isComplete++;
            }
        }
        if (isComplete == matrix.Length) {
            return true;
        } else {
            return false;
        }
    }


  public void printResult(){

        StreamWriter sw = new StreamWriter("testres.txt");

        sw.WriteLine("For budget = " + taskCondition.Budget
                          + "  volume = " + taskCondition.BagVolume
                          + " number of people = " + taskCondition.PeopleNum
                          + "  RESULT ITEMS = " + resultGifts.Count);
        String bar = "";
        for (int i = 0; i < taskCondition.PeopleNum; i++){
            bar = bar + "id\t\t\t" + "price\t\t\t" + "volume\t\t\t";
        }
        sw.WriteLine(bar + " items price\t" + " items volume\t\t\t");
        StringBuilder stringResult = new StringBuilder();

        if (this.resultGifts.Count == 0 ){
            sw.WriteLine("No solutions for conditions  : " + taskCondition);
            return;
        }

        foreach (GiftsInBag gifts in this.resultGifts){
             foreach (Gifts gift in gifts.BagWithGifts){
                foreach (Goods good in gift.Gift){
                    stringResult = stringResult.Append(good.IdGood).Append(',');
                }
                stringResult.Remove(stringResult.Length-1,1)
                            .Append("\t\t\t")
                            .Append(gift.PriceGift).Append("\t\t\t")
                            .Append(gift.VolumeGift).Append("\t\t\t");

            }
            stringResult.Append(gifts.howPriceBag(gifts.BagWithGifts)).Append("\t\t\t")
                        .Append(gifts.howVolumeBag(gifts.BagWithGifts)).Append("\t\t\t")
                    .Append("\nbudget = ").Append(taskCondition.Budget).Append(";  ")
                    .Append("bagVolume = ").Append(taskCondition.BagVolume);
            sw.WriteLine(stringResult );
            stringResult = new StringBuilder();

        }
        sw.WriteLine("\n\n");
        sw.Close();
        }

    }
}
