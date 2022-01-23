class StockCounting {
  int stockCountDetailid;
  int itemPriceId;
  int quantity;
  int stockCountId;
  double MRP;
  double salesPrice;
  String itemCode;
  String itemName;

  StockCounting(
      {this.stockCountDetailid,
      this.itemCode,
      this.stockCountId,
        this.quantity,
      this.itemName,
      this.itemPriceId,
      this.MRP,
      this.salesPrice});
  factory StockCounting.fromJson(Map<String, dynamic> parsed) {
    return StockCounting(
        stockCountDetailid: parsed["stockcountingdetailid"],
        itemCode: parsed["itemcodE1"] as String,
        quantity: parsed["quantity"],
        stockCountId: parsed["stockcountingid"],
        itemName: parsed["itemname"] as String,
        itemPriceId: parsed["itempriceid"],
        MRP: parsed["mrp"] as double,
        salesPrice: parsed["saleprice"] as double);
  }
}
